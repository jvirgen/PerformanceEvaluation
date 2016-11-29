using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;

namespace PES.Services
{
    public class VacationHeaderReqService
    {
        private PESDBContext dbContext = new PESDBContext();
        public VacationHeaderReqService()
        {
            dbContext = new PESDBContext();
        }

        public List<VacHeadReqViewModel> GetAllGeneralVacationHeaderReqByEmployeeId(int employeeId)
        {
            List<VacHeadReqViewModel> Headers = new List<VacHeadReqViewModel>();
            VacHeadReqViewModel Header = new VacHeadReqViewModel();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string Query = @"SELECT " +
                                        "HE.ID_HEADER_REQ, " +
                                        "HE.ID_EMPLOYEE, " +
                                        "HE.TITLE, " +
                                        "HE.NO_VAC_DAYS, " +
                                        "HE.ID_REQ_STATUS," +
                                        "SUB.START_DATE, " +
                                        "SUB.END_DATE, " +
                                        "SUB.RETURN_DATE " +
                                    "FROM " +
                                        "VACATION_HEADER_REQ HE " +
                                    "INNER JOIN VACATION_SUBREQ SUB ON HE.ID_HEADER_REQ = SUB.ID_HEADER_REQ " +
                                    "WHERE " +
                                        "HE.ID_EMPLOYEE = :employeeId " +
                                    "ORDER BY HE.ID_HEADER_REQ";
                    using (OracleCommand command = new OracleCommand(Query, db))
                    {
                        command.Parameters.Add(new OracleParameter("employeeId", employeeId));
                        command.ExecuteReader();
                        OracleDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Header = new VacHeadReqViewModel();
                            Header.vacationHeaderReqId = Convert.ToInt32(reader["ID_HEADER_REQ"]);
                            Header.employeeId = Convert.ToInt32(reader["ID_EMPLOYEE"]);
                            Header.title = Convert.ToString(reader["TITLE"]);
                            Header.noVacDays = Convert.ToInt32(reader["NO_VAC_DAYS"]);
                            Header.ReqStatusId = Convert.ToInt32(reader["ID_REQ_STATUS"]);
                            Header.start_date = Convert.ToDateTime(reader["START_DATE"]);
                            Header.end_date = Convert.ToDateTime(reader["END_DATE"]);
                            Header.return_date = Convert.ToDateTime(reader["RETURN_DATE"]);
                            Headers.Add(Header);
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return Headers;
        }
    }
}