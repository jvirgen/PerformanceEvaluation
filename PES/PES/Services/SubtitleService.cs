using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using PES.DBContext;
using Oracle.ManagedDataAccess.Client;

namespace PES.Services
{
    public class SubtitleService
    {
        private PESDBContext dbContext = new PESDBContext();

        public bool InsertSubtitles(Subtitle subtitle)
        {
            bool status = false;

            try
            {
                using(OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string InsertQuery = "INSERT INTO SUBTITLE (SUBTITLE,"+
                                                                "ID_TITLE)"+
                                         "VALUES ('"+subtitle.Name+"',"+
                                                    subtitle.TitleId+")";

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