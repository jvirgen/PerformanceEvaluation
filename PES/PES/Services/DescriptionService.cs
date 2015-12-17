using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;

namespace PES.Services
{
    public class DescriptionService
    {
        private PESDBContext dbContext = new PESDBContext();


        public bool InsertDescription(Description description)
        {
            bool status = false;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string InsertDescription = "INSERT INTO DESCRIPTION (DESCRIPTION," +
                                                                 "ID_SUBTITLE)" +
                                            "VALUES ('" + description.DescriptionText + "'," +
                                                       description.SubtitleId + ")";
                    OracleCommand Command = new OracleCommand(InsertDescription, db);
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