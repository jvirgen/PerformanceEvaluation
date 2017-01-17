using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;
using OfficeOpenXml;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace PES.Services
{
    public class LatenessService
    {
        private PESDBContext dbContext;

        public LatenessService()
        {
            dbContext = new PESDBContext();
        }

        public List<Lateness> GetLatenessByEmail(string email, string period)
        {
            List<Lateness> latenesses = new List<Lateness>();
            Lateness lateness = new Lateness();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string Query = "";

                    switch (period)
                    {
                        case "week":
                            Query = "SELECT L.ID_LATENESS, L.\"DATE\", E.ID_EMPLOYEE " +
                                    "FROM LATENESS L INNER JOIN EMPLOYEE E " +
                                    "ON L.ID_EMPLOYEE = E.ID_EMPLOYEE " +
                                    "WHERE \"DATE\" BETWEEN TRUNC(TO_DATE(SYSDATE), 'D') AND TO_DATE(SYSDATE) + 1 AND E.EMAIL = '" + email + "' AND (DELETE_STATUS=0 OR DELETE_STATUS=2) " +
                                    "ORDER BY \"DATE\" DESC";
                            break;
                        case "month":
                            Query = "SELECT L.ID_LATENESS, L.\"DATE\", E.ID_EMPLOYEE " +
                                    "FROM LATENESS L INNER JOIN EMPLOYEE E " +
                                    "ON L.ID_EMPLOYEE = E.ID_EMPLOYEE " +
                                    "WHERE \"DATE\" BETWEEN TO_DATE(TRUNC(TO_DATE(SYSDATE), 'MM')) AND to_date(SYSDATE) + 1 AND E.EMAIL = '" + email + "' AND (DELETE_STATUS=0 OR DELETE_STATUS=2) " +
                                    "ORDER BY \"DATE\" DESC";
                            break;
                        case "year":
                            Query = "SELECT L.ID_LATENESS, L.\"DATE\", E.ID_EMPLOYEE " +
                                    "FROM LATENESS L INNER JOIN EMPLOYEE E " +
                                    "ON L.ID_EMPLOYEE = E.ID_EMPLOYEE " +
                                    "WHERE \"DATE\" BETWEEN TO_DATE(TRUNC(TO_DATE(SYSDATE), 'YY')) AND to_date(SYSDATE) + 1 AND E.EMAIL = '" + email + "' AND (DELETE_STATUS=0 OR DELETE_STATUS=2) " +
                                    "ORDER BY \"DATE\" DESC";
                            break;
                        case "last 5 years":
                            Query = "SELECT L.ID_LATENESS, L.\"DATE\", E.ID_EMPLOYEE " +
                                    "FROM LATENESS L INNER JOIN EMPLOYEE E " +
                                    "ON L.ID_EMPLOYEE = E.ID_EMPLOYEE " +
                                    "WHERE \"DATE\" BETWEEN TO_DATE(ADD_MONTHS(TRUNC(TO_DATE(SYSDATE), 'YY'), -12 * 5)) AND to_date(SYSDATE) + 1 AND E.EMAIL = '" + email + "' AND (DELETE_STATUS=0 OR DELETE_STATUS=2) " +
                                    "ORDER BY \"DATE\" DESC";
                            break;
                        default:
                            Query = "SELECT L.ID_LATENESS, L.\"DATE\", E.ID_EMPLOYEE " +
                                   "FROM LATENESS L INNER JOIN EMPLOYEE E " +
                                   "ON L.ID_EMPLOYEE = E.ID_EMPLOYEE " +
                                   "WHERE TRUNC(\"DATE\") = TO_DATE(SYSDATE) AND E.EMAIL = '" + email + "' AND (DELETE_STATUS=0 OR DELETE_STATUS=2)  ";
                            break;
                    }

                    OracleCommand Comand = new OracleCommand(Query, db);
                    OracleDataReader Read = Comand.ExecuteReader();

                    while (Read.Read())
                    {
                        lateness = new Lateness();
                        lateness.LatenessId = Convert.ToInt32(Read["ID_LATENESS"]);
                        string date = Convert.ToString(Read["DATE"]);
                        lateness.Date = Convert.ToDateTime(date);
                        lateness.EmployeeId = Convert.ToInt32(Read["ID_EMPLOYEE"]);

                        latenesses.Add(lateness);
                    }

                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return latenesses;
        }

        public List<Lateness> GetLatenessByCurrentMonth(int idManager)
        {
            List<Lateness> latenesses = new List<Lateness>();
            Lateness lateness = new Lateness();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string Query = "SELECT MAX(TO_CHAR(\"DATE\", 'DD/MM/YYYY')) AS \"DATE\", E.LAST_NAME || ' ' || E.FIRST_NAME AS \"NAME\", E.EMAIL, COUNT(E.ID_EMPLOYEE) AS NO_LATENESS " +
                                   "FROM LATENESS L INNER JOIN EMPLOYEE E " +
                                   "ON L.ID_EMPLOYEE = E.ID_EMPLOYEE " +
                                   "WHERE \"DATE\" BETWEEN TO_DATE(TRUNC(TO_DATE(SYSDATE), 'MM')) AND to_date(SYSDATE) + 1 AND DELETE_STATUS = 0 AND ID_MANAGER = " + idManager + " " +
                                   "GROUP BY E.LAST_NAME, E.FIRST_NAME, E.EMAIL  ORDER BY \"DATE\" DESC ";

                    OracleCommand Comand = new OracleCommand(Query, db);
                    OracleDataReader Read = Comand.ExecuteReader();

                    while (Read.Read())
                    {
                        lateness = new Lateness();
                        string date = Convert.ToString(Read["DATE"]);
                        lateness.EmployeeEmail = Convert.ToString(Read["EMAIL"]);
                        lateness.EmployeeName = Convert.ToString(Read["NAME"]);
                        lateness.NoLateness = Convert.ToInt32(Read["NO_LATENESS"]);
                        lateness.Date = Convert.ToDateTime(date);
                        latenesses.Add(lateness);
                    }

                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return latenesses;
        }

        public List<Lateness> GetEmployeesByManager(int idManager)
        {
            List<Lateness> latenesses = new List<Lateness>();
            Lateness lateness = new Lateness();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string Query = "SELECT LAST_NAME || ' ' || FIRST_NAME AS \"NAME\", EMAIL " +
                                   "FROM EMPLOYEE " +
                                   "WHERE ID_MANAGER = " + idManager + " " +
                                   "ORDER BY \"NAME\"";

                    OracleCommand Comand = new OracleCommand(Query, db);
                    OracleDataReader Read = Comand.ExecuteReader();

                    while (Read.Read())
                    {
                        lateness = new Lateness();
                        lateness.EmployeeEmail = Convert.ToString(Read["EMAIL"]);
                        lateness.EmployeeName = Convert.ToString(Read["NAME"]);
                        latenesses.Add(lateness);
                    }

                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return latenesses;
        }

        public bool insertLateness(List<Lateness> latenesses)
        {
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    foreach (Lateness l in latenesses)
                    {
                        String date = l.Date.ToString("dd/MM/yyyy") + " " + l.Time.ToString("H:mm:ss");
                        string InsertQuery = "INSERT INTO LATENESS(\"DATE\", ID_EMPLOYEE)" +
                                         "VALUES(TO_DATE('" + date + "', 'dd/mm/yyyy hh24:mi:ss'), " +
                                         "(SELECT ID_EMPLOYEE FROM EMPLOYEE WHERE EMAIL='" + l.EmployeeEmail + "'))";

                        OracleCommand Comand = new OracleCommand(InsertQuery, db);
                        Comand.ExecuteNonQuery();
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }

        public bool isExcelImported(string startWeek, string endWeek)
        {

            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string Query = "SELECT COUNT(*) AS \"MATCH\" FROM LATENESS " + 
                                   "WHERE \"DATE\" BETWEEN '" + startWeek + "' AND '" + endWeek + "'";

                    OracleCommand Comand = new OracleCommand(Query, db);
                    OracleDataReader Read = Comand.ExecuteReader();

                    if (Read.Read())
                    {
                        if (Convert.ToInt16(Read["MATCH"]) > 0)
                        {
                            db.Close();
                            return true;
                        }
                        
                    }

                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return false;
        }

        public bool replaceExcel(List<Lateness> latenesses, string startWeek, string endWeek)
        {
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string DeleteQuery = "DELETE FROM LATENESS WHERE \"DATE\" BETWEEN '" + startWeek + "' AND '" + endWeek + "'";

                    OracleCommand Com = new OracleCommand(DeleteQuery, db);
                    Com.ExecuteNonQuery();

                    foreach (Lateness l in latenesses)
                    {
                        String date = l.Date.ToString("dd/MM/yyyy") + " " + l.Time.ToString("H:mm:ss");
                        string InsertQuery = "INSERT INTO LATENESS(\"DATE\", ID_EMPLOYEE, DELETE_STATUS)" +
                                         "VALUES(TO_DATE('" + date + "', 'dd/mm/yyyy hh24:mi:ss'), " +
                                         "(SELECT ID_EMPLOYEE FROM EMPLOYEE WHERE EMAIL='" + l.EmployeeEmail + "'), 2)";

                        OracleCommand Comand = new OracleCommand(InsertQuery, db);
                        Comand.ExecuteNonQuery();
                    }

                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }
        //********************************AGREGADOOOOO*****************************************************
        public bool delete(int id)
        {
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string UpdateQuery = "UPDATE LATENESS SET DELETE_STATUS = 1 WHERE ID_LATENESS = "  + id;


                    OracleCommand Comand = new OracleCommand(UpdateQuery, db);
                    OracleDataReader Read = Comand.ExecuteReader();
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }
    }
}