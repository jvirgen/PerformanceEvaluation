using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

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

            // Connect to the DB 
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string insertQuery = @"INSERT INTO PE (EVALUATION_PERIOD,
                                                       ID_EMPLOYEE,
                                                       ID_EVALUATOR,
                                                       ID_STATUS,
                                                       TOTAL,
                                                       ENGLISH_SCORE,
                                                       PERFORMANCE_SCORE,
                                                       COMPETENCE_SCORE,
                                                       ""RANK""
                                                      ) 
                                               VALUES (:evaluationPeriod,
                                                       :employeeId,
                                                       :evaluatorId,
                                                       :statusId,
                                                       :total,
                                                       :englishScore,
                                                       :performanceScore,
                                                       :competenceScore,
                                                       :rank)";

                // Adding parameters
                using (OracleCommand command = new OracleCommand(insertQuery, db))
                {
                    command.Parameters.Add(new OracleParameter("evaluationPeriod", OracleDbType.Date, pe.EvaluationPeriod, ParameterDirection.Input));
                    command.Parameters.Add(new OracleParameter("employeeId", pe.EmployeeId));
                    command.Parameters.Add(new OracleParameter("evaluatorId", pe.EvaluatorId));
                    command.Parameters.Add(new OracleParameter("statusId", pe.StatusId));
                    command.Parameters.Add(new OracleParameter("total", pe.Total));
                    command.Parameters.Add(new OracleParameter("englishScore", pe.EnglishScore));
                    command.Parameters.Add(new OracleParameter("performanceScore", pe.PerformanceScore));
                    command.Parameters.Add(new OracleParameter("competenceScore", pe.CompeteneceScore));
                    command.Parameters.Add(new OracleParameter("rank", pe.Rank));

                    try
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine(ex.ToString());
                        throw;
                    }

                    status = true;
                }
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
                                            "COMPETENCE_SCORE," +
                                            "\"RANK\"" +
                                            "FROM PE WHERE ID_EMPLOYEE = " + userId + " AND EVALUATION_PERIOD = TO_DATE('" + date.Date.ToShortDateString() + "', 'MM-DD-YYYY') AND ROWNUM <=1 " +
                                            "ORDER BY EVALUATION_PERIOD, ID_PE DESC";

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
                        pes.Rank = Convert.ToDouble(Read["RANK"]);

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
                                           "COMPETENCE_SCORE," +
                                           "\"RANK\"" +
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
                        string englishScore = Convert.ToString(Read["ENGLISH_SCORE"]);
                        pes.PerformanceScore = Convert.ToDouble(Read["PERFORMANCE_SCORE"]);
                        pes.CompeteneceScore = Convert.ToDouble(Read["COMPETENCE_SCORE"]);
                        pes.Rank = Convert.ToDouble(Read["RANK"]);
                        if (!string.IsNullOrEmpty(englishScore))
                        {
                            pes.EnglishScore = Convert.ToDouble(englishScore);
                        }
                        else 
                        {
                            pes.EnglishScore = 0;
                        }

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

        public List<PerformanceSectionHelper> GetPerformanceEvaluationByIDPE(int peId)
        {
            List<PerformanceSectionHelper> listPerformanceSectionHelp = null;

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
                        listPerformanceSectionHelp = new List<PerformanceSectionHelper>();                     
                        while (reader.Read())
                        {
                            var performanceSectionHelp = new PerformanceSectionHelper();

                            performanceSectionHelp.Title = Convert.ToString(reader["TITLE"]);
                            performanceSectionHelp.Subtitle = Convert.ToString(reader["SUBTITLE"]);
                            performanceSectionHelp.Description = Convert.ToString(reader["DESCRIPTION"]);
                            performanceSectionHelp.ScoreEmployee = Convert.ToInt32(reader["SCEMPLOYEE"]);
                            performanceSectionHelp.ScoreEvaluator = Convert.ToInt32(reader["SCEVALUATOR"]); 
                            performanceSectionHelp.Comments = Convert.ToString(reader["COMMENTS"]);
                            performanceSectionHelp.Calculation = Convert.ToDouble(reader["CALCULATION"]);

                            listPerformanceSectionHelp.Add(performanceSectionHelp);
                        }
                    }
                    db.Close();
                }
            }
            catch
            {
                throw;   
            }
            return listPerformanceSectionHelp;
        }

        public bool UpdateRank(int peId, double rank)
        {
            bool status = false;

            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string updateRank = @"UPDATE PE
                                        SET ""RANK"" = :rank
                                       WHERE ID_PE = :peId";

                using (OracleCommand command = new OracleCommand(updateRank, db))
                {
                    command.Parameters.Add(new OracleParameter("rank", rank));
                    command.Parameters.Add(new OracleParameter("peId", peId));

                    try
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();
                        status = true;
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine(ex.ToString());
                        throw;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        throw;
                    }
                }
            }
            return status;
        }
    }
}