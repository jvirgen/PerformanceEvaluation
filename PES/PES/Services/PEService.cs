using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;

namespace PES.Services
{
    public class PEService
    {
        private PESDBContext dbContext = new PESDBContext();
        private EmployeeService _employeeService;

        public PEService()
        {
            _employeeService = new EmployeeService();
        }


        public bool InsertPE(PEs pe)
        {
            bool status = false;

            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string InsertQuery = "INSERT INTO PE (EVALUATION_PERIOD, " +
                                          "ID_EMPLOYEE, " +
                                          "ID_EVALUATOR, " +
                                          "ID_STATUS, " +
                                          "TOTAL, " +
                                          "ENGLISH_SCORE, " +
                                          "PERFORMANCE_SCORE, " +
                                          "COMPETENCE_SCORE) " +
                                          "VALUES (TO_DATE('" + pe.EvaluationPeriod.ToShortDateString() + "','MM-DD-YYYY '), " +
                                                      pe.EmployeeId + ", " +
                                                      pe.EvaluatorId + ", " +
                                                      pe.StatusId + ", " +
                                                      pe.Total + ", " +
                                                      pe.EnglishScore + ", " +
                                                      pe.PerformanceScore + ", " +
                                                      pe.CompeteneceScore + ")";
                    OracleCommand Command = new OracleCommand(InsertQuery, db);
                    Command.ExecuteNonQuery();
                    status = true;
                    db.Close();
                }
            }
            catch
            {
                status = false;
            }
            
            return status;
        }
        
        public PEs GetPerformanceEvaluationByDate(int userId, DateTime date)
        {
            // complete function
            PEs PES = null;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string Query = "SELECT ID_PE," +
                                            "EVALUATION_PERIOD," +
                                            "ID_EMPLOYEE," +
                                            "ID_EVALUATOR," +
                                            "ID_STATUS," +
                                            "TOTAL," +
                                            "ENGLISH_SCORE," +
                                            "PERFORMANCE_SCORE," +
                                            "COMPETENCE_SCORE " +
                                            "FROM PE WHERE ID_EMPLOYEE = " + userId + " AND EVALUATION_PERIOD = TO_DATE('" + date.Date.ToShortDateString() + "', 'MM-DD-YYYY')";

                    OracleCommand Command = new OracleCommand(Query, db);
                    OracleDataReader Read = Command.ExecuteReader();
                    PES = new PEs();
                    while (Read.Read())
                    {

                        // Store data in employee object 
                        PEs pes = new PEs();
                        pes.EmployeeId = Convert.ToInt32(Read["ID_EMPLOYEE"]);
                        pes.PEId = Convert.ToInt32(Read["ID_PE"]);
                        pes.EvaluationPeriod = Convert.ToDateTime(Read["EVALUATION_PERIOD"]);
                        pes.EvaluatorId = Convert.ToInt32(Read["ID_EVALUATOR"]);
                        pes.StatusId = Convert.ToInt32(Read["ID_STATUS"]);
                        pes.Total = Convert.ToDouble(Read["TOTAL"]);
                        pes.EnglishScore = Convert.ToDouble(Read["ENGLISH_SCORE"]);
                        pes.PerformanceScore = Convert.ToDouble(Read["PERFORMANCE_SCORE"]);
                        pes.CompeteneceScore = Convert.ToDouble(Read["COMPETENCE_SCORE"]);

                        PES = pes;
                    }
                    db.Close();
                }
            }
            catch
            {
                PES = null;
            }

            return PES;

        }

        public PEs GetPerformanceEvaluationByDateEmail(string email, DateTime date) 
        {
            // Get user
            var user = _employeeService.GetByEmail(email);

            PEs pe = GetPerformanceEvaluationByDate(user.EmployeeId, date);

            return pe;
        }

        public List<PEs> GetPerformanceEvaluationByUserID(int userid) 
        {
            List<PEs> listPES = null;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string Query = "SELECT ID_PE," +
                                           "EVALUATION_PERIOD," +
                                           "ID_EMPLOYEE," +
                                           "ID_EVALUATOR," +
                                           "ID_STATUS," +
                                           "TOTAL," +
                                           "ENGLISH_SCORE," +
                                           "PERFORMANCE_SCORE," +
                                           "COMPETENCE_SCORE " +
                                           "FROM PE WHERE ID_EMPLOYEE = '" + userid + "'";

                    OracleCommand Comand = new OracleCommand(Query, db);
                    
                    OracleDataReader Read = Comand.ExecuteReader();
                    listPES = new List<PEs>();
                    while (Read.Read())
                    {

                        // Store data in employee object 
                        PEs pes = new PEs();
                        pes.EmployeeId = Convert.ToInt32(Read["ID_EMPLOYEE"]);
                        pes.PEId = Convert.ToInt32(Read["ID_PE"]);
                        pes.EvaluationPeriod = Convert.ToDateTime(Read["EVALUATION_PERIOD"]);
                        pes.EvaluatorId = Convert.ToInt32(Read["ID_EVALUATOR"]);
                        pes.StatusId = Convert.ToInt32(Read["ID_STATUS"]);
                        pes.Total = Convert.ToDouble(Read["TOTAL"]);
                        pes.EnglishScore = Convert.ToDouble(Read["ENGLISH_SCORE"]);
                        pes.PerformanceScore = Convert.ToDouble(Read["PERFORMANCE_SCORE"]);
                        pes.CompeteneceScore = Convert.ToDouble(Read["COMPETENCE_SCORE"]);

                        listPES.Add(pes);
                    }
                    
                    db.Close();
                }
            }
            catch
            {
                listPES = null;
            }
            return listPES;
        }

        //public PEs GetPerformanceEvaluationByUser(int userId)
        //{
        //    return null;
        //}

        public List<PESComplete> GetPerformanceEvaluationByIDPE(int peId)
        {
            List<PESComplete> pesComplete;
            PESComplete peComplete;

            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string selectQuery = @"SELECT T.TITLE AS TITLE,
                                                  ST.SUBTITLE AS SUBTITLE,
                                                  D.DESCRIPTION AS DESCRIPTION,
                                                  SC.SCORE_EMPLOYEE AS SCEMPLOYEe, 
                                                  SC.SCORE_EVALUATOR AS SCEVALUATOR, 
                                                  SC.COMMENTS AS COMMENTS, 
                                                  SC.CALCULATION AS CALCULATION
                                             FROM TITLE T
                                       INNER JOIN SUBTITLE ST ON T.ID_TITLE = ST.ID_TITLE
                                       INNER JOIN DESCRIPTION D ON ST.ID_SUBTITLE = D.ID_SUBTITLE
                                        LEFT JOIN SCORE SC ON D.ID_DESCRIPTION = SC.ID_DESCRIPTION
                                            WHERE SC.ID_PE = :peId 
                                         ORDER BY T.ID_TITLE, ST.ID_SUBTITLE, D.ID_DESCRIPTION";
                    using (OracleCommand command = new OracleCommand(selectQuery, db))
                    {
                        command.Parameters.Add(new OracleParameter("peId", peId));
                        OracleDataReader reader = command.ExecuteReader();
                        pesComplete = new List<PESComplete>();

                        while (reader.Read())
                        {
                            peComplete = new PESComplete();

                            peComplete.title1.Name = Convert.ToString(reader["TITLE"]);
                            peComplete.subtitle1.Name = Convert.ToString(reader["SUBTITLE"]);
                            peComplete.description1.DescriptionText = Convert.ToString(reader["DESCRIPTION"]);
                            peComplete.scorePerformance.ScoreEmployee = Convert.ToInt32(reader["SCEMPLOYEE"]);
                            peComplete.scorePerformance.ScoreEvaluator = Convert.ToInt32(reader["SCEVALUATOR"]); 
                            peComplete.scorePerformance.Comments = Convert.ToString(reader["COMMENTS"]);
                            peComplete.scorePerformance.Calculation = Convert.ToInt32(reader["CALCULATION"]);

                            pesComplete.Add(peComplete);
                        }
                    }
                    db.Close();
                }
            }
            catch
            {
                pesComplete = null;
                }
            return pesComplete;
        }
    }
}