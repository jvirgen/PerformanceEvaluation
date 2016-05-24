using Oracle.ManagedDataAccess.Client;
using PES.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;

namespace PES.Services
{
    public class LocationService
    {
        private PESDBContext dbContext;

        public LocationService()
        {
            dbContext = new PESDBContext();
        }

        //Get all locations
        public List<Period> GetAll()
        {
            List<Period> periods = new List<Period>();
            Period employee = new Period();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string Query = "SELECT ID_LOCATION, " +
                                           "NAME " +
                                           "FROM LOCATION";

                    OracleCommand Comand = new OracleCommand(Query, db);
                    OracleDataReader Read = Comand.ExecuteReader();
                    while (Read.Read())
                    {

                        // Store data in period object 
                        Period period = new Period();
                        period.PeriodId = Convert.ToInt32(Read["ID_LOCATION"]);
                        period.Name = Convert.ToString(Read["NAME"]);

                        periods.Add(period);
                    }
                    db.Close();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return periods;
        }

        // Get a location by id
        public Period GetPeriodById(int id)
        {
            Period period = new Period();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string Query = "SELECT ID_LOCATION," +
                                    "NAME " +
                                    "FROM LOCATION WHERE ID_LOCATION = '" + id + "'";

                    OracleCommand Comand = new OracleCommand(Query, db);
                    OracleDataReader Read = Comand.ExecuteReader();
                    while (Read.Read())
                    {
                        // Store data in a period object 
                        period = new Period();
                        period.PeriodId = Convert.ToInt32(Read["ID_LOCATION"]);
                        period.Name = Convert.ToString(Read["NAME"]);
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return period;
        }
    }
}