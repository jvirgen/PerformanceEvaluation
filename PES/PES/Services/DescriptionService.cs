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
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string InsertDescription = @"INSERT INTO DESCRIPTION (DESCRIPTION, 
                                                                ID_SUBTITLE)
                                        VALUES (:DescriptionText, 
                                                    :SubtitleId)";
                using (OracleCommand Command = new OracleCommand(InsertDescription, db))
                {
                    Command.Parameters.Add(new OracleParameter("DescriptionText", description.DescriptionText));
                    Command.Parameters.Add(new OracleParameter("SubtitleId", description.SubtitleId));

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