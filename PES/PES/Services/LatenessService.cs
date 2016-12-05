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
                                    "WHERE \"DATE\" BETWEEN TRUNC(TO_DATE(SYSDATE), 'D') AND TO_DATE(SYSDATE) + 1 AND E.EMAIL = '" + email + "' " +
                                    "ORDER BY \"DATE\" DESC";
                            break;
                        case "month":
                            Query = "SELECT L.ID_LATENESS, L.\"DATE\", E.ID_EMPLOYEE " +
                                    "FROM LATENESS L INNER JOIN EMPLOYEE E " +
                                    "ON L.ID_EMPLOYEE = E.ID_EMPLOYEE " +
                                    "WHERE \"DATE\" BETWEEN TO_DATE(TRUNC(TO_DATE(SYSDATE), 'MM')) AND to_date(SYSDATE) + 1 AND E.EMAIL = '" + email + "' " +
                                    "ORDER BY \"DATE\" DESC";
                            break;
                        case "year":
                            Query = "SELECT L.ID_LATENESS, L.\"DATE\", E.ID_EMPLOYEE " +
                                    "FROM LATENESS L INNER JOIN EMPLOYEE E " +
                                    "ON L.ID_EMPLOYEE = E.ID_EMPLOYEE " +
                                    "WHERE \"DATE\" BETWEEN TO_DATE(TRUNC(TO_DATE(SYSDATE), 'YY')) AND to_date(SYSDATE) + 1 AND E.EMAIL = '" + email + "' " +
                                    "ORDER BY \"DATE\" DESC";
                            break;
                        case "last 5 years":
                            Query = "SELECT L.ID_LATENESS, L.\"DATE\", E.ID_EMPLOYEE " +
                                    "FROM LATENESS L INNER JOIN EMPLOYEE E " +
                                    "ON L.ID_EMPLOYEE = E.ID_EMPLOYEE " +
                                    "WHERE \"DATE\" BETWEEN TO_DATE(ADD_MONTHS(TRUNC(TO_DATE(SYSDATE), 'YY'), -12 * 5)) AND to_date(SYSDATE) + 1 AND E.EMAIL = '"+ email +"' " +
                                    "ORDER BY \"DATE\" DESC";
                            break;
                        default:
                            Query = "SELECT L.ID_LATENESS, L.\"DATE\", E.ID_EMPLOYEE " +
                                   "FROM LATENESS L INNER JOIN EMPLOYEE E " +
                                   "ON L.ID_EMPLOYEE = E.ID_EMPLOYEE " +
                                   "WHERE TRUNC(\"DATE\") = TO_DATE(SYSDATE) AND E.EMAIL = '" + email + "'";
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

         public bool insertLateness(List<Lateness> latenesses)
        {
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    foreach (Lateness l in latenesses)
                    {
                        String date = l.Date.ToString("dd/MM/yyyy")+ " "+l.Time.ToString("H:mm:ss");
                        string InsertQuery = "INSERT INTO lateness(\"DATE\", ID_EMPLOYEE)" +
                                         "VALUES(TO_DATE('"+date+"', 'dd/mm/yyyy hh24:mi:ss'), " +
                                         "(SELECT ID_EMPLOYEE FROM EMPLOYEE WHERE EMAIL='"+l.EmployeeEmail+"'))";

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
    }
}