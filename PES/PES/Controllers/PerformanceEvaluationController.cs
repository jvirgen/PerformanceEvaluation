using PES.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
using PES.Services;
using PES.ViewModels;
using System.Web.Script.Serialization;

namespace PES.Controllers
{

    [Authorize]
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
            return View();
        }

        public bool ReadPerformanceFile(string path, Employee user, Employee evaluator) 
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook wb = excel.Workbooks.Open(path);
            Excel.Worksheet excelSheet = wb.ActiveSheet;
            try
            {
                // Read
                /*read excel*/

                PESComplete PESc = new PESComplete();

                #region Performance Evaluation - insert
                PESc.pes.Total = excelSheet.Cells[6, 9].Value;
                PESc.pes.EmployeeId = user.EmployeeId;
                PESc.pes.EvaluatorId = evaluator.EmployeeId;
                PESc.pes.EvaluationPeriod = DateTime.Now.Date;
                var status = _statusService.GetStatusByDescription("Incomplete");
                PESc.pes.StatusId = status != null ? status.StatusId : 1;
                PESc.pes.EnglishScore = Convert.ToInt32(excelSheet.Cells[72, 7].Value);
                PESc.pes.PerformanceScore = excelSheet.Cells[37, 9].Value;
                PESc.pes.CompeteneceScore = excelSheet.Cells[73, 9].Value;

                // Insert 
                bool peInserted = _peService.InsertPE(PESc.pes);

                // Look for and get id 
                PESc.pes = _peService.GetPerformanceEvaluationByDateEmail(user.Email, DateTime.Now.Date);
                #endregion

                #region Employee - update
                // data from user
                PESc.empleado.EmployeeId = user.EmployeeId;
                PESc.empleado.Email = user.Email;
                PESc.empleado.ProfileId = user.ProfileId;
                PESc.empleado.ManagerId = user.ManagerId;
                PESc.empleado.HireDate = user.HireDate;
                PESc.empleado.EndDate = user.EndDate;
                // data from excel
                PESc.empleado.FirstName = excelSheet.Cells[3, 3].Value;
                PESc.empleado.LastName = excelSheet.Cells[3, 3].Value;
                PESc.empleado.Position = excelSheet.Cells[4, 3].Value;
                PESc.empleado.Customer = excelSheet.Cells[5, 3].Value;
                PESc.empleado.Project = excelSheet.Cells[6, 3].Value;

                bool employeeInserted = _employeeService.UpdateEmployee(PESc.empleado);
                #endregion

                #region Title - insert
                //PESc.title1.Name = PerformanceSection.PerformanceTitle.Name;
                //PESc.title2.Name = CompetencesSection.CompetenceTitle.Name;
                ////PESc.title1.Name = excelSheet.Cells[19, 2].Value;
                ////PESc.title2.Name = excelSheet.Cells[39, 2].Value;

                //bool title1Inserted = _titleService.InsertTitle(PESc.title1);
                //bool title2Inserted = _titleService.InsertTitle(PESc.title2);
                #endregion

                #region Subtitle - insert
                //PESc.subtitle1.Name = PerformanceSection.QualitySubtitle.Name;
                //PESc.subtitle1.TitleId = PerformanceSection.QualitySubtitle.TitleId;
                //PESc.subtitle2.Name = PerformanceSection.OpportunitySubtitle.Name;
                //PESc.subtitle2.TitleId = PerformanceSection.OpportunitySubtitle.TitleId;
                //PESc.subtitle3.Name = CompetencesSection.SkillsSubtitle.Name;
                //PESc.subtitle3.TitleId = CompetencesSection.SkillsSubtitle.TitleId;
                //PESc.subtitle4.Name = CompetencesSection.InterpersonalSubtitle.Name;
                //PESc.subtitle4.TitleId = CompetencesSection.InterpersonalSubtitle.TitleId;
                //PESc.subtitle5.Name = CompetencesSection.GrowthSubtitle.Name;
                //PESc.subtitle5.TitleId = CompetencesSection.GrowthSubtitle.TitleId;
                //PESc.subtitle6.Name = CompetencesSection.PoliciesSubtitle.Name;
                //PESc.subtitle6.TitleId = CompetencesSection.PoliciesSubtitle.TitleId;
                ////PESc.subtitle1.Name = excelSheet.Cells[23, 2].Value;
                ////PESc.subtitle2.Name = excelSheet.Cells[32, 2].Value;
                ////PESc.subtitle3.Name = excelSheet.Cells[43, 2].Value;
                ////PESc.subtitle4.Name = excelSheet.Cells[52, 2].Value;
                ////PESc.subtitle5.Name = excelSheet.Cells[59, 2].Value;
                ////PESc.subtitle6.Name = excelSheet.Cells[67, 2].Value;

                //bool subtitle1Inserted = _subtitleService.InsertSubtitles(PESc.subtitle1);
                //bool subtitle2Inserted = _subtitleService.InsertSubtitles(PESc.subtitle2);
                //bool subtitle3Inserted = _subtitleService.InsertSubtitles(PESc.subtitle3);
                //bool subtitle4Inserted = _subtitleService.InsertSubtitles(PESc.subtitle4);
                //bool subtitle5Inserted = _subtitleService.InsertSubtitles(PESc.subtitle5);
                //bool subtitle6Inserted = _subtitleService.InsertSubtitles(PESc.subtitle6);
                #endregion

                #region Description - insert
                //PESc.description1.DescriptionText = PerformanceSection.AccuracyQualityDescription1.DescriptionText;
                //PESc.description1.SubtitleId = PerformanceSection.AccuracyQualityDescription1.SubtitleId;
                //PESc.description2.DescriptionText = PerformanceSection.ThoroughnessQualityDescription2.DescriptionText;
                //PESc.description2.SubtitleId = PerformanceSection.ThoroughnessQualityDescription2.SubtitleId;
                //PESc.description3.DescriptionText = PerformanceSection.ReliabilityQualityDescription3.DescriptionText;
                //PESc.description3.SubtitleId = PerformanceSection.ReliabilityQualityDescription3.SubtitleId;
                //PESc.description4.DescriptionText = PerformanceSection.ResponsivenessQualityDescription4.DescriptionText;
                //PESc.description4.SubtitleId = PerformanceSection.ResponsivenessQualityDescription4.SubtitleId;
                //PESc.description5.DescriptionText = PerformanceSection.FollowQualityDescription5.DescriptionText;
                //PESc.description5.SubtitleId = PerformanceSection.FollowQualityDescription5.SubtitleId;
                //PESc.description6.DescriptionText = PerformanceSection.JudgmentQualityDescription6.DescriptionText;
                //PESc.description6.SubtitleId = PerformanceSection.JudgmentQualityDescription6.SubtitleId;
                //PESc.subtotalDescQuality.DescriptionText = PerformanceSection.SubtotalQualityDescription7.DescriptionText;
                //PESc.subtotalDescQuality.SubtitleId = PerformanceSection.SubtotalQualityDescription7.SubtitleId;

                //PESc.description7.DescriptionText = PerformanceSection.PriorityOpportunityDescription8.DescriptionText;
                //PESc.description7.SubtitleId = PerformanceSection.PriorityOpportunityDescription8.SubtitleId;
                //PESc.description8.DescriptionText = PerformanceSection.AmountOpportunityDescription9.DescriptionText;
                //PESc.description8.SubtitleId = PerformanceSection.AmountOpportunityDescription9.SubtitleId;
                //PESc.description9.DescriptionText = PerformanceSection.WorkOpportunityDescription10.DescriptionText;
                //PESc.description9.SubtitleId = PerformanceSection.WorkOpportunityDescription10.SubtitleId;
                //PESc.subtotalOpportunity.DescriptionText = PerformanceSection.SubtotalOpportunityDescription11.DescriptionText;
                //PESc.subtotalOpportunity.SubtitleId = PerformanceSection.SubtotalOpportunityDescription11.SubtitleId;
                //PESc.totalPerformance.DescriptionText = PerformanceSection.TotalPerformanceDescription12.DescriptionText;
                //PESc.totalPerformance.SubtitleId = PerformanceSection.TotalPerformanceDescription12.SubtitleId;

                //PESc.description10.DescriptionText = CompetencesSection.JobSkillDescription13.DescriptionText;
                //PESc.description10.SubtitleId = CompetencesSection.JobSkillDescription13.SubtitleId;
                //PESc.description11.DescriptionText = CompetencesSection.AnalyzesSkillDescription14.DescriptionText;
                //PESc.description11.SubtitleId = CompetencesSection.AnalyzesSkillDescription14.SubtitleId;
                //PESc.description12.DescriptionText = CompetencesSection.FlexibleSkillDescription15.DescriptionText;
                //PESc.description12.SubtitleId = CompetencesSection.FlexibleSkillDescription15.SubtitleId;
                //PESc.description13.DescriptionText = CompetencesSection.PlanningSkillDescription16.DescriptionText;
                //PESc.description13.SubtitleId = CompetencesSection.PlanningSkillDescription16.SubtitleId;
                //PESc.description14.DescriptionText = CompetencesSection.CompetentSkillDescription17.DescriptionText;
                //PESc.description14.SubtitleId = CompetencesSection.CompetentSkillDescription17.SubtitleId;
                //PESc.description15.DescriptionText = CompetencesSection.FollowsSkillDescription18.DescriptionText;
                //PESc.description15.SubtitleId = CompetencesSection.FollowsSkillDescription18.SubtitleId;
                //PESc.subtotalSkills.DescriptionText = CompetencesSection.SubtotalSkillDescription19.DescriptionText;
                //PESc.subtotalSkills.SubtitleId = CompetencesSection.SubtotalSkillDescription19.SubtitleId;

                //PESc.description16.DescriptionText = CompetencesSection.SupervisorInterpersonalDescription20.DescriptionText;
                //PESc.description16.SubtitleId = CompetencesSection.SupervisorInterpersonalDescription20.SubtitleId;
                //PESc.description17.DescriptionText = CompetencesSection.OtherInterpersonalDescription21.DescriptionText;
                //PESc.description17.SubtitleId = CompetencesSection.OtherInterpersonalDescription21.SubtitleId;
                //PESc.description18.DescriptionText = CompetencesSection.ClientInterpersonalDescription22.DescriptionText;
                //PESc.description18.SubtitleId = CompetencesSection.ClientInterpersonalDescription22.SubtitleId;
                //PESc.description19.DescriptionText = CompetencesSection.CommitmentInterpersonalDescription23.DescriptionText;
                //PESc.description19.SubtitleId = CompetencesSection.CommitmentInterpersonalDescription23.SubtitleId;
                //PESc.subtotalInterpersonal.DescriptionText = CompetencesSection.SubtotalInterpersonalDescription24.DescriptionText;
                //PESc.subtotalInterpersonal.SubtitleId = CompetencesSection.SubtotalInterpersonalDescription24.SubtitleId;

                //PESc.description20.DescriptionText = CompetencesSection.ActivelyGrowthDescription25.DescriptionText;
                //PESc.description20.SubtitleId = CompetencesSection.ActivelyGrowthDescription25.SubtitleId;
                //PESc.description21.DescriptionText = CompetencesSection.OpenGrowthDescription26.DescriptionText;
                //PESc.description21.SubtitleId = CompetencesSection.OpenGrowthDescription26.SubtitleId;
                //PESc.description22.DescriptionText = CompetencesSection.InvolvementGrowthDescription27.DescriptionText;
                //PESc.description22.SubtitleId = CompetencesSection.InvolvementGrowthDescription27.SubtitleId;
                //PESc.description23.DescriptionText = CompetencesSection.ChallengesGrowthDescription28.DescriptionText;
                //PESc.description23.SubtitleId = CompetencesSection.ChallengesGrowthDescription28.SubtitleId;
                //PESc.description24.DescriptionText = CompetencesSection.SeeksGrowthDescription29.DescriptionText;
                //PESc.description24.SubtitleId = CompetencesSection.SeeksGrowthDescription29.SubtitleId;
                //PESc.subtotalGrowth.DescriptionText = CompetencesSection.SubtotalGrowthDescription30.DescriptionText;
                //PESc.subtotalGrowth.SubtitleId = CompetencesSection.SubtotalGrowthDescription30.SubtitleId;

                //PESc.descriptionPuctuality.DescriptionText = CompetencesSection.PunctualityPoliciesDescription31.DescriptionText;
                //PESc.descriptionPuctuality.SubtitleId = CompetencesSection.PunctualityPoliciesDescription31.SubtitleId;
                //PESc.descriptionPolicies.DescriptionText = CompetencesSection.PoliciesPoliciesDescription32.DescriptionText;
                //PESc.descriptionPolicies.SubtitleId = CompetencesSection.PoliciesPoliciesDescription32.SubtitleId;
                //PESc.descriptionValues.DescriptionText = CompetencesSection.ValuesPoliciesDescription33.DescriptionText;
                //PESc.descriptionValues.SubtitleId = CompetencesSection.ValuesPoliciesDescription33.SubtitleId;
                //PESc.subtotalPolicies.DescriptionText = CompetencesSection.SubtotalPoliciesDescription34.DescriptionText;
                //PESc.subtotalPolicies.SubtitleId = CompetencesSection.SubtotalPoliciesDescription34.SubtitleId;
                //PESc.EnglishEvaluationDescription.DescriptionText = CompetencesSection.EnglishEvaluationDescription.DescriptionText;
                //PESc.EnglishEvaluationDescription.SubtitleId = CompetencesSection.EnglishEvaluationDescription.SubtitleId;
                //PESc.totalCompetences.DescriptionText = CompetencesSection.TotalCompetencesDescription35.DescriptionText;
                //PESc.totalCompetences.SubtitleId = CompetencesSection.TotalCompetencesDescription35.SubtitleId;

                ////PESc.description1.DescriptionText = excelSheet.Cells[24, 2].Value;
                ////PESc.description2.DescriptionText = excelSheet.Cells[25, 2].Value;
                ////PESc.description3.DescriptionText = excelSheet.Cells[26, 2].Value;
                ////PESc.description4.DescriptionText = excelSheet.Cells[27, 2].Value;
                ////PESc.description5.DescriptionText = excelSheet.Cells[28, 2].Value;
                ////PESc.description6.DescriptionText = excelSheet.Cells[29, 2].Value;
                ////PESc.description7.DescriptionText = excelSheet.Cells[33, 2].Value;
                ////PESc.description8.DescriptionText = excelSheet.Cells[34, 2].Value;
                ////PESc.description9.DescriptionText = excelSheet.Cells[35, 2].Value;
                ////PESc.description10.DescriptionText = excelSheet.Cells[44, 2].Value;
                ////PESc.description11.DescriptionText = excelSheet.Cells[45, 2].Value;
                ////PESc.description12.DescriptionText = excelSheet.Cells[46, 2].Value;
                ////PESc.description13.DescriptionText = excelSheet.Cells[47, 2].Value;
                ////PESc.description14.DescriptionText = excelSheet.Cells[48, 2].Value;
                ////PESc.description15.DescriptionText = excelSheet.Cells[49, 2].Value;
                ////PESc.description16.DescriptionText = excelSheet.Cells[53, 2].Value;
                ////PESc.description17.DescriptionText = excelSheet.Cells[54, 2].Value;
                ////PESc.description18.DescriptionText = excelSheet.Cells[55, 2].Value;
                ////PESc.description19.DescriptionText = excelSheet.Cells[56, 2].Value;
                ////PESc.description21.DescriptionText = excelSheet.Cells[61, 2].Value;
                ////PESc.description22.DescriptionText = excelSheet.Cells[62, 2].Value;
                ////PESc.description24.DescriptionText = excelSheet.Cells[64, 2].Value;
                ////PESc.descriptionPuctuality.DescriptionText = excelSheet.Cells[68, 2].Value;
                ////PESc.descriptionPolicies.DescriptionText = excelSheet.Cells[69, 2].Value;
                ////PESc.descriptionValues.DescriptionText = excelSheet.Cells[70, 2].Value;
                ////PESc.subtotalQuality.DescriptionText = excelSheet.Cells[30, 2].Value;
                ////PESc.subtotalOpportunity.DescriptionText = excelSheet.Cells[36, 2].Value;
                ////PESc.totalPerformance.DescriptionText = excelSheet.Cells[37, 2].Value;
                ////PESc.subtotalSkills.DescriptionText = excelSheet.Cells[50, 2].Value;
                ////PESc.subtotalInterpersonal.DescriptionText = excelSheet.Cells[57, 2].Value;
                ////PESc.subtotalGrowth.DescriptionText = excelSheet.Cells[65, 2].Value;
                ////PESc.subtotalPolicies.DescriptionText = excelSheet.Cells[71, 2].Value;
                ////PESc.totalCompetences.DescriptionText = excelSheet.Cells[73, 2].Value;

                //bool description1Inserted = _descriptionService.InsertDescription(PESc.description1);
                //bool description2Inserted = _descriptionService.InsertDescription(PESc.description2);
                //bool description3Inserted = _descriptionService.InsertDescription(PESc.description3);
                //bool description4Inserted = _descriptionService.InsertDescription(PESc.description4);
                //bool description5Inserted = _descriptionService.InsertDescription(PESc.description5);
                //bool description6Inserted = _descriptionService.InsertDescription(PESc.description6);
                //bool descriptionSubtotalQualityInserted = _descriptionService.InsertDescription(PESc.subtotalDescQuality);
                //bool description7Inserted = _descriptionService.InsertDescription(PESc.description7);
                //bool description8Inserted = _descriptionService.InsertDescription(PESc.description8);
                //bool description9Inserted = _descriptionService.InsertDescription(PESc.description9);
                //bool descriptionSubtotalOpportunityInserted = _descriptionService.InsertDescription(PESc.subtotalOpportunity);
                //bool descriptionTotalPerformanceInserted = _descriptionService.InsertDescription(PESc.totalPerformance);
                //bool description10Inserted = _descriptionService.InsertDescription(PESc.description10);
                //bool description11Inserted = _descriptionService.InsertDescription(PESc.description11);
                //bool description12Inserted = _descriptionService.InsertDescription(PESc.description12);
                //bool description13Inserted = _descriptionService.InsertDescription(PESc.description13);
                //bool description14Inserted = _descriptionService.InsertDescription(PESc.description14);
                //bool description15Inserted = _descriptionService.InsertDescription(PESc.description15);
                //bool descriptionSubtotalSkillsInserted = _descriptionService.InsertDescription(PESc.subtotalSkills);
                //bool description16Inserted = _descriptionService.InsertDescription(PESc.description16);
                //bool description17Inserted = _descriptionService.InsertDescription(PESc.description17);
                //bool description18Inserted = _descriptionService.InsertDescription(PESc.description18);
                //bool description19Inserted = _descriptionService.InsertDescription(PESc.description19);
                //bool descriptionSubtotalInterpersonalInserted = _descriptionService.InsertDescription(PESc.subtotalInterpersonal);
                //bool description20Inserted = _descriptionService.InsertDescription(PESc.description20);
                //bool description21Inserted = _descriptionService.InsertDescription(PESc.description21);
                //bool description22Inserted = _descriptionService.InsertDescription(PESc.description22);
                //bool description23Inserted = _descriptionService.InsertDescription(PESc.description23);
                //bool description24Inserted = _descriptionService.InsertDescription(PESc.description24);
                //bool descriptionSubtotalGrowthInserted = _descriptionService.InsertDescription(PESc.subtotalGrowth);
                //bool descriptionPuctuallityInserted = _descriptionService.InsertDescription(PESc.descriptionPuctuality);
                //bool descriptionPoliciesInserted = _descriptionService.InsertDescription(PESc.descriptionPolicies);
                //bool descriptionValuesInserted = _descriptionService.InsertDescription(PESc.descriptionValues);
                //bool descriptionSubtotalPoliciesInserted = _descriptionService.InsertDescription(PESc.subtotalPolicies);
                //bool descriptionEnglishEvalationInserted = _descriptionService.InsertDescription(PESc.EnglishEvaluationDescription);
                //bool descriptionTotalCompetencesInserted = _descriptionService.InsertDescription(PESc.totalCompetences);
                #endregion

                #region ScoreEmployee - insert
                PESc.one.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[24, 6].Value);
                PESc.one.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.AccuracyQualityDescription1.DescriptionText).DescriptionId;
                PESc.two.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[25, 6].Value);
                PESc.two.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.ThoroughnessQualityDescription2.DescriptionText).DescriptionId;
                PESc.three.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[26, 6].Value);
                PESc.three.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.ReliabilityQualityDescription3.DescriptionText).DescriptionId;
                PESc.four.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[27, 6].Value);
                PESc.four.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.ResponsivenessQualityDescription4.DescriptionText).DescriptionId;
                PESc.five.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[28, 6].Value);
                PESc.five.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.FollowQualityDescription5.DescriptionText).DescriptionId;
                PESc.six.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[29, 6].Value);
                PESc.six.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.JudgmentQualityDescription6.DescriptionText).DescriptionId;
                PESc.scoreQuality.ScoreEmployee = 0;
                PESc.scoreQuality.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.SubtotalQualityDescription7.DescriptionText).DescriptionId;

                PESc.seven.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[33, 6].Value);
                PESc.seven.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.PriorityOpportunityDescription8.DescriptionText).DescriptionId;
                PESc.eight.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[34, 6].Value);
                PESc.eight.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.AmountOpportunityDescription9.DescriptionText).DescriptionId;
                PESc.nine.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[35, 6].Value);
                PESc.nine.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.WorkOpportunityDescription10.DescriptionText).DescriptionId;
                PESc.scoreOpportunity.ScoreEmployee = 0;
                PESc.scoreOpportunity.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.SubtotalOpportunityDescription11.DescriptionText).DescriptionId;
                PESc.scorePerformance.ScoreEmployee = 0;
                PESc.scorePerformance.DescriptionId = _descriptionService.GetDescriptionByText(PerformanceSection.TotalPerformanceDescription12.DescriptionText).DescriptionId;

                PESc.ten.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[44, 6].Value);
                PESc.ten.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.JobSkillDescription13.DescriptionText).DescriptionId;
                PESc.eleven.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[45, 6].Value);
                PESc.eleven.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.AnalyzesSkillDescription14.DescriptionText).DescriptionId;
                PESc.twelve.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[46, 6].Value);
                PESc.twelve.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.FlexibleSkillDescription15.DescriptionText).DescriptionId;
                PESc.thirteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[47, 6].Value);
                PESc.thirteen.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.PlanningSkillDescription16.DescriptionText).DescriptionId;
                PESc.fourteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[48, 6].Value);
                PESc.fourteen.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.CompetentSkillDescription17.DescriptionText).DescriptionId;
                PESc.fifteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[49, 6].Value);
                PESc.fifteen.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.FollowsSkillDescription18.DescriptionText).DescriptionId;
                PESc.scoreSkills.ScoreEmployee = 0;
                PESc.scoreSkills.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.SubtotalSkillDescription19.DescriptionText).DescriptionId;

                PESc.sixteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[53, 6].Value);
                PESc.sixteen.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.SupervisorInterpersonalDescription20.DescriptionText).DescriptionId;
                PESc.seventeen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[54, 6].Value);
                PESc.seventeen.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.OtherInterpersonalDescription21.DescriptionText).DescriptionId;
                PESc.eighteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[55, 6].Value);
                PESc.eighteen.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.ClientInterpersonalDescription22.DescriptionText).DescriptionId;
                PESc.nineteen.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[56, 6].Value);
                PESc.nineteen.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.CommitmentInterpersonalDescription23.DescriptionText).DescriptionId;
                PESc.scoreInterpersonal.ScoreEmployee = 0;
                PESc.scoreInterpersonal.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.SubtotalInterpersonalDescription24.DescriptionText).DescriptionId;

                PESc.twenty.ScoreEmployee = 0;
                PESc.twenty.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.ActivelyGrowthDescription25.DescriptionText).DescriptionId;
                PESc.twentyone.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[61, 6].Value);
                PESc.twentyone.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.OpenGrowthDescription26.DescriptionText).DescriptionId;
                PESc.twentytwo.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[62, 6].Value);
                PESc.twentytwo.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.InvolvementGrowthDescription27.DescriptionText).DescriptionId;
                PESc.twentythree.ScoreEmployee = 0;
                PESc.twentythree.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.ChallengesGrowthDescription28.DescriptionText).DescriptionId;
                PESc.twentyfour.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[64, 6].Value);
                PESc.twentyfour.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.SeeksGrowthDescription29.DescriptionText).DescriptionId;
                PESc.scoreGrowth.ScoreEmployee = 0;
                PESc.scoreGrowth.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.SubtotalGrowthDescription30.DescriptionText).DescriptionId;

                PESc.punctuality.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[68, 6].Value);
                PESc.punctuality.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.PunctualityPoliciesDescription31.DescriptionText).DescriptionId;
                PESc.policies.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[69, 6].Value);
                PESc.policies.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.PoliciesPoliciesDescription32.DescriptionText).DescriptionId;
                PESc.values.ScoreEmployee = Convert.ToInt32(excelSheet.Cells[70, 6].Value);
                PESc.values.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.ValuesPoliciesDescription33.DescriptionText).DescriptionId;
                PESc.scorePolicies.ScoreEmployee = 0;
                PESc.scorePolicies.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.SubtotalPoliciesDescription34.DescriptionText).DescriptionId;
                PESc.scoreCompetences.ScoreEmployee = 0;
                PESc.scoreCompetences.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.TotalCompetencesDescription36.DescriptionText).DescriptionId;
                PESc.englishScore.ScoreEmployee = 0;
                PESc.englishScore.DescriptionId = _descriptionService.GetDescriptionByText(CompetencesSection.EnglishEvaluationDescription.DescriptionText).DescriptionId;
                #endregion

                #region ScoreEvaluator - insert
                PESc.one.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[24, 7].Value);
                PESc.one.PEId = PESc.pes.PEId;
                PESc.two.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[25, 7].Value);
                PESc.two.PEId = PESc.pes.PEId;
                PESc.three.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[26, 7].Value);
                PESc.three.PEId = PESc.pes.PEId;
                PESc.four.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[27, 7].Value);
                PESc.four.PEId = PESc.pes.PEId;
                PESc.five.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[28, 7].Value);
                PESc.five.PEId = PESc.pes.PEId;
                PESc.six.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[29, 7].Value);
                PESc.six.PEId = PESc.pes.PEId;
                PESc.scoreQuality.ScoreEvaluator = 0;
                PESc.scoreQuality.PEId = PESc.pes.PEId;

                PESc.seven.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[33, 7].Value);
                PESc.seven.PEId = PESc.pes.PEId;
                PESc.eight.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[34, 7].Value);
                PESc.eight.PEId = PESc.pes.PEId;
                PESc.nine.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[35, 7].Value);
                PESc.nine.PEId = PESc.pes.PEId;
                PESc.scoreOpportunity.ScoreEvaluator = 0;
                PESc.scoreOpportunity.PEId = PESc.pes.PEId;
                PESc.scorePerformance.ScoreEvaluator = 0;
                PESc.scorePerformance.PEId = PESc.pes.PEId;

                PESc.ten.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[44, 7].Value);
                PESc.ten.PEId = PESc.pes.PEId;
                PESc.eleven.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[45, 7].Value);
                PESc.eleven.PEId = PESc.pes.PEId;
                PESc.twelve.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[46, 7].Value);
                PESc.twelve.PEId = PESc.pes.PEId;
                PESc.thirteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[47, 7].Value);
                PESc.thirteen.PEId = PESc.pes.PEId;
                PESc.fourteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[48, 7].Value);
                PESc.fourteen.PEId = PESc.pes.PEId;
                PESc.fifteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[49, 7].Value);
                PESc.fifteen.PEId = PESc.pes.PEId;
                PESc.scoreSkills.ScoreEvaluator = 0;
                PESc.scoreSkills.PEId = PESc.pes.PEId;

                PESc.sixteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[53, 7].Value);
                PESc.sixteen.PEId = PESc.pes.PEId;
                PESc.seventeen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[54, 7].Value);
                PESc.seventeen.PEId = PESc.pes.PEId;
                PESc.eighteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[55, 7].Value);
                PESc.eighteen.PEId = PESc.pes.PEId;
                PESc.nineteen.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[56, 7].Value);
                PESc.nineteen.PEId = PESc.pes.PEId;
                PESc.scoreInterpersonal.ScoreEvaluator = 0;
                PESc.scoreInterpersonal.PEId = PESc.pes.PEId;

                PESc.twenty.ScoreEvaluator = 0;
                PESc.twenty.PEId = PESc.pes.PEId;
                PESc.twentyone.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[61, 7].Value);
                PESc.twentyone.PEId = PESc.pes.PEId;
                PESc.twentytwo.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[62, 7].Value);
                PESc.twentytwo.PEId = PESc.pes.PEId;
                PESc.twentythree.ScoreEvaluator = 0;
                PESc.twentythree.PEId = PESc.pes.PEId;
                PESc.twentyfour.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[64, 7].Value);
                PESc.twentyfour.PEId = PESc.pes.PEId;
                PESc.scoreGrowth.ScoreEvaluator = 0;
                PESc.scoreGrowth.PEId = PESc.pes.PEId;

                PESc.punctuality.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[68, 7].Value);
                PESc.punctuality.PEId = PESc.pes.PEId;
                PESc.policies.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[69, 7].Value);
                PESc.policies.PEId = PESc.pes.PEId;
                PESc.values.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[70, 7].Value);
                PESc.values.PEId = PESc.pes.PEId;
                PESc.scorePolicies.ScoreEvaluator = 0;
                PESc.scorePolicies.PEId = PESc.pes.PEId;
                PESc.scoreCompetences.ScoreEvaluator = 0;
                PESc.scoreCompetences.PEId = PESc.pes.PEId;
                PESc.englishScore.ScoreEvaluator = Convert.ToInt32(excelSheet.Cells[72, 7].Value);
                PESc.englishScore.PEId = PESc.pes.PEId;
                #endregion

                #region Comments - insert
                PESc.one.Comments = excelSheet.Cells[24, 8].Value;
                PESc.two.Comments = excelSheet.Cells[25, 8].Value;
                PESc.three.Comments = excelSheet.Cells[26, 8].Value;
                PESc.four.Comments = excelSheet.Cells[27, 8].Value;
                PESc.five.Comments = excelSheet.Cells[28, 8].Value;
                PESc.six.Comments = excelSheet.Cells[29, 8].Value;
                PESc.scoreQuality.Comments = "";

                PESc.seven.Comments = excelSheet.Cells[33, 8].Value;
                PESc.eight.Comments = excelSheet.Cells[34, 8].Value;
                PESc.nine.Comments = excelSheet.Cells[35, 8].Value;
                PESc.scoreOpportunity.Comments = "";
                PESc.scorePerformance.Comments = "";

                PESc.ten.Comments = excelSheet.Cells[44, 8].Value;
                PESc.eleven.Comments = excelSheet.Cells[45, 8].Value;
                PESc.twelve.Comments = excelSheet.Cells[46, 8].Value;
                PESc.thirteen.Comments = excelSheet.Cells[47, 8].Value;
                PESc.fourteen.Comments = excelSheet.Cells[48, 8].Value;
                PESc.fifteen.Comments = excelSheet.Cells[49, 8].Value;
                PESc.scoreSkills.Comments = "";

                PESc.sixteen.Comments = excelSheet.Cells[53, 8].Value;
                PESc.seventeen.Comments = excelSheet.Cells[54, 8].Value;
                PESc.eighteen.Comments = excelSheet.Cells[55, 8].Value;
                PESc.nineteen.Comments = excelSheet.Cells[56, 8].Value;
                PESc.scoreInterpersonal.Comments = "";

                PESc.twenty.Comments = "";
                PESc.twentyone.Comments = excelSheet.Cells[61, 8].Value;
                PESc.twentytwo.Comments = excelSheet.Cells[62, 8].Value;
                PESc.twentythree.Comments = "";
                PESc.twentyfour.Comments = excelSheet.Cells[64, 8].Value;
                PESc.scoreGrowth.Comments = "";

                PESc.punctuality.Comments = excelSheet.Cells[68, 8].Value;
                PESc.policies.Comments = excelSheet.Cells[69, 8].Value;
                PESc.values.Comments = excelSheet.Cells[70, 8].Value;
                PESc.scorePolicies.Comments = "";
                PESc.scoreCompetences.Comments = "";
                PESc.englishScore.Comments = excelSheet.Cells[72, 8].Value;
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

                PESc.twenty.Calculation = 0;
                PESc.twentyone.Calculation = excelSheet.Cells[61, 9].Value;
                PESc.twentytwo.Calculation = excelSheet.Cells[62, 9].Value;
                PESc.twentythree.Calculation = 0;
                PESc.twentyfour.Calculation = excelSheet.Cells[64, 9].Value;

                PESc.punctuality.Calculation = excelSheet.Cells[68, 9].Value;
                PESc.policies.Calculation = excelSheet.Cells[69, 9].Value;
                PESc.values.Calculation = excelSheet.Cells[70, 9].Value;
                #endregion

                #region Totals and subtotals - insert
                PESc.scoreQuality.Calculation = excelSheet.Cells[30, 9].Value;
                PESc.scoreOpportunity.Calculation = excelSheet.Cells[36, 9].Value;
                PESc.scorePerformance.Calculation = PESc.pes.PerformanceScore;

                PESc.scoreSkills.Calculation = excelSheet.Cells[50, 9].Value;
                PESc.scoreInterpersonal.Calculation = excelSheet.Cells[57, 9].Value;
                PESc.scoreGrowth.Calculation = excelSheet.Cells[65, 9].Value;
                PESc.scorePolicies.Calculation = excelSheet.Cells[71, 9].Value;
                PESc.scoreCompetences.Calculation = PESc.pes.CompeteneceScore;
                PESc.englishScore.Calculation = excelSheet.Cells[72, 9].Value;

                bool scoreOneInserted = _scoreService.InsertScore(PESc.one);
                bool scoreTwoInserted = _scoreService.InsertScore(PESc.two);
                bool scoreThreeInserted = _scoreService.InsertScore(PESc.three);
                bool scoreFourInserted = _scoreService.InsertScore(PESc.four);
                bool scoreFiveInserted = _scoreService.InsertScore(PESc.five);
                bool scoreSixInserted = _scoreService.InsertScore(PESc.six);
                bool scoreScoreQualityInserted = _scoreService.InsertScore(PESc.scoreQuality);

                bool scoreSevenInserted = _scoreService.InsertScore(PESc.seven);
                bool scoreEightInserted = _scoreService.InsertScore(PESc.eight);
                bool scoreNineInserted = _scoreService.InsertScore(PESc.nine);
                bool scoreScoreOpportunityInserted = _scoreService.InsertScore(PESc.scoreOpportunity);
                bool scoreScorePerformanceInserted = _scoreService.InsertScore(PESc.scorePerformance);

                bool scoreTenInserted = _scoreService.InsertScore(PESc.ten);
                bool scoreElevenInserted = _scoreService.InsertScore(PESc.eleven);
                bool scoreTwelveInserted = _scoreService.InsertScore(PESc.twelve);
                bool scoreThirteenInserted = _scoreService.InsertScore(PESc.thirteen);
                bool scoreFourteenInserted = _scoreService.InsertScore(PESc.fourteen);
                bool scoreFifteenInserted = _scoreService.InsertScore(PESc.fifteen);
                bool scoreScoreSkillsInserted = _scoreService.InsertScore(PESc.scoreSkills);

                bool scoreSixteenInserted = _scoreService.InsertScore(PESc.sixteen);
                bool scoreSeventeenInserted = _scoreService.InsertScore(PESc.seventeen);
                bool scoreEighteenInserted = _scoreService.InsertScore(PESc.eighteen);
                bool scoreNineteenInserted = _scoreService.InsertScore(PESc.nineteen);
                bool scoreScoreInterpersonalInserted = _scoreService.InsertScore(PESc.scoreInterpersonal);

                bool scoreTwentyInserted = _scoreService.InsertScore(PESc.twenty);
                bool scoreTwentyoneInserted = _scoreService.InsertScore(PESc.twentyone);
                bool scoreTwentytwoInserted = _scoreService.InsertScore(PESc.twentytwo);
                bool scoreTwentythreeInserted = _scoreService.InsertScore(PESc.twentythree);
                bool scoreTwentyfourInserted = _scoreService.InsertScore(PESc.twentyfour);
                bool scoreScoreGrowthInserted = _scoreService.InsertScore(PESc.scoreGrowth);

                bool scorePunctuallityInserted = _scoreService.InsertScore(PESc.punctuality);
                bool scorePoliciesInserted = _scoreService.InsertScore(PESc.policies);
                bool scoreValuesInserted = _scoreService.InsertScore(PESc.values);
                bool scoreScorePolicesInserted = _scoreService.InsertScore(PESc.scorePolicies);
                bool scoreScoreCompetencesInserted = _scoreService.InsertScore(PESc.scoreCompetences);
                bool scoreEnglishEvaluationInserted = _scoreService.InsertScore(PESc.englishScore);
                #endregion

                #region Trainning Comment - insert
                string comment1 = excelSheet.Cells[79, 2].Value;
                PESc.comment.TrainningEmployee = (excelSheet.Cells[78, 2].Value + (string.IsNullOrEmpty(comment1) == true ? "" : ", ") + comment1);
                //PESc.comment2.TrainningEmployee = excelSheet.Cells[79, 2].Value;
                PESc.comment.TrainningEvaluator = excelSheet.Cells[80, 2].Value;
                PESc.comment.PEId = PESc.pes.PEId;
                #endregion

                #region Acknowledge Comment - insert
                string comment2 = excelSheet.Cells[83, 2].Value;
                PESc.comment.AcknowledgeEvaluator = (excelSheet.Cells[82, 2].Value + (string.IsNullOrEmpty(comment2) == true ? "" : ", ") + comment2);
                //PESc.comment2.AcknowledgeEvaluator = excelSheet.Cells[83, 2].Value;
                #endregion

                #region Comments and recommendations - insert
                string comment3 = excelSheet.Cells[86, 2].Value;
                string comment4 = excelSheet.Cells[87, 2].Value;
                string comment5 = excelSheet.Cells[89, 2].Value;
                string comment6 = excelSheet.Cells[90, 2].Value;
                PESc.comment.CommRecommEmployee = (excelSheet.Cells[85, 2].Value + (string.IsNullOrEmpty(comment3) == true ? "" : ", ") + comment3 + (string.IsNullOrEmpty(comment4) == true ? "" : ", ") + comment4);
                //PESc.comment2.CommRecommEmployee = excelSheet.Cells[86, 2].Value;
                //PESc.comment3.CommRecommEmployee = excelSheet.Cells[87, 2].Value;
                PESc.comment.CommRecommEvaluator = (excelSheet.Cells[88, 2].Value + (string.IsNullOrEmpty(comment5) == true ? "" : ", ") + comment5 + (string.IsNullOrEmpty(comment6) == true ? "" : ", ") + comment6);
                //PESc.comment2.CommRecommEvaluator = excelSheet.Cells[89, 2].Value;
                //PESc.comment3.CommRecommEvaluator = excelSheet.Cells[90, 2].Value;

                bool commentInserted = _commentService.InsertComment(PESc.comment);
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

                //PESc.skill1.Description = SkillsSection.SupervisesSkill1.Description;
                //PESc.skill2.Description = SkillsSection.CoordinatesSkill2.Description;
                //PESc.skill3.Description = SkillsSection.DefinesSkill3.Description;
                //PESc.skill4.Description = SkillsSection.SupportsSkill4.Description;
                //PESc.skill5.Description = SkillsSection.KeepsSkill5.Description;
                //PESc.skill6.Description = SkillsSection.GeneratesSkill6.Description;
                //PESc.skill7.Description = SkillsSection.TrainsSkill7.Description;
                //PESc.skill8.Description = SkillsSection.SupportsSkill8.Description;
                //PESc.skill9.Description = SkillsSection.EvaluatesSkill9.Description;
                //PESc.skill10.Description = SkillsSection.FacesSkill10.Description;
                //PESc.skill11.Description = SkillsSection.SupportsSkill11.Description;
                //PESc.skill12.Description = SkillsSection.HelpsSkill12.Description;
                //PESc.skill13.Description = SkillsSection.InstillsSkill13.Description;
                //PESc.skill14.Description = SkillsSection.SetsSkill14.Description;
                //PESc.skill15.Description = SkillsSection.SupportsSkill15.Description;
                //PESc.skill16.Description = SkillsSection.WelcomesSkill16.Description;
                //PESc.skill17.Description = SkillsSection.SetsSkill17.Description;

                //bool skill1Inserted = _skillService.InsertSkill(PESc.skill1);
                //bool skill2Inserted = _skillService.InsertSkill(PESc.skill2);
                //bool skill3Inserted = _skillService.InsertSkill(PESc.skill3);
                //bool skill4Inserted = _skillService.InsertSkill(PESc.skill4);
                //bool skill5Inserted = _skillService.InsertSkill(PESc.skill5);
                //bool skill6Inserted = _skillService.InsertSkill(PESc.skill6);
                //bool skill7Inserted = _skillService.InsertSkill(PESc.skill7);
                //bool skill8Inserted = _skillService.InsertSkill(PESc.skill8);
                //bool skill9Inserted = _skillService.InsertSkill(PESc.skill9);
                //bool skill10Inserted = _skillService.InsertSkill(PESc.skill10);
                //bool skill11Inserted = _skillService.InsertSkill(PESc.skill11);
                //bool skill12Inserted = _skillService.InsertSkill(PESc.skill12);
                //bool skill13Inserted = _skillService.InsertSkill(PESc.skill13);
                //bool skill14Inserted = _skillService.InsertSkill(PESc.skill14);
                //bool skill15Inserted = _skillService.InsertSkill(PESc.skill15);
                //bool skill16Inserted = _skillService.InsertSkill(PESc.skill16);
                //bool skill17Inserted = _skillService.InsertSkill(PESc.skill17);
                #endregion

                #region skill check employee - insert
                PESc.supervises.CheckEmployee = excelSheet.Cells[96, 6].Value;
                PESc.supervises.SkillId = SkillsSection.SupervisesSkill1.SkillId;
                PESc.coordinates.CheckEmployee = excelSheet.Cells[97, 6].Value;
                PESc.coordinates.SkillId = SkillsSection.CoordinatesSkill2.SkillId;
                PESc.defines.CheckEmployee = excelSheet.Cells[98, 6].Value;
                PESc.defines.SkillId = SkillsSection.DefinesSkill3.SkillId;
                PESc.supports.CheckEmployee = excelSheet.Cells[99, 6].Value;
                PESc.supports.SkillId = SkillsSection.SupportsSkill4.SkillId;
                PESc.keeps.CheckEmployee = excelSheet.Cells[100, 6].Value;
                PESc.keeps.SkillId = SkillsSection.KeepsSkill5.SkillId;
                PESc.generates.CheckEmployee = excelSheet.Cells[101, 6].Value;
                PESc.generates.SkillId = SkillsSection.GeneratesSkill6.SkillId;
                PESc.trains.CheckEmployee = excelSheet.Cells[102, 6].Value;
                PESc.trains.SkillId = SkillsSection.TrainsSkill7.SkillId;
                PESc.supportsExperimentation.CheckEmployee = excelSheet.Cells[103, 6].Value;
                PESc.supportsExperimentation.SkillId = SkillsSection.SupportsSkill8.SkillId;
                PESc.evaluates.CheckEmployee = excelSheet.Cells[104, 6].Value;
                PESc.evaluates.SkillId = SkillsSection.EvaluatesSkill9.SkillId;
                PESc.faces.CheckEmployee = excelSheet.Cells[105, 6].Value;
                PESc.faces.SkillId = SkillsSection.FacesSkill10.SkillId;
                PESc.supportsResponsible.CheckEmployee = excelSheet.Cells[106, 6].Value;
                PESc.supportsResponsible.SkillId = SkillsSection.SupportsSkill11.SkillId;
                PESc.helps.CheckEmployee = excelSheet.Cells[107, 6].Value;
                PESc.helps.SkillId = SkillsSection.HelpsSkill12.SkillId;
                PESc.instills.CheckEmployee = excelSheet.Cells[108, 6].Value;
                PESc.instills.SkillId = SkillsSection.InstillsSkill13.SkillId;
                PESc.sets.CheckEmployee = excelSheet.Cells[109, 6].Value;
                PESc.sets.SkillId = SkillsSection.SetsSkill14.SkillId;
                PESc.supportsUseful.CheckEmployee = excelSheet.Cells[110, 6].Value;
                PESc.supportsUseful.SkillId = SkillsSection.SupportsSkill15.SkillId;
                PESc.welcomes.CheckEmployee = excelSheet.Cells[111, 6].Value;
                PESc.welcomes.SkillId = SkillsSection.WelcomesSkill16.SkillId;
                PESc.setsSpecific.CheckEmployee = excelSheet.Cells[112, 6].Value;
                PESc.setsSpecific.SkillId = SkillsSection.SetsSkill17.SkillId;
                #endregion

                #region skill check evaluator - insert
                PESc.supervises.CheckEvaluator = excelSheet.Cells[96, 7].Value;
                PESc.supervises.PEId = PESc.pes.PEId;
                PESc.coordinates.CheckEvaluator = excelSheet.Cells[97, 7].Value;
                PESc.coordinates.PEId = PESc.pes.PEId;
                PESc.defines.CheckEvaluator = excelSheet.Cells[98, 7].Value;
                PESc.defines.PEId = PESc.pes.PEId;
                PESc.supports.CheckEvaluator = excelSheet.Cells[99, 7].Value;
                PESc.supports.PEId = PESc.pes.PEId;
                PESc.keeps.CheckEvaluator = excelSheet.Cells[100, 7].Value;
                PESc.keeps.PEId = PESc.pes.PEId;
                PESc.generates.CheckEvaluator = excelSheet.Cells[101, 7].Value;
                PESc.generates.PEId = PESc.pes.PEId;
                PESc.trains.CheckEvaluator = excelSheet.Cells[102, 7].Value;
                PESc.trains.PEId = PESc.pes.PEId;
                PESc.supportsExperimentation.CheckEvaluator = excelSheet.Cells[103, 7].Value;
                PESc.supportsExperimentation.PEId = PESc.pes.PEId;
                PESc.evaluates.CheckEvaluator = excelSheet.Cells[104, 7].Value;
                PESc.evaluates.PEId = PESc.pes.PEId;
                PESc.faces.CheckEvaluator = excelSheet.Cells[105, 7].Value;
                PESc.faces.PEId = PESc.pes.PEId;
                PESc.supportsResponsible.CheckEvaluator = excelSheet.Cells[106, 7].Value;
                PESc.supportsResponsible.PEId = PESc.pes.PEId;
                PESc.helps.CheckEvaluator = excelSheet.Cells[107, 7].Value;
                PESc.helps.PEId = PESc.pes.PEId;
                PESc.instills.CheckEvaluator = excelSheet.Cells[108, 7].Value;
                PESc.instills.PEId = PESc.pes.PEId;
                PESc.sets.CheckEvaluator = excelSheet.Cells[109, 7].Value;
                PESc.sets.PEId = PESc.pes.PEId;
                PESc.supportsUseful.CheckEvaluator = excelSheet.Cells[110, 7].Value;
                PESc.supportsUseful.PEId = PESc.pes.PEId;
                PESc.welcomes.CheckEvaluator = excelSheet.Cells[111, 7].Value;
                PESc.welcomes.PEId = PESc.pes.PEId;
                PESc.setsSpecific.CheckEvaluator = excelSheet.Cells[112, 7].Value;
                PESc.setsSpecific.PEId = PESc.pes.PEId;

                bool SupervicesSkill = _lm_skillService.InsertLM_Skill(PESc.supervises);
                bool CoordinatesSkill = _lm_skillService.InsertLM_Skill(PESc.coordinates);
                bool DefinesSkill = _lm_skillService.InsertLM_Skill(PESc.defines);
                bool SupportsSkill = _lm_skillService.InsertLM_Skill(PESc.supports);
                bool KeepsSkill = _lm_skillService.InsertLM_Skill(PESc.keeps);
                bool GeneratesSkill = _lm_skillService.InsertLM_Skill(PESc.generates);
                bool TrainsSkill = _lm_skillService.InsertLM_Skill(PESc.trains);
                bool SupportsExperimentationSkill = _lm_skillService.InsertLM_Skill(PESc.supportsExperimentation);
                bool EvaluatesSkill = _lm_skillService.InsertLM_Skill(PESc.evaluates);
                bool FacesSkill = _lm_skillService.InsertLM_Skill(PESc.faces);
                bool SupportsResponsibleSkill = _lm_skillService.InsertLM_Skill(PESc.supportsResponsible);
                bool HelpsSkill = _lm_skillService.InsertLM_Skill(PESc.helps);
                bool InstillsSkill = _lm_skillService.InsertLM_Skill(PESc.instills);
                bool SetsSkill = _lm_skillService.InsertLM_Skill(PESc.sets);
                bool SupportsUsefulSkill = _lm_skillService.InsertLM_Skill(PESc.supportsUseful);
                bool WelcomesSkill = _lm_skillService.InsertLM_Skill(PESc.welcomes);
                bool SetsSpecificSkill = _lm_skillService.InsertLM_Skill(PESc.setsSpecific);
                #endregion

                excel.Workbooks.Close();
                excel.Quit();
                return true;
            }
            catch (Exception ex)
            {
                excel.Workbooks.Close();
                excel.Quit();
                throw;
            }
            
        }

        [HttpPost]
        public ActionResult UploadFile(UploadFileViewModel uploadVM, HttpPostedFileBase fileUploaded)
        {
            string message = "";
            Excel.Application excel = new Excel.Application();

            #region New Upload File
            // Check file was submitted             
            if (fileUploaded != null && fileUploaded.ContentLength > 0)
            {
                if (fileUploaded.FileName.EndsWith("xls") || fileUploaded.FileName.EndsWith("xlsx") || fileUploaded.FileName.EndsWith("XLSX") || fileUploaded.FileName.EndsWith("XLS")) 
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
                        // Get selected user
                        var user = _employeeService.GetByID(uploadVM.SelectedEmployee);

                        if (user != null)
                        {
                            var evaluator = _employeeService.GetByID(uploadVM.SelectedEvaluator);

                            bool fileSaved = ReadPerformanceFile(fullPath, user, evaluator);

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
                            TempData["Error"] = "Selected resource not found. Please select a valid resource.";
                        }
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
                else
                {
                    TempData["Error"] = "File is not a valid excel file";
                }
            }
            else 
            {
                // No file uploaded
                TempData["Error"] = "No File Uploaded";
            }
            #endregion

            return RedirectToAction("UploadFile");
            #region old upload file
            //if (fileUploaded == null || fileUploaded.ContentLength == 0)
            //{
            //    //ViewBag.Error = "Please Select  a excel file<br>";
            //    return View("UploadFile");
            //}
            //else
            //{
            //    if (fileUploaded.FileName.EndsWith("xls") || fileUploaded.FileName.EndsWith("xlsx"))
            //    {
            //        string path = Server.MapPath("~/Content/" + fileUploaded.FileName);
            //        if (System.IO.File.Exists(path))
            //        {
            //            excel.Quit();
            //            System.IO.File.Delete(path);
            //        }

            //        //Save the file in the repository   
            //        fileUploaded.SaveAs(path);


            //        #region comment for now
            //        try
            //        {
            //            // Get selected user
            //            var user = _employeeService.GetByID(uploadVM.SelectedEmployee);

            //            if (user != null)
            //            {
            //                var evaluator = _employeeService.GetByID(user.ManagerId);

            //                bool fileSaved = ReadPerformanceFile(path, user, evaluator);

            //                if (!fileSaved)
            //                {
            //                    // File not saved
            //                    TempData["Error"] = "There were some problems saving the file. Please try again later.";
            //                }
            //                else
            //                {
            //                    // File saved successfully
            //                    TempData["Success"] = "File saved successfully";
            //                }
            //            }
            //        }
            //        finally
            //        {
            //            //Delete the file from the repository
            //            if (System.IO.File.Exists(path))
            //            {
            //                excel.Quit();
            //                System.IO.File.Delete(path);
            //            }
            //        }
            //        #endregion

            //        return RedirectToAction("UploadFile");
            //    }
            //    else
            //    {
            //        //ViewBag.Error = "File type is incorrect<br>";
            //        TempData["Error"] = "File is not a valid excel file";

            //        return RedirectToAction("UploadFile");
            //    }

            //}
            #endregion
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

            List<Employee> listAllEmployees = new List<Employee>();
            listAllEmployees = _employeeService.GetAll();

            uploadVM.ListAllEmployees = listAllEmployees;
            uploadVM.ListEmployees = listEmployees;

            return View(uploadVM);
        }

        // GET: PeformanceEvaluation/SearchIformation
        public ActionResult SearchInformation()
        {
            // Get current users by using email in Session
            // Get current user 
            Employee currentUser = new Employee();
            var userEmail = (string)Session["UserEmail"];

            currentUser = _employeeService.GetByEmail(userEmail);

            if(currentUser.ProfileId == (int)ProfileUser.Resource)
            {
                // user is resource not allowed, return to home 
                // send error
                return RedirectToAction("ChoosePeriod");
            }

            PerformanceFilesPartial model = FillPerformancePartial(currentUser);
            
            return View(model);
        }

        // GET: PerformanceEvaluation/ChoosePeriod
        [HttpGet]
        public ActionResult ChoosePeriod(string employeeEmail, int employeeID)
        {
            //Get current user
            Employee currentUser = new Employee();
            var currentuserEmail = (string)Session["UserEmail"];
            currentUser = _employeeService.GetByEmail(currentuserEmail);

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

            ViewBag.UserName = user.FirstName + " " + user.LastName;
            ViewBag.UserEmail = employeeEmail;
            ViewBag.UserID = employeeID;
            ViewBag.CurrentUserProfile = currentUser.ProfileId;
            return View(choosePeriodVM);
        }

        //GET: PerformanceEvaluation/PEVisualization
        public ActionResult PEVisualization(int peID)
        {
            List<PerformanceSectionHelper> listPerformancHelper = new List<PerformanceSectionHelper>();
            _peService = new PEService();

            // Get performance evaluation 
            var pe = _peService.GetPerformanceEvaluationById(peID);
            var employee = _employeeService.GetByID(pe.EmployeeId);
            var evaluator = _employeeService.GetByID(pe.EvaluatorId);
            
            listPerformancHelper = _peService.GetPerformanceEvaluationByIDPE(peID);
            
            // Complete view PEVisualization
            PEVisualizationViewModel model = new PEVisualizationViewModel();

            model.PerformanceMain = new PerformanceMainPartial();
            model.PerformanceMain.Employee = employee;
            model.PerformanceMain.Evaluator = evaluator;
            model.PerformanceMain.totalEvaluation = pe.Total;

            // Initialize sections
            model.PerformanceSections = new PerformanceSectionsPartial();
            model.PerformanceSections.Sections = listPerformancHelper;

            // Initializae comments
            model.PerformanceComments = new PerformanceCommentsPartial();
            model.PerformanceComments.Comments = _commentService.GetCommentByPE(peID);

            // Initializae skills
            model.PerformanceSkills = new PerformanceSkilsPartial();
            model.PerformanceSkills.Skills = _lm_skillService.GetSkillsWithNameByPEId(peID);

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

            return View(model);
        }

        // GET: PerformanceEvaluation/SearchInfoRank
        public ActionResult SearchInfoRank()
        {
            return View();
        }


        public ActionResult UpdateRank(List<PerformanceRankHelper> listPerformances)
        {
            int countUpdated = 0;

            // Update each performance record
            foreach (var peformance in listPerformances)
            {
                var updated = _peService.UpdateRank(peformance.performanceId, peformance.rankValue);
                if (updated)
                {
                    countUpdated++;
                }
            }

            var userEmail = (string)Session["UserEmail"];

            Employee currentUser = _employeeService.GetByEmail(userEmail);

            PerformanceFilesPartial partial = FillPerformancePartial(currentUser);
            partial.countRankUpdated = countUpdated;

            return PartialView("_PerformanceFilesPartial", partial);
        }

        public PerformanceFilesPartial FillPerformancePartial(Employee currentUser) 
        {
            currentUser = _employeeService.GetByEmail(currentUser.Email);

            List<Employee> employees = new List<Employee>();
            if (currentUser.ProfileId == (int)ProfileUser.Director)
            {
                // Get all
                employees = _employeeService.GetAll();
            }
            else if (currentUser.ProfileId == (int)ProfileUser.Manager)
            {
                // Get by manager 
                employees = _employeeService.GetEmployeeByManager(currentUser.EmployeeId);
            }

            List<EmployeeManagerViewModel> listEmployeeVM = new List<EmployeeManagerViewModel>();
            foreach (var employee in employees)
            {
                var employeeVM = new EmployeeManagerViewModel();
                employeeVM.employee = employee;
                employeeVM.manager = _employeeService.GetByID(employee.ManagerId);

                var listPE = _peService.GetPerformanceEvaluationByUserID(employee.EmployeeId);

                if (listPE != null && listPE.Count > 0)
                {
                    var lastPE = listPE.OrderByDescending(pe => pe.EvaluationPeriod).FirstOrDefault();

                    employeeVM.lastPe = lastPE;
                }

                listEmployeeVM.Add(employeeVM);
            }

            PerformanceFilesPartial partial = new PerformanceFilesPartial(listEmployeeVM, currentUser);

            return partial;
        }
    }
}