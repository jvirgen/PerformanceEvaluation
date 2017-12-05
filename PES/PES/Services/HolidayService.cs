using Oracle.ManagedDataAccess.Client;
using PES.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;

namespace PES.Services
{
    public class HolidayService
    {
        private PESDBContext dbContext = new PESDBContext();
        public HolidayService()
        {
            dbContext = new PESDBContext();
        }

        /// <summary>
        /// Metod to INSERT a subrequest in the DB using a VacationSubreq object
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns>True if the insert was successful</returns>
        //public bool InsertSubReq(Holiday holiday)
        //{
        //    bool status = false;

        //    //establish a connection
        //    try
        //    {

        //        using (OracleConnection db = dbContext.GetDBConnection())
        //        {
        //            string query = @"INSERT INTO HOLIDAYS 
        //                                ( HOLIDAY_DAY,
        //                                DESCRIPTION) 
        //                        VALUES
        //                            (:Holiday,
        //                             :Description)";

        //            using (OracleCommand command = new OracleCommand(query, db))
        //            {
        //                command.Parameters.Add(new OracleParameter("Holiday", holiday.Day));
        //                command.Parameters.Add(new OracleParameter("Description", holiday.Description));


        //                try
        //                {
        //                    command.Connection.Open();
        //                    command.ExecuteNonQuery();
        //                    command.Connection.Close();
        //                }
        //                catch (OracleException ex)
        //                {
        //                    Console.WriteLine(ex.ToString());
        //                    throw;
        //                }
        //                status = true;
        //            }
        //        }
        //    }
        //    catch (Exception xe)
        //    {
        //        throw;
        //    }

        //    return status;
        //}
        /// <summary>
        /// Selec date and description  from db 
        /// </summary>
        /// <returns></returns>
        public List<Holiday> GetAllHolidays()
        {
            List<Holiday> holidays;
            Holiday holiday;

            try
            {

                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string selectAllHolidays = @"SELECT  
                                                ID_HOLIDAY, 
                                                HOLIDAY_DAY,
                                                DESCRIPTION FROM HOLIDAYS";

                    using (OracleCommand command = new OracleCommand(selectAllHolidays, db))
                    {
                        OracleDataReader reader = command.ExecuteReader();
                        holidays = new List<Holiday>();
                        while (reader.Read())
                        {
                            holiday = new Holiday();
                            holiday.HolidayId = Convert.ToInt32(reader["ID_HOLIDAY"]);
                            holiday.Day = Convert.ToDateTime(reader["HOLIDAY_DAY"]);
                            holiday.Description = Convert.ToString(reader["DESCRIPTION"]);

                            holidays.Add(holiday);
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception xe)
            {
                throw;
            }

            return holidays;
        }
    }
}