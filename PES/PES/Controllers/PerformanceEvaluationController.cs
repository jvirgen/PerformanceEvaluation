using PES.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
using PES.Services;

namespace PES.Controllers
{

    [AllowAnonymous]
    public class PerformanceEvaluationController : Controller
    {
        // Declare services 
        private PEService _peService;
        private EmployeeService _employeeService;
        private TitleService _titleService;
        private SubtitleService _subtitleService;
        private DescriptionService _descriptionService;
        private ScoreService _scoreService;
        private CommentService _commentService;
        private SkillService _skillService;
        private LM_SkillService _lm_skillService;
        private StatusService _statusService;

        public PerformanceEvaluationController() 
        {
            _peService = new PEService();
            _employeeService = new EmployeeService();
            _titleService = new TitleService();
            _subtitleService = new SubtitleService();
            _descriptionService = new DescriptionService();
            _scoreService = new ScoreService();
            _commentService = new CommentService();
            _skillService = new SkillService();
            _lm_skillService = new LM_SkillService();
            _statusService = new StatusService();
        }

        // GET: PerformanceEvaluation
        public ActionResult Index()
        {
            List<PES.Models.PEs> performanceEvaluations = new List<Models.PEs>();

            // Get performance evaluation
            string queryPEs;
            //Whille()
            // 

            //string userId = "1";
            //string managerId = "2";
            //// Get user 

            //Employee e;
            //e = new Employee();
            //e.HireDate = DateTime.Now;
            //e.Ranking = 0;
            

            // Get manager of user 


            // Insert employee 

            //foreach(var pe in performanceEvaluations)
            //{
            //    // Get comments 
            //    string queryComments;
                
            //    // Get employee
            //    string query = "select * from Employee where Employee_id =  " + pe.EmployeeId;
            //    PES.Models.Employee employee;
            //    employee = new PES.Models.Employee();
            //    //employee; 
            //    // Get 
            //}

            

            return View();


        }

        public PESComplete ReadPerformanceFile(string path, Employee user, Employee evaluator) 
        {
            // Read
            /*read excel*/
            Excel.Application excel = new Excel.Application();
            Excel.Workbook wb = excel.Workbooks.Open(path);
            Excel.Worksheet excelSheet = wb.ActiveSheet;
            PESComplete PESc = new PESComplete();

            #region Performance Evaluation - insert
            PESc.pes.Total = excelSheet.Cells[6,9].Value;
            PESc.pes.EmployeeId = user.EmployeeId;
            PESc.pes.EvaluatorId = evaluator.EmployeeId;
            PESc.pes.EvaluationPeriod = DateTime.Now.Date;
            var status = _statusService.GetStatusByDescription("Incomplete");
            PESc.pes.StatusId = status != null ? status.StatusId : 1; 
            PESc.pes.EnglishScore = 0;
            PESc.pes.PerformanceScore = 0;
            PESc.pes.CompeteneceScore = 0;

            // Insert 
            bool peInserted = _peService.InsertPE(PESc.pes);

            // Look for and get id 
            PESc.pes = _peService.GetPerformanceEvaluationByDateEmail(user.Email, DateTime.Now);
            #endregion

            #region Employee - update
            // data from user
            PESc.empleado.EmployeeId = user.EmployeeId;
            PESc.empleado.Email = user.Email;
            PESc.empleado.ProfileId = user.ProfileId;
            PESc.empleado.ManagerId = user.ManagerId; 
            PESc.empleado.HireDate = user.HireDate;
            PESc.empleado.Ranking = user.Ranking;
            PESc.empleado.EndDate = user.EndDate;

            // data from excel
            PESc.empleado.FirstName = excelSheet.Cells[3, 3].Value;
            PESc.empleado.LastName = excelSheet.Cells[3, 3].Value;
            PESc.empleado.Position = excelSheet.Cells[4, 3].Value;
            PESc.empleado.Customer = excelSheet.Cells[5, 3].Value;
            PESc.empleado.Project = excelSheet.Cells[6, 3].Value;
            #endregion

            #region Title - insert
            PESc.title1.Name = PerformanceSection.PerformanceTitle.Name;
            PESc.title2.Name = CompetencesSection.CompetenceTitle.Name;
            //PESc.title1.Name = excelSheet.Cells[19, 2].Value;
            //PESc.title2.Name = excelSheet.Cells[39, 2].Value;
            #endregion

            #region Subtitle - insert
            PESc.subtitle1.Name = PerformanceSection.QualitySubtitle.Name;
            PESc.subtitle1.TitleId = PerformanceSection.QualitySubtitle.TitleId;
            PESc.subtitle2.Name = PerformanceSection.OpportunitySubtitle.Name;
            PESc.subtitle2.TitleId = PerformanceSection.OpportunitySubtitle.TitleId;
            PESc.subtitle3.Name = CompetencesSection.SkillsSubtitle.Name;
            PESc.subtitle3.TitleId = CompetencesSection.SkillsSubtitle.TitleId;
            PESc.subtitle4.Name = CompetencesSection.InterpersonalSubtitle.Name;
            PESc.subtitle4.TitleId = CompetencesSection.InterpersonalSubtitle.TitleId;
            PESc.subtitle5.Name = CompetencesSection.GrowthSubtitle.Name;
            PESc.subtitle5.TitleId = CompetencesSection.GrowthSubtitle.TitleId;
            PESc.subtitle6.Name = CompetencesSection.PoliciesSubtitle.Name;
            PESc.subtitle6.TitleId = CompetencesSection.PoliciesSubtitle.TitleId;
            //PESc.subtitle1.Name = excelSheet.Cells[23, 2].Value;
            //PESc.subtitle2.Name = excelSheet.Cells[32, 2].Value;
            //PESc.subtitle3.Name = excelSheet.Cells[43, 2].Value;
            //PESc.subtitle4.Name = excelSheet.Cells[52, 2].Value;
            //PESc.subtitle5.Name = excelSheet.Cells[59, 2].Value;
            //PESc.subtitle6.Name = excelSheet.Cells[67, 2].Value;
            #endregion

            #region Description - insert
            PESc.description1.DescriptionText = PerformanceSection.AccuracyQualityDescription1.DescriptionText;
            PESc.description1.SubtitleId = PerformanceSection.AccuracyQualityDescription1.SubtitleId;
            PESc.description2.DescriptionText = PerformanceSection.ThoroughnessQualityDescription2.DescriptionText;
            PESc.description2.SubtitleId = PerformanceSection.ThoroughnessQualityDescription2.SubtitleId;
            PESc.description3.DescriptionText = PerformanceSection.ReliabilityQualityDescription3.DescriptionText;
            PESc.description3.SubtitleId = PerformanceSection.ReliabilityQualityDescription3.SubtitleId;
            PESc.description4.DescriptionText = PerformanceSection.ResponsivenessQualityDescription4.DescriptionText;
            PESc.description4.SubtitleId = PerformanceSection.ResponsivenessQualityDescription4.SubtitleId;
            PESc.description5.DescriptionText = PerformanceSection.FollowQualityDescription5.DescriptionText;
            PESc.description5.SubtitleId = PerformanceSection.FollowQualityDescription5.SubtitleId;
            PESc.description6.DescriptionText = PerformanceSection.JudgmentQualityDescription6.DescriptionText;
            PESc.description6.SubtitleId = PerformanceSection.JudgmentQualityDescription6.SubtitleId;
            PESc.subtotalDescQuality.DescriptionText = PerformanceSection.SubtotalQualityDescription7.DescriptionText;
            PESc.subtotalDescQuality.SubtitleId = PerformanceSection.SubtotalQualityDescription7.SubtitleId;

            PESc.description7.DescriptionText = PerformanceSection.PriorityOpportunityDescription8.DescriptionText;
            PESc.description7.SubtitleId = PerformanceSection.PriorityOpportunityDescription8.SubtitleId;
            PESc.description8.DescriptionText = PerformanceSection.AmountOpportunityDescription9.DescriptionText;
            PESc.description8.SubtitleId = PerformanceSection.AmountOpportunityDescription9.SubtitleId;
            PESc.description9.DescriptionText = PerformanceSection.WorkOpportunityDescription10.DescriptionText;
            PESc.description9.SubtitleId = PerformanceSection.WorkOpportunityDescription10.SubtitleId;
            PESc.subtotalOpportunity.DescriptionText = PerformanceSection.SubtotalOpportunityDescription11.DescriptionText;
            PESc.subtotalOpportunity.SubtitleId = PerformanceSection.SubtotalOpportunityDescription11.SubtitleId;
            PESc.totalPerformance.DescriptionText = PerformanceSection.TotalPerformanceDescription12.DescriptionText;
            PESc.totalPerformance.SubtitleId = PerformanceSection.TotalPerformanceDescription12.SubtitleId;

            PESc.description10.DescriptionText = CompetencesSection.JobSkillDescription13.DescriptionText;
            PESc.description10.SubtitleId = CompetencesSection.JobSkillDescription13.SubtitleId;
            PESc.description11.DescriptionText = CompetencesSection.AnalyzesSkillDescription14.DescriptionText;
            PESc.description11.SubtitleId = CompetencesSection.AnalyzesSkillDescription14.SubtitleId;
            PESc.description12.DescriptionText = CompetencesSection.FlexibleSkillDescription15.DescriptionText;
            PESc.description12.SubtitleId = CompetencesSection.FlexibleSkillDescription15.SubtitleId;
            PESc.description13.DescriptionText = CompetencesSection.PlanningSkillDescription16.DescriptionText;
            PESc.description13.SubtitleId = CompetencesSection.PlanningSkillDescription16.SubtitleId;
            PESc.description14.DescriptionText = CompetencesSection.CompetentSkillDescription17.DescriptionText;
            PESc.description14.SubtitleId = CompetencesSection.CompetentSkillDescription17.SubtitleId;
            PESc.description15.DescriptionText = CompetencesSection.FollowsSkillDescription18.DescriptionText;
            PESc.description15.SubtitleId = CompetencesSection.FollowsSkillDescription18.SubtitleId;
            PESc.subtotalSkills.DescriptionText = CompetencesSection.SubtotalSkillDescription19.DescriptionText;
            PESc.subtotalSkills.SubtitleId = CompetencesSection.SubtotalSkillDescription19.SubtitleId;

            PESc.description16.DescriptionText = CompetencesSection.SupervisorInterpersonalDescription20.DescriptionText;
            PESc.description16.SubtitleId = CompetencesSection.SupervisorInterpersonalDescription20.SubtitleId;
            PESc.description17.DescriptionText = CompetencesSection.OtherInterpersonalDescription21.DescriptionText;
            PESc.description17.SubtitleId = CompetencesSection.OtherInterpersonalDescription21.SubtitleId;
            PESc.description18.DescriptionText = CompetencesSection.ClientInterpersonalDescription22.DescriptionText;
            PESc.description18.SubtitleId = CompetencesSection.ClientInterpersonalDescription22.SubtitleId;
            PESc.description19.DescriptionText = CompetencesSection.CommitmentInterpersonalDescription23.DescriptionText;
            PESc.description19.SubtitleId = CompetencesSection.CommitmentInterpersonalDescription23.SubtitleId;
            PESc.subtotalInterpersonal.DescriptionText = CompetencesSection.SubtotalInterpersonalDescription24.DescriptionText;
            PESc.subtotalInterpersonal.SubtitleId = CompetencesSection.SubtotalInterpersonalDescription24.SubtitleId;

            PESc.description20.DescriptionText = CompetencesSection.ActivelyGrowthDescription25.DescriptionText;
            PESc.description20.SubtitleId = CompetencesSection.ActivelyGrowthDescription25.SubtitleId;
            PESc.description21.DescriptionText = CompetencesSection.OpenGrowthDescription26.DescriptionText;
            PESc.description21.SubtitleId = CompetencesSection.OpenGrowthDescription26.SubtitleId;
            PESc.description22.DescriptionText = CompetencesSection.InvolvementGrowthDescription27.DescriptionText;
            PESc.description22.SubtitleId = CompetencesSection.InvolvementGrowthDescription27.SubtitleId;
            PESc.description23.DescriptionText = CompetencesSection.ChallengesGrowthDescription28.DescriptionText;
            PESc.description23.SubtitleId = CompetencesSection.ChallengesGrowthDescription28.SubtitleId;
            PESc.description24.DescriptionText = CompetencesSection.SeeksGrowthDescription29.DescriptionText;
            PESc.description24.SubtitleId = CompetencesSection.SeeksGrowthDescription29.SubtitleId;
            PESc.subtotalGrowth.DescriptionText = CompetencesSection.SubtotalGrowthDescription30.DescriptionText;
            PESc.subtotalGrowth.SubtitleId = CompetencesSection.SubtotalGrowthDescription30.SubtitleId;

            PESc.descriptionPuctuality.DescriptionText = CompetencesSection.PunctualityPoliciesDescription31.DescriptionText;
            PESc.descriptionPuctuality.SubtitleId = CompetencesSection.PunctualityPoliciesDescription31.SubtitleId;
            PESc.descriptionPolicies.DescriptionText = CompetencesSection.PoliciesPoliciesDescription32.DescriptionText;
            PESc.descriptionPolicies.SubtitleId = CompetencesSection.PoliciesPoliciesDescription32.SubtitleId;
            PESc.descriptionValues.DescriptionText = CompetencesSection.ValuesPoliciesDescription33.DescriptionText;
            PESc.descriptionValues.SubtitleId = CompetencesSection.ValuesPoliciesDescription33.SubtitleId;
            PESc.subtotalPolicies.DescriptionText = CompetencesSection.SubtotalPoliciesDescription34.DescriptionText;
            PESc.subtotalPolicies.SubtitleId = CompetencesSection.SubtotalPoliciesDescription34.SubtitleId;
            PESc.totalCompetences.DescriptionText = CompetencesSection.TotalCompetencesDescription35.DescriptionText;
            PESc.totalCompetences.SubtitleId = CompetencesSection.TotalCompetencesDescription35.SubtitleId;

            //PESc.description1.DescriptionText = excelSheet.Cells[24, 2].Value;
            //PESc.description2.DescriptionText = excelSheet.Cells[25, 2].Value;
            //PESc.description3.DescriptionText = excelSheet.Cells[26, 2].Value;
            //PESc.description4.DescriptionText = excelSheet.Cells[27, 2].Value;
            //PESc.description5.DescriptionText = excelSheet.Cells[28, 2].Value;
            //PESc.description6.DescriptionText = excelSheet.Cells[29, 2].Value;
            //PESc.description7.DescriptionText = excelSheet.Cells[33, 2].Value;
            //PESc.description8.DescriptionText = excelSheet.Cells[34, 2].Value;
            //PESc.description9.DescriptionText = excelSheet.Cells[35, 2].Value;
            //PESc.description10.DescriptionText = excelSheet.Cells[44, 2].Value;
            //PESc.description11.DescriptionText = excelSheet.Cells[45, 2].Value;
            //PESc.description12.DescriptionText = excelSheet.Cells[46, 2].Value;
            //PESc.description13.DescriptionText = excelSheet.Cells[47, 2].Value;
            //PESc.description14.DescriptionText = excelSheet.Cells[48, 2].Value;
            //PESc.description15.DescriptionText = excelSheet.Cells[49, 2].Value;
            //PESc.description16.DescriptionText = excelSheet.Cells[53, 2].Value;
            //PESc.description17.DescriptionText = excelSheet.Cells[54, 2].Value;
            //PESc.description18.DescriptionText = excelSheet.Cells[55, 2].Value;
            //PESc.description19.DescriptionText = excelSheet.Cells[56, 2].Value;
            //PESc.description21.DescriptionText = excelSheet.Cells[61, 2].Value;
            //PESc.description22.DescriptionText = excelSheet.Cells[62, 2].Value;
            //PESc.description24.DescriptionText = excelSheet.Cells[64, 2].Value;
            //PESc.descriptionPuctuality.DescriptionText = excelSheet.Cells[68, 2].Value;
            //PESc.descriptionPolicies.DescriptionText = excelSheet.Cells[69, 2].Value;
            //PESc.descriptionValues.DescriptionText = excelSheet.Cells[70, 2].Value;
            //PESc.subtotalQuality.DescriptionText = excelSheet.Cells[30, 2].Value;
            //PESc.subtotalOpportunity.DescriptionText = excelSheet.Cells[36, 2].Value;
            //PESc.totalPerformance.DescriptionText = excelSheet.Cells[37, 2].Value;
            //PESc.subtotalSkills.DescriptionText = excelSheet.Cells[50, 2].Value;
            //PESc.subtotalInterpersonal.DescriptionText = excelSheet.Cells[57, 2].Value;
            //PESc.subtotalGrowth.DescriptionText = excelSheet.Cells[65, 2].Value;
            //PESc.subtotalPolicies.DescriptionText = excelSheet.Cells[71, 2].Value;
            //PESc.totalCompetences.DescriptionText = excelSheet.Cells[73, 2].Value;
            #endregion

            #region ScoreEmployee - insert
            PESc.one.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[24, 6].Value);
            PESc.one.DescriptionId = PerformanceSection.AccuracyQualityDescription1.DescriptionId;
            PESc.two.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[25, 6].Value);
            PESc.two.DescriptionId = PerformanceSection.ThoroughnessQualityDescription2.DescriptionId;
            PESc.three.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[26, 6].Value);
            PESc.thirteen.DescriptionId = PerformanceSection.ReliabilityQualityDescription3.DescriptionId;
            PESc.four.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[27, 6].Value);
            PESc.four.DescriptionId = PerformanceSection.ResponsivenessQualityDescription4.DescriptionId;
            PESc.five.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[28, 6].Value);
            PESc.five.DescriptionId = PerformanceSection.FollowQualityDescription5.DescriptionId;
            PESc.six.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[29, 6].Value);
            PESc.six.DescriptionId = PerformanceSection.JudgmentQualityDescription6.DescriptionId;
            PESc.scoreQuality.ScoreEmployee = 0;
            PESc.scoreQuality.DescriptionId = PerformanceSection.SubtotalQualityDescription7.DescriptionId;

            PESc.seven.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[33, 6].Value);
            PESc.seven.DescriptionId = PerformanceSection.PriorityOpportunityDescription8.DescriptionId;
            PESc.eight.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[34, 6].Value);
            PESc.eight.DescriptionId = PerformanceSection.AmountOpportunityDescription9.DescriptionId;
            PESc.nine.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[35, 6].Value);
            PESc.nine.DescriptionId = PerformanceSection.WorkOpportunityDescription10.DescriptionId;
            PESc.scoreOpportunity.ScoreEmployee = 0;
            PESc.scoreOpportunity.DescriptionId = PerformanceSection.SubtotalOpportunityDescription11.DescriptionId;
            PESc.scorePerformance.ScoreEmployee = 0;
            PESc.scorePerformance.DescriptionId = PerformanceSection.TotalPerformanceDescription12.DescriptionId;

            PESc.ten.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[44, 6].Value);
            PESc.ten.DescriptionId = CompetencesSection.JobSkillDescription13.DescriptionId;
            PESc.eleven.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[45, 6].Value);
            PESc.eleven.DescriptionId = CompetencesSection.AnalyzesSkillDescription14.DescriptionId;
            PESc.twelve.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[46, 6].Value);
            PESc.twelve.DescriptionId = CompetencesSection.FlexibleSkillDescription15.DescriptionId;
            PESc.thirteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[47, 6].Value);
            PESc.thirteen.DescriptionId = CompetencesSection.PlanningSkillDescription16.DescriptionId;
            PESc.fourteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[48, 6].Value);
            PESc.fourteen.DescriptionId = CompetencesSection.CompetentSkillDescription17.DescriptionId;
            PESc.fifteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[49, 6].Value);
            PESc.fifteen.DescriptionId = CompetencesSection.FollowsSkillDescription18.DescriptionId;
            PESc.scoreSkills.ScoreEmployee = 0;
            PESc.scoreSkills.DescriptionId = CompetencesSection.SubtotalSkillDescription19.DescriptionId;

            PESc.sixteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[53, 6].Value);
            PESc.sixteen.DescriptionId = CompetencesSection.SupervisorInterpersonalDescription20.DescriptionId;
            PESc.seventeen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[54, 6].Value);
            PESc.seventeen.DescriptionId = CompetencesSection.OtherInterpersonalDescription21.DescriptionId;
            PESc.eighteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[55, 6].Value);
            PESc.eighteen.DescriptionId = CompetencesSection.ClientInterpersonalDescription22.DescriptionId;
            PESc.nineteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[56, 6].Value);
            PESc.nineteen.DescriptionId = CompetencesSection.CommitmentInterpersonalDescription23.DescriptionId;
            PESc.scoreInterpersonal.ScoreEmployee = 0;
            PESc.scoreInterpersonal.DescriptionId = CompetencesSection.SubtotalInterpersonalDescription24.DescriptionId;

            PESc.twenty.ScoreEmployee = 0;
            PESc.twenty.DescriptionId = CompetencesSection.ActivelyGrowthDescription25.DescriptionId;
            PESc.twentyone.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[61, 6].Value);
            PESc.twentyone.DescriptionId = CompetencesSection.OpenGrowthDescription26.DescriptionId;
            PESc.twentytwo.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[62, 6].Value);
            PESc.twentytwo.DescriptionId = CompetencesSection.InvolvementGrowthDescription27.DescriptionId;
            PESc.twentythree.ScoreEmployee = 0;
            PESc.twentythree.DescriptionId = CompetencesSection.ChallengesGrowthDescription28.DescriptionId;
            PESc.twentyfour.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[64, 6].Value);
            PESc.twentyfour.DescriptionId = CompetencesSection.SeeksGrowthDescription29.DescriptionId;
            PESc.scoreGrowth.ScoreEmployee = 0;
            PESc.scoreGrowth.DescriptionId = CompetencesSection.SubtotalGrowthDescription30.DescriptionId;

            PESc.punctuality.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[68, 6].Value);
            PESc.punctuality.DescriptionId = CompetencesSection.PunctualityPoliciesDescription31.DescriptionId;
            PESc.policies.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[69, 6].Value);
            PESc.policies.DescriptionId = CompetencesSection.PoliciesPoliciesDescription32.DescriptionId;
            PESc.values.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[70, 6].Value);
            PESc.values.DescriptionId = CompetencesSection.ValuesPoliciesDescription33.DescriptionId;
            PESc.scorePolicies.ScoreEmployee = 0;
            PESc.scorePolicies.DescriptionId = CompetencesSection.SubtotalPoliciesDescription34.DescriptionId;
            PESc.scoreCompetences.ScoreEmployee = 0;
            PESc.scoreCompetences.DescriptionId = CompetencesSection.TotalCompetencesDescription35.DescriptionId;
            #endregion

            #region ScoreEvaluator - insert
            PESc.one.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[24, 7].Value);
            PESc.one.PEId = PESc.pes.PEId;
            PESc.two.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[25, 7].Value);
            PESc.two.PEId = PESc.pes.PEId; 
            PESc.three.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[26, 7].Value);
            PESc.four.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[27, 7].Value);
            PESc.five.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[28, 7].Value);
            PESc.six.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[29, 7].Value);
            
            PESc.seven.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[33, 7].Value);
            PESc.eight.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[34, 7].Value);
            PESc.nine.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[35, 7].Value);

            PESc.ten.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[44, 7].Value);
            PESc.eleven.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[45, 7].Value);
            PESc.twelve.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[46, 7].Value);
            PESc.thirteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[47, 7].Value);
            PESc.fourteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[48, 7].Value);
            PESc.fifteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[49, 7].Value);

            PESc.sixteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[53, 7].Value);
            PESc.seventeen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[54, 7].Value);
            PESc.eighteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[55, 7].Value);
            PESc.nineteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[56, 7].Value);
            PESc.twentyone.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[61, 7].Value);
            PESc.twentytwo.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[62, 7].Value);
            PESc.twentyfour.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[64, 7].Value);
            PESc.punctuality.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[68, 7].Value);
            PESc.policies.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[69, 7].Value);
            PESc.values.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[70, 7].Value);
            #endregion

            #region Comments - insert
            PESc.one.Comments = excelSheet.Cells[24, 8].Value;
            PESc.two.Comments = excelSheet.Cells[25, 8].Value;
            PESc.three.Comments = excelSheet.Cells[26, 8].Value;
            PESc.four.Comments = excelSheet.Cells[27, 8].Value;
            PESc.five.Comments = excelSheet.Cells[28, 8].Value;
            PESc.six.Comments = excelSheet.Cells[29, 8].Value;
            PESc.seven.Comments = excelSheet.Cells[33, 8].Value;
            PESc.eight.Comments = excelSheet.Cells[34, 8].Value;
            PESc.nine.Comments = excelSheet.Cells[35, 8].Value;
            PESc.ten.Comments = excelSheet.Cells[44, 8].Value;
            PESc.eleven.Comments = excelSheet.Cells[45, 8].Value;
            PESc.twelve.Comments = excelSheet.Cells[46, 8].Value;
            PESc.thirteen.Comments = excelSheet.Cells[47, 8].Value;
            PESc.fourteen.Comments = excelSheet.Cells[48, 8].Value;
            PESc.fifteen.Comments = excelSheet.Cells[49, 8].Value;
            PESc.sixteen.Comments = excelSheet.Cells[53, 8].Value;
            PESc.seventeen.Comments = excelSheet.Cells[54, 8].Value;
            PESc.eighteen.Comments = excelSheet.Cells[55, 8].Value;
            PESc.nineteen.Comments = excelSheet.Cells[56, 8].Value;
            PESc.twentyone.Comments = excelSheet.Cells[61, 8].Value;
            PESc.twentytwo.Comments = excelSheet.Cells[62, 8].Value;
            PESc.twentyfour.Comments = excelSheet.Cells[64, 8].Value;
            PESc.punctuality.Comments = excelSheet.Cells[68, 8].Value;
            PESc.policies.Comments = excelSheet.Cells[69, 8].Value;
            PESc.values.Comments = excelSheet.Cells[70, 8].Value;
            #endregion

            #region Calculation - insert
            PESc.one.Calculation = excelSheet.Cells[24, 9].Value;
            PESc.two.Calculation = excelSheet.Cells[25, 9].Value;
            PESc.three.Calculation = excelSheet.Cells[26, 9].Value;
            PESc.four.Calculation = excelSheet.Cells[27, 9].Value;
            PESc.five.Calculation = excelSheet.Cells[28, 9].Value;
            PESc.six.Calculation = excelSheet.Cells[29, 9].Value;
            PESc.seven.Calculation = excelSheet.Cells[33, 9].Value;
            PESc.eight.Calculation = excelSheet.Cells[34, 9].Value;
            PESc.nine.Calculation = excelSheet.Cells[35, 9].Value;
            PESc.ten.Calculation = excelSheet.Cells[44, 9].Value;
            PESc.eleven.Calculation = excelSheet.Cells[45, 9].Value;
            PESc.twelve.Calculation = excelSheet.Cells[46, 9].Value;
            PESc.thirteen.Calculation = excelSheet.Cells[47, 9].Value;
            PESc.fourteen.Calculation = excelSheet.Cells[48, 9].Value;
            PESc.fifteen.Calculation = excelSheet.Cells[49, 9].Value;
            PESc.sixteen.Calculation = excelSheet.Cells[53, 9].Value;
            PESc.seventeen.Calculation = excelSheet.Cells[54, 9].Value;
            PESc.eighteen.Calculation = excelSheet.Cells[55, 9].Value;
            PESc.nineteen.Calculation = excelSheet.Cells[56, 9].Value;
            PESc.twentyone.Calculation = excelSheet.Cells[61, 9].Value;
            PESc.twentytwo.Calculation = excelSheet.Cells[62, 9].Value;
            PESc.twentyfour.Calculation = excelSheet.Cells[64, 9].Value;
            PESc.punctuality.Calculation = excelSheet.Cells[68, 9].Value;
            PESc.policies.Calculation = excelSheet.Cells[69, 9].Value;
            PESc.values.Calculation = excelSheet.Cells[70, 9].Value;
            #endregion

            #region Totals and subtotals - insert
            PESc.scoreQuality.Calculation = excelSheet.Cells[30, 9].Value;
            PESc.scoreOpportunity.Calculation = excelSheet.Cells[36, 9].Value;
            PESc.scorePerformance.Calculation = excelSheet.Cells[37, 9].Value;
            PESc.scoreSkills.Calculation = excelSheet.Cells[50, 9].Value;
            PESc.scoreInterpersonal.Calculation = excelSheet.Cells[57, 9].Value;
            PESc.scoreGrowth.Calculation = excelSheet.Cells[65, 9].Value;
            PESc.scorePolicies.Calculation = excelSheet.Cells[71, 9].Value;
            PESc.scoreCompetences.Calculation = excelSheet.Cells[73, 9].Value;
            #endregion

            #region Trainning Comment - insert
            PESc.comment1.TrainningEmployee = excelSheet.Cells[78, 2].Value;
            PESc.comment2.TrainningEmployee = excelSheet.Cells[79, 2].Value;
            PESc.comment1.TrainningEvaluator = excelSheet.Cells[80, 2].Value;
            #endregion

            #region Acknowledge Comment - insert
            PESc.comment1.AcknowledgeEvaluator = excelSheet.Cells[82, 2].Value;
            PESc.comment2.AcknowledgeEvaluator = excelSheet.Cells[83, 2].Value;
            #endregion

            #region Comments and recommendations - insert
            PESc.comment1.CommRecommEmployee = excelSheet.Cells[85, 2].Value;
            PESc.comment2.CommRecommEmployee = excelSheet.Cells[86, 2].Value;
            PESc.comment3.CommRecommEmployee = excelSheet.Cells[87, 2].Value;
            PESc.comment1.CommRecommEvaluator = excelSheet.Cells[88, 2].Value;
            PESc.comment2.CommRecommEvaluator = excelSheet.Cells[89, 2].Value;
            PESc.comment3.CommRecommEvaluator = excelSheet.Cells[90, 2].Value;
            #endregion

            #region Skill description - insert
            //PESc.skill1.Description = excelSheet.Cells[96, 2].Value;
            //PESc.skill2.Description = excelSheet.Cells[97, 2].Value;
            //PESc.skill3.Description = excelSheet.Cells[98, 2].Value;
            //PESc.skill4.Description = excelSheet.Cells[99, 2].Value;
            //PESc.skill5.Description = excelSheet.Cells[100, 2].Value;
            //PESc.skill6.Description = excelSheet.Cells[101, 2].Value;
            //PESc.skill7.Description = excelSheet.Cells[102, 2].Value;
            //PESc.skill8.Description = excelSheet.Cells[103, 2].Value;
            //PESc.skill9.Description = excelSheet.Cells[104, 2].Value;
            //PESc.skill10.Description = excelSheet.Cells[105, 2].Value;
            //PESc.skill11.Description = excelSheet.Cells[106, 2].Value;
            //PESc.skill12.Description = excelSheet.Cells[107, 2].Value;
            //PESc.skill13.Description = excelSheet.Cells[108, 2].Value;
            //PESc.skill14.Description = excelSheet.Cells[109, 2].Value;
            //PESc.skill15.Description = excelSheet.Cells[110, 2].Value;
            //PESc.skill16.Description = excelSheet.Cells[111, 2].Value;
            //PESc.skill17.Description = excelSheet.Cells[112, 2].Value;
            #endregion

            #region skill check employee - insert
            PESc.supervises.CheckEmployee = excelSheet.Cells[96, 6].Value;
            PESc.coordinates.CheckEmployee = excelSheet.Cells[97, 6].Value;
            PESc.defines.CheckEmployee = excelSheet.Cells[98, 6].Value;
            PESc.supports.CheckEmployee = excelSheet.Cells[99, 6].Value;
            PESc.keeps.CheckEmployee = excelSheet.Cells[100, 6].Value;
            PESc.generates.CheckEmployee = excelSheet.Cells[101, 6].Value;
            PESc.trains.CheckEmployee = excelSheet.Cells[102, 6].Value;
            PESc.supportsExperimentation.CheckEmployee = excelSheet.Cells[103, 6].Value;
            PESc.evaluates.CheckEmployee = excelSheet.Cells[104, 6].Value;
            PESc.faces.CheckEmployee = excelSheet.Cells[105, 6].Value;
            PESc.supportsResponsible.CheckEmployee = excelSheet.Cells[106, 6].Value;
            PESc.helps.CheckEmployee = excelSheet.Cells[107, 6].Value;
            PESc.instills.CheckEmployee = excelSheet.Cells[108, 6].Value;
            PESc.sets.CheckEmployee = excelSheet.Cells[109, 6].Value;
            PESc.supportsUseful.CheckEmployee = excelSheet.Cells[110, 6].Value;
            PESc.welcomes.CheckEmployee = excelSheet.Cells[111, 6].Value;
            PESc.setsSpecific.CheckEmployee = excelSheet.Cells[112, 6].Value;
            #endregion

            #region skill check evaluator - insert
            PESc.supervises.CheckEvaluator = excelSheet.Cells[96, 6].Value;
            PESc.coordinates.CheckEvaluator = excelSheet.Cells[97, 6].Value;
            PESc.defines.CheckEvaluator = excelSheet.Cells[98, 6].Value;
            PESc.supports.CheckEvaluator = excelSheet.Cells[99, 6].Value;
            PESc.keeps.CheckEvaluator = excelSheet.Cells[100, 6].Value;
            PESc.generates.CheckEvaluator = excelSheet.Cells[101, 6].Value;
            PESc.trains.CheckEvaluator = excelSheet.Cells[102, 6].Value;
            PESc.supportsExperimentation.CheckEvaluator = excelSheet.Cells[103, 6].Value;
            PESc.evaluates.CheckEvaluator = excelSheet.Cells[104, 6].Value;
            PESc.faces.CheckEvaluator = excelSheet.Cells[105, 6].Value;
            PESc.supportsResponsible.CheckEvaluator = excelSheet.Cells[106, 6].Value;
            PESc.helps.CheckEvaluator = excelSheet.Cells[107, 6].Value;
            PESc.instills.CheckEvaluator = excelSheet.Cells[108, 6].Value;
            PESc.sets.CheckEvaluator = excelSheet.Cells[109, 6].Value;
            PESc.supportsUseful.CheckEvaluator = excelSheet.Cells[110, 6].Value;
            PESc.welcomes.CheckEvaluator = excelSheet.Cells[111, 6].Value;
            PESc.setsSpecific.CheckEvaluator = excelSheet.Cells[112, 6].Value;
            #endregion


            excel.Quit();
            
            return PESc;
        }

        public bool SavePEFile(PESComplete pEFile) 
        {
            try
            {
                // Call services to insert
                bool peResult = _peService.InsertPE(pEFile.pes);

                _employeeService.UpdateEmployee(pEFile.empleado);

                #region Titles, subtitles, descriptions and skills
                //_titleService.InsertTitle(pEFile.title1);
                //_titleService.InsertTitle(pEFile.title2);

                //_subtitleService.InsertSubtitles(pEFile.subtitle1);
                //_subtitleService.InsertSubtitles(pEFile.subtitle2);
                //_subtitleService.InsertSubtitles(pEFile.subtitle3);
                //_subtitleService.InsertSubtitles(pEFile.subtitle4);
                //_subtitleService.InsertSubtitles(pEFile.subtitle5);
                //_subtitleService.InsertSubtitles(pEFile.subtitle6);

                //_descriptionService.InsertDescription(pEFile.description1);
                //_descriptionService.InsertDescription(pEFile.description2);
                //_descriptionService.InsertDescription(pEFile.description3);
                //_descriptionService.InsertDescription(pEFile.description4);
                //_descriptionService.InsertDescription(pEFile.description5);
                //_descriptionService.InsertDescription(pEFile.description6);
                //_descriptionService.InsertDescription(pEFile.description7);
                //_descriptionService.InsertDescription(pEFile.description8);
                //_descriptionService.InsertDescription(pEFile.description9);
                //_descriptionService.InsertDescription(pEFile.description10);
                //_descriptionService.InsertDescription(pEFile.description11);
                //_descriptionService.InsertDescription(pEFile.description12);
                //_descriptionService.InsertDescription(pEFile.description13);
                //_descriptionService.InsertDescription(pEFile.description14);
                //_descriptionService.InsertDescription(pEFile.description15);
                //_descriptionService.InsertDescription(pEFile.description16);
                //_descriptionService.InsertDescription(pEFile.description17);
                //_descriptionService.InsertDescription(pEFile.description18);
                //_descriptionService.InsertDescription(pEFile.description19);
                //_descriptionService.InsertDescription(pEFile.description21);
                //_descriptionService.InsertDescription(pEFile.description22);
                //_descriptionService.InsertDescription(pEFile.description24);
                //_descriptionService.InsertDescription(pEFile.descriptionPuctuality);
                //_descriptionService.InsertDescription(pEFile.descriptionPolicies);
                //_descriptionService.InsertDescription(pEFile.descriptionValues);
                //_descriptionService.InsertDescription(pEFile.subtotalQuality);
                //_descriptionService.InsertDescription(pEFile.subtotalOpportunity);
                //_descriptionService.InsertDescription(pEFile.totalPerformance);
                //_descriptionService.InsertDescription(pEFile.subtotalSkills);
                //_descriptionService.InsertDescription(pEFile.subtotalInterpersonal);
                //_descriptionService.InsertDescription(pEFile.subtotalGrowth);
                //_descriptionService.InsertDescription(pEFile.subtotalPolicies);
                //_descriptionService.InsertDescription(pEFile.totalCompetences);

                //_skillService.InsertSkill(pEFile.skill1);
                //_skillService.InsertSkill(pEFile.skill2);
                //_skillService.InsertSkill(pEFile.skill3);
                //_skillService.InsertSkill(pEFile.skill4);
                //_skillService.InsertSkill(pEFile.skill5);
                //_skillService.InsertSkill(pEFile.skill6);
                //_skillService.InsertSkill(pEFile.skill7);
                //_skillService.InsertSkill(pEFile.skill8);
                //_skillService.InsertSkill(pEFile.skill9);
                //_skillService.InsertSkill(pEFile.skill10);
                //_skillService.InsertSkill(pEFile.skill11);
                //_skillService.InsertSkill(pEFile.skill12);
                //_skillService.InsertSkill(pEFile.skill13);
                //_skillService.InsertSkill(pEFile.skill14);
                //_skillService.InsertSkill(pEFile.skill15);
                //_skillService.InsertSkill(pEFile.skill16);
                //_skillService.InsertSkill(pEFile.skill17);
                #endregion

                _scoreService.InsertScore(pEFile.one);
                _scoreService.InsertScore(pEFile.two);
                _scoreService.InsertScore(pEFile.three);
                _scoreService.InsertScore(pEFile.four);
                _scoreService.InsertScore(pEFile.five);
                _scoreService.InsertScore(pEFile.six);
                _scoreService.InsertScore(pEFile.seven);
                _scoreService.InsertScore(pEFile.eight);
                _scoreService.InsertScore(pEFile.nine);
                _scoreService.InsertScore(pEFile.ten);
                _scoreService.InsertScore(pEFile.eleven);
                _scoreService.InsertScore(pEFile.twelve);
                _scoreService.InsertScore(pEFile.thirteen);
                _scoreService.InsertScore(pEFile.fourteen);
                _scoreService.InsertScore(pEFile.fifteen);
                _scoreService.InsertScore(pEFile.sixteen);
                _scoreService.InsertScore(pEFile.seventeen);
                _scoreService.InsertScore(pEFile.eighteen);
                _scoreService.InsertScore(pEFile.nineteen);
                _scoreService.InsertScore(pEFile.twentyone);
                _scoreService.InsertScore(pEFile.twentytwo);
                _scoreService.InsertScore(pEFile.twentyfour);
                _scoreService.InsertScore(pEFile.policies);
                _scoreService.InsertScore(pEFile.punctuality);
                _scoreService.InsertScore(pEFile.values);
                _scoreService.InsertScore(pEFile.scoreQuality);
                _scoreService.InsertScore(pEFile.scoreOpportunity);
                _scoreService.InsertScore(pEFile.scorePerformance);
                _scoreService.InsertScore(pEFile.scoreSkills);
                _scoreService.InsertScore(pEFile.scoreInterpersonal);
                _scoreService.InsertScore(pEFile.scoreGrowth);
                _scoreService.InsertScore(pEFile.scorePolicies);
                _scoreService.InsertScore(pEFile.scoreCompetences);

                _commentService.InsertComment(pEFile.comment1);
                _commentService.InsertComment(pEFile.comment2);
                _commentService.InsertComment(pEFile.comment3);


                _lm_skillService.InsertLM_Skill(pEFile.supervises);
                _lm_skillService.InsertLM_Skill(pEFile.coordinates);
                _lm_skillService.InsertLM_Skill(pEFile.defines);
                _lm_skillService.InsertLM_Skill(pEFile.supports);
                _lm_skillService.InsertLM_Skill(pEFile.keeps);
                _lm_skillService.InsertLM_Skill(pEFile.generates);
                _lm_skillService.InsertLM_Skill(pEFile.trains);
                _lm_skillService.InsertLM_Skill(pEFile.supportsExperimentation);
                _lm_skillService.InsertLM_Skill(pEFile.evaluates);
                _lm_skillService.InsertLM_Skill(pEFile.faces);
                _lm_skillService.InsertLM_Skill(pEFile.supportsResponsible);
                _lm_skillService.InsertLM_Skill(pEFile.helps);
                _lm_skillService.InsertLM_Skill(pEFile.instills);
                _lm_skillService.InsertLM_Skill(pEFile.sets);
                _lm_skillService.InsertLM_Skill(pEFile.supportsUseful);
                _lm_skillService.InsertLM_Skill(pEFile.welcomes);
                _lm_skillService.InsertLM_Skill(pEFile.setsSpecific);
                
            }
            catch (Exception ex)
            {
                throw;
            }

            return true; 
        }

        [HttpPost]
        public ActionResult UploadFile(UploadFileViewModel uploadVM, HttpPostedFileBase fileUploaded)
        {
            string message = "";
                   
            if (fileUploaded == null || fileUploaded.ContentLength == 0)
            {
                //ViewBag.Error = "Please Select  a excel file<br>";
                return View("UploadFile");
            }
            else
            {
                if(fileUploaded.FileName.EndsWith("xls") || fileUploaded.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Content/" + fileUploaded.FileName);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    //Save the file in the repository   
                    fileUploaded.SaveAs(path);
                    
                    /*
                    //Read Employees by a Excel file *************************************
                    //List<Employee> employees = new List<Employee>();
                    //EmployeeService employeeservice = new EmployeeService();
                    //employees = employeeservice.GetEmployeesFromXLSFile(path);
                   
                    //// insert employees
                    //foreach (Employee employee in employees)
                    //{
                    //    employeeservice.InsertEmployee(employee);
                    //}
                    //********************************************************************
                    */
                    #region comment for now
                    try
                    {
                        // Get selected user
                        var user = _employeeService.GetByID(uploadVM.SelectedEmployee);

                        if(user != null)
                        {
                            var evaluator = _employeeService.GetByID(user.ManagerId);
                            PESComplete file = ReadPerformanceFile(path, user, evaluator);
                            if (file != null)
                            {
                                // Load file into db
                                bool fileSaved = SavePEFile(file);

                                if (!fileSaved)
                                {
                                    // File not saved
                                    TempData["Error"] = "There were some problems saving the file. Please try again later.";
                                }
                                else
                                {
                                    // File saved successfully
                                    TempData["Success"] = "File saved successfully";
                                }

                            }
                            else
                            {
                                TempData["Error"] = "File was not read successfully";

                            }
                        }
                    }
                    finally
                    {
                        //Delete the file from the repository
                        if (System.IO.File.Exists(path))
                        {
                            
                            System.IO.File.Delete(path);
                        }
                    }
                    #endregion

                    return RedirectToAction("UploadFile");
                }
                else
                {
                    //ViewBag.Error = "File type is incorrect<br>";
                    TempData["Error"] = "File is not a valid excel file";

                    return RedirectToAction("UploadFile");
                }

            }
        }
        
        // GET: PerformanceEvaluation/UploadFile
        public ActionResult UploadFile()
        {
            UploadFileViewModel uploadVM = new UploadFileViewModel();

            // Get current user by using the session
            Employee currentUser = new Employee();
            EmployeeService EmployeeService = new EmployeeService();
            currentUser = EmployeeService.GetByEmail((string)Session["UserEmail"]);
            uploadVM.CurrentUser = currentUser;

            List<Employee> listEmployees = new List<Employee>();
            if(currentUser.ProfileId == (int)ProfileUser.Director)
            {
                // Get all employees 
                listEmployees = _employeeService.GetAll();
            }
            else if(currentUser.ProfileId == (int)ProfileUser.Manager)
            {
                // Get employees by manager
                listEmployees = null;
                listEmployees = _employeeService.GetEmployeeByManager(currentUser.EmployeeId);
            }

            uploadVM.ListEmployees = listEmployees;

            return View(uploadVM);
        }

        // GET: PeformanceEvaluation/SearchIformation
        public ActionResult SearchInformation()
        {

            // Read from database
 
            // Get current users by using email in Session
            // Get current user 
            Employee currentUser = new Employee();
            var userEmail = (string)Session["UserEmail"];

            currentUser = _employeeService.GetByEmail(userEmail);

            List<Employee> employees = new List<Employee>();
            if(currentUser.ProfileId == (int)ProfileUser.Director)
            {
                // Get all
                employees = _employeeService.GetAll();
            }
            else if(currentUser.ProfileId == (int)ProfileUser.Manager)
            {
                // Get by manager 
                employees = _employeeService.GetEmployeeByManager(currentUser.EmployeeId);
            }
            else
            {
                // user is resource not allowed, return to home 
                // send error
                return RedirectToAction("ChoosePeriod");
            }

            List<EmployeeManagerViewModel> listEmployeeVM = new List<EmployeeManagerViewModel>();
            foreach(var employee in employees)
            {
                var employeeVM = new EmployeeManagerViewModel();
                employeeVM.employee = employee;
                employeeVM.manager = _employeeService.GetByID(employee.ManagerId);
                
                var listPE = _peService.GetPerformanceEvaluationByUserID(employee.EmployeeId);

                if (listPE != null && listPE.Count > 0)
                {
                    var lastPE = listPE.OrderByDescending(pe => pe.EvaluationPeriod).FirstOrDefault();

                    employeeVM.totalScore = listPE != null ? lastPE.Total : 0;
                }
                else 
                {
                    employeeVM.totalScore = 0;
                }

                listEmployeeVM.Add(employeeVM);
            }

            ViewBag.currentEmployee = currentUser;
            
            return View(listEmployeeVM);
        }
        
        // GET: PeformanceEvaluation/SearchIformation
        public ActionResult Login()
        {
            return View();
        }

        // GET: PerformanceEvaluation/ChoosePeriod
        [HttpGet]
        public ActionResult ChoosePeriod(string employeeEmail, int employeeID)
        {
            // Get user 
            var user = _employeeService.GetByEmail(employeeEmail);
            var userid = _employeeService.GetByID(employeeID);
            // -- Get performance evaluation data
            // Get performance evaluation 
            List<PEs> listUserPE = _peService.GetPerformanceEvaluationByUserID(employeeID);

            //decimal totalEvaluation = _scoreService.GetScoreByPE(userPE.PEId);
            
            List<EmployeeChoosePeriodViewModel> choosePeriodVM = new List<EmployeeChoosePeriodViewModel>();

            if(listUserPE != null && listUserPE.Count > 0)
            {
                foreach(var userPE in listUserPE)
                {
                    var chooseVM = new EmployeeChoosePeriodViewModel
                    {
                        employeeid = userPE.EmployeeId,
                        pesid = userPE.PEId,
                        period = userPE.EvaluationPeriod,
                        totalEvaluation = userPE.Total,
                        totalEnglish = userPE.EnglishScore,
                        totalPerforformance = userPE.PerformanceScore,
                        totalCompetences = userPE.CompeteneceScore
                    };

                    choosePeriodVM.Add(chooseVM);
                }
            }

            ViewBag.UserEmail = employeeEmail;
            ViewBag.UserID = employeeID;
            return View(choosePeriodVM);
        }

        //GET: PerformanceEvaluation/PEVisualization
        public ActionResult PEVisualization(int peID, string email)
        {
            PESComplete peComplete = new PESComplete();
            List<Score> peScores = new List<Score>();
            List<Comment> peComments = new List<Comment>();
            List<LM_Skill> peSkills = new List<LM_Skill>();
            PEs pe = new PEs();
            
            //pe = _peService.
            peScores = _scoreService.GetPEScoresbyPEID(peID); 
            peComments = _commentService.GetCommentByPE(peID);
            peSkills = _lm_skillService.GetSkillsBypeID(peID);

            #region 
            /*
            #region Subtitles
            // Peformance Quality Subtitle 
            peComplete.subtitle1 = PerformanceSection.QualitySubtitle;
            // Performance Opportuniyy Subtitle 
            peComplete.subtitle2 = PerformanceSection.OpportunitySubtitle;
            #endregion

            #region Descriptions
            // Performance Quality Descriptions
            peComplete.description1 = PerformanceSection.AccuracyQualityDescription1;
            peComplete.description2 = PerformanceSection.ThoroughnessQualityDescription2;
            peComplete.description3 = PerformanceSection.ReliabilityQualityDescription3;
            peComplete.description4 = PerformanceSection.ResponsivenessQualityDescription4;
            peComplete.description5 = PerformanceSection.FollowQualityDescription5;
            peComplete.description6 = PerformanceSection.JudgmentQualityDescription6;
            peComplete.subtotalDescQuality = PerformanceSection.SubtotalQualityDescription7;
            
            //Performance Opportunity Description
            peComplete.description7 = PerformanceSection.PriorityOpportunityDescription8;
            peComplete.description8 = PerformanceSection.AmountOpportunityDescription9;
            peComplete.description9 = PerformanceSection.WorkOpportunityDescription10;
            peComplete.subtotalOpportunity = PerformanceSection.SubtotalOpportunityDescription11;
            #endregion

            #region Performance Scores
            // Quality Scores
            
            //Accuracy
            peComplete.one = _scoreService.GetPEScorebyPEIdDescId(pe.PEId, PerformanceSection.AccuracyQualityDescription1.DescriptionId);
            //Thoroughness
            peComplete.two = _scoreService.GetPEScorebyPEIdDescId(pe.PEId, PerformanceSection.ThoroughnessQualityDescription2.DescriptionId);
            //Reliability
            peComplete.three = _scoreService.GetPEScorebyPEIdDescId(pe.PEId, PerformanceSection.ReliabilityQualityDescription3.DescriptionId);
            //Responsiveness
            peComplete.four = _scoreService.GetPEScorebyPEIdDescId(pe.PEId, PerformanceSection.ResponsivenessQualityDescription4.DescriptionId);
            //Follow
            peComplete.five = _scoreService.GetPEScorebyPEIdDescId(pe.PEId, PerformanceSection.FollowQualityDescription5.DescriptionId);
            //Judgment/Decision
            peComplete.six = _scoreService.GetPEScorebyPEIdDescId(pe.PEId, PerformanceSection.JudgmentQualityDescription6.DescriptionId);
            // Quality Subtotal
            peComplete.subtotalDescQuality = _scoreService.GetPEScorebyPEIdDescId(pe.PEId, PerformanceSection.SubtotalQualityDescription7.DescriptionId);

            #endregion
            */
#endregion



            // Get score of 1. Accuracy or Precision
           // var score = _scoreService.GetPEScoreByPEIdDescId(pe.PEId, PerformanceSection.);


            foreach (var score in peScores)
            {
                peComplete.one = score;
            }
            foreach (var comment in peComments)
            {
                peComplete.comment1 = comment;
            }
            foreach (var skill in peSkills)
            {
                peComplete.supervises = skill;
            }

            return View();
        }

        // GET: PerformanceEvaluation/SearchInfoRank
        public ActionResult SearchInfoRank()
        {
            return View();
        }

        public ActionResult LoadPerformanceEvaluationFile(HttpPostedFileBase fileUploaded)
        {
            string errorMessage = "";
            try
            {
               
                bool result = false;

                // Check file was submitted             
                if (fileUploaded != null && fileUploaded.ContentLength > 0)
                {
                    string fname = "";
                    string fullPath = "";

                    //Store file temporary
                    fname = Path.GetFileName(fileUploaded.FileName);
                    fileUploaded.SaveAs(Server.MapPath(Path.Combine("~/App_Data/", fname)));

                    //Get full path of the file 
                    fullPath = Request.MapPath("~/App_Data/" + fname);

                    try
                    {
                        // Load file into database
                        //result = await _userService.LoadUsersFromXLSFile(fullPath);
                    }
                    finally
                    {
                        //Delete temporary file, always delete file already stored 
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }
                }

                //If there are users 
                if (result != false)
                {
                    // File loaded successfuly
                    return View("SaveUsers", result);
                }
                else
                {
                    // Problem loading the file
                    errorMessage = "There was a problem trying to load the file. Please review file and try again.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = "There was a problem trying to load the file. Please review file and try again. " + ex.Message;
            }

            return View();
        }

        private bool SavePerformanceEvaluationFile(string pathFileString)
        {
            //Declare variables

            #region Load XLS file with EPPlus
            //Validate path file 
            //if (!String.IsNullOrEmpty(pathFileString))
            //{
            //    FileInfo file;

            //    try
            //    {
            //        //Creates a new file
            //        file = new FileInfo(pathFileString);

            //        //Excel file valid extensions
            //        var validExtensions = new string[] { ".xls", ".xlsx" };

            //        //Validate extesion of file selected
            //        if (!validExtensions.Contains(file.Extension))
            //        {
            //            throw new System.IO.FileFormatException("Excel file not found in specified path");
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //        throw;
            //    }

            //    // Open and read the XlSX file.
            //    ExcelPackage package = null;
            //    try
            //    {
            //        package = new ExcelPackage(file);
            //    }
            //    catch (System.IO.FileFormatException ex)
            //    {
            //        throw new System.IO.FileFormatException("Unable to read excel file");
            //    }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }

            //    using (package)
            //    {
            //        // Get the work book in the file
            //        ExcelWorkbook workBook = package.Workbook;

            //        //If there is a workbook
            //        if (workBook != null && workBook.Worksheets.Count > 0)
            //        {
            //            // Get the first worksheet
            //            ExcelWorksheet sheet = workBook.Worksheets.First();
            //        }
            //    }
            //}
            #endregion

            return false;
        }
    }
}