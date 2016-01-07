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


        public bool InsertPE(PEs pe)
        {
            bool status = false;

            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string InsertQuery = "INSERT INTO PE (EVALUATION_PERIOD," +
                                          "ID_EMPLOYEE," +
                                          "ID_EVALUATOR," +
                                          "ID_STATUS," +
                                          "TOTAL) " +
                                          "VALUES (TO_DATE('" + pe.EvaluationPeriod.ToShortDateString() + "','DD-MM-YYYY ')," +
                                                      pe.EmployeeId + "," +
                                                      pe.EvaluatorId + "," +
                                                      pe.StatusId + "," +
                                                      pe.Total + ")";
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
                                            "FROM PE WHERE ID_EMPLOYEE = '" + userId + "' AND EVALUATION_PERIOD = '" + date.Date + "'";
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
    }
}