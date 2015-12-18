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
                                          "VALUES ('" +pe.EvaluationPeriod.ToShortDateString() + "'," +
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
    }
}