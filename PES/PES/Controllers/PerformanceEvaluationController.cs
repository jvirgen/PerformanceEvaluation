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
    //[Authorize]
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

        public PESComplete ReadPerformanceFile(string path) 
        {
            // Read
            /*read excel*/
            Excel.Application excel = new Excel.Application();
            Excel.Workbook wb = excel.Workbooks.Open(path);
            Excel.Worksheet excelSheet = wb.ActiveSheet;
             PESComplete PESc = new PESComplete();

            PESc.pes.Total = (int)excelSheet.Cells[9, 6];

            PESc.empleado.FirstName = excelSheet.Cells[3, 3];
            PESc.empleado.LastName = excelSheet.Cells[3, 3];
            PESc.empleado.Position = excelSheet.Cells[3, 4];
            PESc.empleado.Customer = excelSheet.Cells[3, 5];
            PESc.empleado.Project = excelSheet.Cells[3, 6];

            PESc.title1.Name = excelSheet.Cells[2, 19];
            PESc.title2.Name = excelSheet.Cells[2, 39];

            PESc.subtitle1.Name = excelSheet.Cells[2, 23];
            PESc.subtitle2.Name = excelSheet.Cells[2, 32];
            PESc.subtitle3.Name = excelSheet.Cells[2, 43];
            PESc.subtitle4.Name = excelSheet.Cells[2, 52];
            PESc.subtitle5.Name = excelSheet.Cells[2, 59];
            PESc.subtitle6.Name = excelSheet.Cells[2, 67];

            PESc.description1.DescriptionText = excelSheet.Cells[2, 24];
            PESc.description2.DescriptionText = excelSheet.Cells[2, 25];
            PESc.description3.DescriptionText = excelSheet.Cells[2, 26];
            PESc.description4.DescriptionText = excelSheet.Cells[2, 27];
            PESc.description5.DescriptionText = excelSheet.Cells[2, 28];
            PESc.description6.DescriptionText = excelSheet.Cells[2, 29];
            PESc.description7.DescriptionText = excelSheet.Cells[2, 33];
            PESc.description8.DescriptionText = excelSheet.Cells[2, 34];
            PESc.description9.DescriptionText = excelSheet.Cells[2, 35];
            PESc.description10.DescriptionText = excelSheet.Cells[2, 44];
            PESc.description11.DescriptionText = excelSheet.Cells[2, 45];
            PESc.description12.DescriptionText = excelSheet.Cells[2, 46];
            PESc.description13.DescriptionText = excelSheet.Cells[2, 47];
            PESc.description14.DescriptionText = excelSheet.Cells[2, 48];
            PESc.description15.DescriptionText = excelSheet.Cells[2, 49];
            PESc.description16.DescriptionText = excelSheet.Cells[2, 53];
            PESc.description17.DescriptionText = excelSheet.Cells[2, 54];
            PESc.description18.DescriptionText = excelSheet.Cells[2, 55];
            PESc.description19.DescriptionText = excelSheet.Cells[2, 56];
            PESc.description21.DescriptionText = excelSheet.Cells[2, 61];
            PESc.description22.DescriptionText = excelSheet.Cells[2, 62];
            PESc.description24.DescriptionText = excelSheet.Cells[2, 64];
            PESc.descriptionPuctuality.DescriptionText = excelSheet.Cells[2, 68];
            PESc.descriptionPolicies.DescriptionText = excelSheet.Cells[2, 69];
            PESc.descriptionValues.DescriptionText = excelSheet.Cells[2, 70];
            PESc.subtotalQuality.DescriptionText = excelSheet.Cells[2, 30];
            PESc.subtotalOpportunity.DescriptionText = excelSheet.Cells[2, 36];
            PESc.totalPerformance.DescriptionText = excelSheet.Cells[2, 37];
            PESc.subtotalSkills.DescriptionText = excelSheet.Cells[2, 50];
            PESc.subtotalInterpersonal.DescriptionText = excelSheet.Cells[2, 57];
            PESc.subtotalGrowth.DescriptionText = excelSheet.Cells[2, 65];
            PESc.subtotalPolicies.DescriptionText = excelSheet.Cells[2, 71];
            PESc.totalCompetences.DescriptionText = excelSheet.Cells[2, 73];

            PESc.one.ScoreEmployee = excelSheet.Cells[6, 24];
            PESc.two.ScoreEmployee = excelSheet.Cells[6, 25];
            PESc.three.ScoreEmployee = excelSheet.Cells[6, 26];
            PESc.four.ScoreEmployee = excelSheet.Cells[6, 27];
            PESc.five.ScoreEmployee = excelSheet.Cells[6, 28];
            PESc.six.ScoreEmployee = excelSheet.Cells[6, 29];
            PESc.seven.ScoreEmployee = excelSheet.Cells[6, 33];
            PESc.eight.ScoreEmployee = excelSheet.Cells[6, 34];
            PESc.nine.ScoreEmployee = excelSheet.Cells[6, 35];
            PESc.ten.ScoreEmployee = excelSheet.Cells[6, 44];
            PESc.eleven.ScoreEmployee = excelSheet.Cells[6, 45];
            PESc.twelve.ScoreEmployee = excelSheet.Cells[6, 46];
            PESc.thirteen.ScoreEmployee = excelSheet.Cells[6, 47];
            PESc.fourteen.ScoreEmployee = excelSheet.Cells[6, 48];
            PESc.fifteen.ScoreEmployee = excelSheet.Cells[6, 49];
            PESc.sixteen.ScoreEmployee = excelSheet.Cells[6, 53];
            PESc.seventeen.ScoreEmployee = excelSheet.Cells[6, 54];
            PESc.eighteen.ScoreEmployee = excelSheet.Cells[6, 55];
            PESc.nineteen.ScoreEmployee = excelSheet.Cells[6, 56];
            PESc.twentyone.ScoreEmployee = excelSheet.Cells[6, 61];
            PESc.twentytwo.ScoreEmployee = excelSheet.Cells[6, 62];
            PESc.twentyfour.ScoreEmployee = excelSheet.Cells[6, 64];
            PESc.punctuality.ScoreEmployee = excelSheet.Cells[6, 68];
            PESc.policies.ScoreEmployee = excelSheet.Cells[6, 69];
            PESc.values.ScoreEmployee = excelSheet.Cells[6, 70];

            PESc.one.ScoreEvaluator = excelSheet.Cells[7, 24];
            PESc.two.ScoreEvaluator = excelSheet.Cells[7, 25];
            PESc.three.ScoreEvaluator = excelSheet.Cells[7, 26];
            PESc.four.ScoreEvaluator = excelSheet.Cells[7, 27];
            PESc.five.ScoreEvaluator = excelSheet.Cells[7, 28];
            PESc.six.ScoreEvaluator = excelSheet.Cells[7, 29];
            PESc.seven.ScoreEvaluator = excelSheet.Cells[7, 33];
            PESc.eight.ScoreEvaluator = excelSheet.Cells[7, 34];
            PESc.nine.ScoreEvaluator = excelSheet.Cells[7, 35];
            PESc.ten.ScoreEvaluator = excelSheet.Cells[7, 44];
            PESc.eleven.ScoreEvaluator = excelSheet.Cells[7, 45];
            PESc.twelve.ScoreEvaluator = excelSheet.Cells[7, 46];
            PESc.thirteen.ScoreEvaluator = excelSheet.Cells[7, 47];
            PESc.fourteen.ScoreEvaluator = excelSheet.Cells[7, 48];
            PESc.fifteen.ScoreEvaluator = excelSheet.Cells[7, 49];
            PESc.sixteen.ScoreEvaluator = excelSheet.Cells[7, 53];
            PESc.seventeen.ScoreEvaluator = excelSheet.Cells[7, 54];
            PESc.eighteen.ScoreEvaluator = excelSheet.Cells[7, 55];
            PESc.nineteen.ScoreEvaluator = excelSheet.Cells[7, 56];
            PESc.twentyone.ScoreEvaluator = excelSheet.Cells[7, 61];
            PESc.twentytwo.ScoreEvaluator = excelSheet.Cells[7, 62];
            PESc.twentyfour.ScoreEvaluator = excelSheet.Cells[7, 64];
            PESc.punctuality.ScoreEvaluator = excelSheet.Cells[7, 68];
            PESc.policies.ScoreEvaluator = excelSheet.Cells[7, 69];
            PESc.values.ScoreEvaluator = excelSheet.Cells[7, 70];

            PESc.one.Comments = excelSheet.Cells[8, 24];
            PESc.two.Comments = excelSheet.Cells[8, 25];
            PESc.three.Comments = excelSheet.Cells[8, 26];
            PESc.four.Comments = excelSheet.Cells[8, 27];
            PESc.five.Comments = excelSheet.Cells[8, 28];
            PESc.six.Comments = excelSheet.Cells[8, 29];
            PESc.seven.Comments = excelSheet.Cells[8, 33];
            PESc.eight.Comments = excelSheet.Cells[8, 34];
            PESc.nine.Comments = excelSheet.Cells[8, 35];
            PESc.ten.Comments = excelSheet.Cells[8, 44];
            PESc.eleven.Comments = excelSheet.Cells[8, 45];
            PESc.twelve.Comments = excelSheet.Cells[8, 46];
            PESc.thirteen.Comments = excelSheet.Cells[8, 47];
            PESc.fourteen.Comments = excelSheet.Cells[8, 48];
            PESc.fifteen.Comments = excelSheet.Cells[8, 49];
            PESc.sixteen.Comments = excelSheet.Cells[8, 53];
            PESc.seventeen.Comments = excelSheet.Cells[8, 54];
            PESc.eighteen.Comments = excelSheet.Cells[8, 55];
            PESc.nineteen.Comments = excelSheet.Cells[8, 56];
            PESc.twentyone.Comments = excelSheet.Cells[8, 61];
            PESc.twentytwo.Comments = excelSheet.Cells[8, 62];
            PESc.twentyfour.Comments = excelSheet.Cells[8, 64];
            PESc.punctuality.Comments = excelSheet.Cells[8, 68];
            PESc.policies.Comments = excelSheet.Cells[8, 69];
            PESc.values.Comments = excelSheet.Cells[8, 70];

            PESc.one.Calculation = excelSheet.Cells[9, 24];
            PESc.two.Calculation = excelSheet.Cells[9, 25];
            PESc.three.Calculation = excelSheet.Cells[9, 26];
            PESc.four.Calculation = excelSheet.Cells[9, 27];
            PESc.five.Calculation = excelSheet.Cells[9, 28];
            PESc.six.Calculation = excelSheet.Cells[9, 29];
            PESc.seven.Calculation = excelSheet.Cells[9, 33];
            PESc.eight.Calculation = excelSheet.Cells[9, 34];
            PESc.nine.Calculation = excelSheet.Cells[9, 35];
            PESc.ten.Calculation = excelSheet.Cells[9, 44];
            PESc.eleven.Calculation = excelSheet.Cells[9, 45];
            PESc.twelve.Calculation = excelSheet.Cells[9, 46];
            PESc.thirteen.Calculation = excelSheet.Cells[9, 47];
            PESc.fourteen.Calculation = excelSheet.Cells[9, 48];
            PESc.fifteen.Calculation = excelSheet.Cells[9, 49];
            PESc.sixteen.Calculation = excelSheet.Cells[9, 53];
            PESc.seventeen.Calculation = excelSheet.Cells[9, 54];
            PESc.eighteen.Calculation = excelSheet.Cells[9, 55];
            PESc.nineteen.Calculation = excelSheet.Cells[9, 56];
            PESc.twentyone.Calculation = excelSheet.Cells[9, 61];
            PESc.twentytwo.Calculation = excelSheet.Cells[9, 62];
            PESc.twentyfour.Calculation = excelSheet.Cells[9, 64];
            PESc.punctuality.Calculation = excelSheet.Cells[9, 68];
            PESc.policies.Calculation = excelSheet.Cells[9, 69];
            PESc.values.Calculation = excelSheet.Cells[9, 70];

            PESc.scoreQuality.Calculation = excelSheet.Cells[9, 30];
            PESc.scoreOpportunity.Calculation = excelSheet.Cells[9, 36];
            PESc.scorePerformance.Calculation = excelSheet.Cells[9, 37];
            PESc.scoreSkills.Calculation = excelSheet.Cells[9, 50];
            PESc.scoreInterpersonal.Calculation = excelSheet.Cells[9, 57];
            PESc.scoreGrowth.Calculation = excelSheet.Cells[9, 65];
            PESc.scorePolicies.Calculation = excelSheet.Cells[9, 71];
            PESc.scoreCompetences.Calculation = excelSheet.Cells[9, 73];

            PESc.comment1.TrainningEmployee = excelSheet.Cells[2, 78];
            PESc.comment2.TrainningEmployee = excelSheet.Cells[2, 79];
            PESc.comment1.TrainningEvaluator = excelSheet.Cells[2, 80];

            PESc.comment1.AcknowledgeEvaluator = excelSheet.Cells[2, 82];
            PESc.comment2.AcknowledgeEvaluator = excelSheet.Cells[2, 83];

            PESc.comment1.CommRecommEmployee = excelSheet.Cells[2, 85];
            PESc.comment2.CommRecommEmployee = excelSheet.Cells[2, 86];
            PESc.comment3.CommRecommEmployee = excelSheet.Cells[2, 87];
            PESc.comment1.CommRecommEvaluator = excelSheet.Cells[2, 88];
            PESc.comment2.CommRecommEvaluator = excelSheet.Cells[2, 89];
            PESc.comment3.CommRecommEvaluator = excelSheet.Cells[2, 90];

            PESc.skill1.Description = excelSheet.Cells[2, 96];
            PESc.skill2.Description = excelSheet.Cells[2, 97];
            PESc.skill3.Description = excelSheet.Cells[2, 98];
            PESc.skill4.Description = excelSheet.Cells[2, 99];
            PESc.skill5.Description = excelSheet.Cells[2, 100];
            PESc.skill6.Description = excelSheet.Cells[2, 101];
            PESc.skill7.Description = excelSheet.Cells[2, 102];
            PESc.skill8.Description = excelSheet.Cells[2, 103];
            PESc.skill9.Description = excelSheet.Cells[2, 104];
            PESc.skill10.Description = excelSheet.Cells[2, 105];
            PESc.skill11.Description = excelSheet.Cells[2, 106];
            PESc.skill12.Description = excelSheet.Cells[2, 107];
            PESc.skill13.Description = excelSheet.Cells[2, 108];
            PESc.skill14.Description = excelSheet.Cells[2, 109];
            PESc.skill15.Description = excelSheet.Cells[2, 110];
            PESc.skill16.Description = excelSheet.Cells[2, 111];
            PESc.skill17.Description = excelSheet.Cells[2, 112];

            PESc.supervises.CheckEmployee = excelSheet.Cells[6, 96];
            PESc.coordinates.CheckEmployee = excelSheet.Cells[6, 97];
            PESc.defines.CheckEmployee = excelSheet.Cells[6, 98];
            PESc.supports.CheckEmployee = excelSheet.Cells[6, 99];
            PESc.keeps.CheckEmployee = excelSheet.Cells[6, 100];
            PESc.generates.CheckEmployee = excelSheet.Cells[6, 101];
            PESc.trains.CheckEmployee = excelSheet.Cells[6, 102];
            PESc.supportsExperimentation.CheckEmployee = excelSheet.Cells[6, 103];
            PESc.evaluates.CheckEmployee = excelSheet.Cells[6, 104];
            PESc.faces.CheckEmployee = excelSheet.Cells[6, 105];
            PESc.supportsResponsible.CheckEmployee = excelSheet.Cells[6, 106];
            PESc.helps.CheckEmployee = excelSheet.Cells[6, 107];
            PESc.instills.CheckEmployee = excelSheet.Cells[6, 108];
            PESc.sets.CheckEmployee = excelSheet.Cells[6, 109];
            PESc.supportsUseful.CheckEmployee = excelSheet.Cells[6, 110];
            PESc.welcomes.CheckEmployee = excelSheet.Cells[6, 111];
            PESc.setsSpecific.CheckEmployee = excelSheet.Cells[6, 112];

            PESc.supervises.CheckEvaluator = excelSheet.Cells[6, 96];
            PESc.coordinates.CheckEvaluator = excelSheet.Cells[6, 97];
            PESc.defines.CheckEvaluator = excelSheet.Cells[6, 98];
            PESc.supports.CheckEvaluator = excelSheet.Cells[6, 99];
            PESc.keeps.CheckEvaluator = excelSheet.Cells[6, 100];
            PESc.generates.CheckEvaluator = excelSheet.Cells[6, 101];
            PESc.trains.CheckEvaluator = excelSheet.Cells[6, 102];
            PESc.supportsExperimentation.CheckEvaluator = excelSheet.Cells[6, 103];
            PESc.evaluates.CheckEvaluator = excelSheet.Cells[6, 104];
            PESc.faces.CheckEvaluator = excelSheet.Cells[6, 105];
            PESc.supportsResponsible.CheckEvaluator = excelSheet.Cells[6, 106];
            PESc.helps.CheckEvaluator = excelSheet.Cells[6, 107];
            PESc.instills.CheckEvaluator = excelSheet.Cells[6, 108];
            PESc.sets.CheckEvaluator = excelSheet.Cells[6, 109];
            PESc.supportsUseful.CheckEvaluator = excelSheet.Cells[6, 110];
            PESc.welcomes.CheckEvaluator = excelSheet.Cells[6, 111];
            PESc.setsSpecific.CheckEvaluator = excelSheet.Cells[6, 112];
            
            return PESc;
        }

        public bool SavePEFile(PESComplete pEFile) 
        {
            try
            {
                // Call services to insert
                _peService.InsertPE(pEFile.pes);

                _employeeService.InsertEmployee(pEFile.empleado);

                _titleService.InsertTitle(pEFile.title1);
                _titleService.InsertTitle(pEFile.title2);

                _subtitleService.InsertSubtitles(pEFile.subtitle1);
                _subtitleService.InsertSubtitles(pEFile.subtitle2);
                _subtitleService.InsertSubtitles(pEFile.subtitle3);
                _subtitleService.InsertSubtitles(pEFile.subtitle4);
                _subtitleService.InsertSubtitles(pEFile.subtitle5);
                _subtitleService.InsertSubtitles(pEFile.subtitle6);

                _descriptionService.InsertDescription(pEFile.description1);
                _descriptionService.InsertDescription(pEFile.description2);
                _descriptionService.InsertDescription(pEFile.description3);
                _descriptionService.InsertDescription(pEFile.description4);
                _descriptionService.InsertDescription(pEFile.description5);
                _descriptionService.InsertDescription(pEFile.description6);
                _descriptionService.InsertDescription(pEFile.description7);
                _descriptionService.InsertDescription(pEFile.description8);
                _descriptionService.InsertDescription(pEFile.description9);
                _descriptionService.InsertDescription(pEFile.description10);
                _descriptionService.InsertDescription(pEFile.description11);
                _descriptionService.InsertDescription(pEFile.description12);
                _descriptionService.InsertDescription(pEFile.description13);
                _descriptionService.InsertDescription(pEFile.description14);
                _descriptionService.InsertDescription(pEFile.description15);
                _descriptionService.InsertDescription(pEFile.description16);
                _descriptionService.InsertDescription(pEFile.description17);
                _descriptionService.InsertDescription(pEFile.description18);
                _descriptionService.InsertDescription(pEFile.description19);
                _descriptionService.InsertDescription(pEFile.description21);
                _descriptionService.InsertDescription(pEFile.description22);
                _descriptionService.InsertDescription(pEFile.description24);
                _descriptionService.InsertDescription(pEFile.descriptionPuctuality);
                _descriptionService.InsertDescription(pEFile.descriptionPolicies);
                _descriptionService.InsertDescription(pEFile.descriptionValues);
                _descriptionService.InsertDescription(pEFile.subtotalQuality);
                _descriptionService.InsertDescription(pEFile.subtotalOpportunity);
                _descriptionService.InsertDescription(pEFile.totalPerformance);
                _descriptionService.InsertDescription(pEFile.subtotalSkills);
                _descriptionService.InsertDescription(pEFile.subtotalInterpersonal);
                _descriptionService.InsertDescription(pEFile.subtotalGrowth);
                _descriptionService.InsertDescription(pEFile.subtotalPolicies);
                _descriptionService.InsertDescription(pEFile.totalCompetences);

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
                
                _skillService.InsertSkill(pEFile.skill1);
                _skillService.InsertSkill(pEFile.skill2);
                _skillService.InsertSkill(pEFile.skill3);
                _skillService.InsertSkill(pEFile.skill4);
                _skillService.InsertSkill(pEFile.skill5);
                _skillService.InsertSkill(pEFile.skill6);
                _skillService.InsertSkill(pEFile.skill7);
                _skillService.InsertSkill(pEFile.skill8);
                _skillService.InsertSkill(pEFile.skill9);
                _skillService.InsertSkill(pEFile.skill10);
                _skillService.InsertSkill(pEFile.skill11);
                _skillService.InsertSkill(pEFile.skill12);
                _skillService.InsertSkill(pEFile.skill13);
                _skillService.InsertSkill(pEFile.skill14);
                _skillService.InsertSkill(pEFile.skill15);
                _skillService.InsertSkill(pEFile.skill16);
                _skillService.InsertSkill(pEFile.skill17);

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
        public ActionResult UploadFile(HttpPostedFileBase fileUploaded)
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

                    try
                    {
                        PESComplete file = ReadPerformanceFile(path);
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
                    finally
                    {
                        //Delete the file from the repository
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }

                    return View("UploadFile");
                }
                else
                {
                    //ViewBag.Error = "File type is incorrect<br>";
                    TempData["Error"] = "File is not a valid excel file";

                    return View("UploadFile");
                }

            }
        }
        
        // GET: PerformanceEvaluation/UploadFile
        public ActionResult UploadFile()
        {
            return View();
        }

        // GET: PeformanceEvaluation/SearchIformation
        public ActionResult SearchInformation()
        {
            return View();
        }
        
        // GET: PeformanceEvaluation/SearchIformation
        public ActionResult Login()
        {
            return View();
        }

        // GET: PerformanceEvaluation/ChoosePeriod
        public ActionResult ChoosePeriod()
        {
            return View();
        }

        // GET: PerformanceEvaluation/SearchInfoRank
        public ActionResult SearchInfoRank()
        {
            return View();
        }

        //GET: PerformanceEvaluation/PEVisualization
        public ActionResult PEVisualization()
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