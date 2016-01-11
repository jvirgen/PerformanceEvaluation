using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using PES.DBContext;
using Oracle.ManagedDataAccess.Client;
using OfficeOpenXml;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace PES.Services
{
    public class ScoreService
    {
        private PESDBContext dbContext;
        public ScoreService()
        {
            dbContext = new PESDBContext();
        }
        public bool InsertScore (Score score)
        {
            bool status= false;
            
            using (OracleConnection db = dbContext.GetDBConnection())
            {

                string Insertquery = @" INSERT INTO SCORE (ID_DESCRIPTION,
                                                                 ID_PE, 
                                                                 SCORE_EMPLOYEE, 
                                                                 SCORE_EVALUATOR, 
                                                                 COMMENTS, 
                                                                 CALCULATION) 
                                                VALUES (:DescriptionId, 
                                                      :PEId, 
                                                      :ScoreEmployee, 
                                                      :ScoreEvaluator, 
                                                      :Comments, 
                                                      :Calculation)";

                using (OracleCommand Command = new OracleCommand(Insertquery, db))
                {
                    Command.Parameters.Add(new OracleParameter("DescriptionId", score.DescriptionId));
                    Command.Parameters.Add(new OracleParameter("PEId", score.PEId));
                    Command.Parameters.Add(new OracleParameter("ScoreEmployee", score.ScoreEmployee));
                    Command.Parameters.Add(new OracleParameter("ScoreEvaluator", score.ScoreEvaluator));
                    Command.Parameters.Add(new OracleParameter("Comments", score.Comments));
                    Command.Parameters.Add(new OracleParameter("Calculation", score.Calculation));

                    try
                    {
                        Command.Connection.Open();
                        Command.ExecuteNonQuery();
                        Command.Connection.Close();
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
    }
}