using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;

namespace PES.Services
{
    public class TitleService
    {
        private PESDBContext dbContext = new PESDBContext();


        public bool InsertTitle(Title title)
        {
            bool status = false;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string InsertTitle = "INSERT INTO TITLE (TITLE)" +
                                            "VALUES ('" + title.Name + "')";
                    OracleCommand Command = new OracleCommand(InsertTitle, db);
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