using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using PES.DBContext;
using Oracle.ManagedDataAccess.Client;
using OfficeOpenXml;
using System.IO;

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
            
                try
                {
                    using (OracleConnection db = dbContext.GetDBConnection())
                    {

                        db.Open();
                        string Insertquery = "INSERT INTO SCORE (ID_DESCRIPTION, " +
                                                                 "ID_PE, " +
                                                                 "SCORE_EMPLOYEE, " +
                                                                 "SCORE_EVAULATOR, " +
                                                                 "COMMENTS, " +
                                                                 "CALCULATION) " +
                                            "VALUES (" + score.DescriptionId + ", " +
                                                      score.PEId + ", " +
                                                      score.ScoreEmployee + ", " +
                                                      score.ScoreEvaluator + ", " +
                                                      score.Comments + ", " +
                                                      score.Calculation + ")";
                        OracleCommand Command = new OracleCommand(Insertquery, db);
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
    }
}