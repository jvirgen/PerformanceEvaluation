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

        public bool DeleteHoliday(Holiday holiday)
        {
            bool status = false;
            int holidayId = holiday.HolidayId;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    string query = @"delete
                                 from 
                                    PE.holidays 
                                 where 
                                    ID_HOLIDAY = :Holiday__id;";
                    using (OracleCommand command = new OracleCommand(query, db))
                    {
                        command.Parameters.Add(new OracleParameter("Holiday_id", holiday.HolidayId));

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
            }

            catch (Exception xe)
            {
                throw;
            }
            return status;
        }

        public bool DeleteHoliday(int holidayId)
        {
            bool status = false;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    //string query = @"delete from PE.holidays where ID_HOLIDAY =  '" + holidayId +"'";
                    string query = @"delete from PE.holidays where ID_HOLIDAY = "+ holidayId;
                    using (OracleCommand command = new OracleCommand(query, db))
                    {
                        command.Parameters.Add(new OracleParameter("Holiday_id", OracleDbType.Int32, holidayId, System.Data.ParameterDirection.Input));

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
            }
            catch (Exception xe)
            {
                throw;
            }
            return status;
        }


        public bool CreateHoliday(Holiday holiday)
        {
            bool status = false;
            DateTime date = new DateTime();
            //establish a connection
            try
            {

                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    string query = @"INSERT INTO HOLIDAYS 
                                        ( HOLIDAY_DAY,
                                          DESCRIPTION) 
                                VALUES
                                    (:Holiday,
                                     :Description)";

                    using (OracleCommand command = new OracleCommand(query, db))
                    {
                        command.Parameters.Add(new OracleParameter("Holiday", holiday.InsertDay ));
                        command.Parameters.Add(new OracleParameter("Description", holiday.Description));


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
            }
            catch (Exception xe)
            {
                throw;
            }
            return status;
        }


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