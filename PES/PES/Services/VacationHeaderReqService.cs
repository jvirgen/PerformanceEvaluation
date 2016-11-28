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

        public List<VacationHeaderReq> GetAllGeneralVacationHeaderReqByEmployeeId(int employeeId)
        {
            List<VacationHeaderReq> Headers = new List<VacationHeaderReq>();
            VacationHeaderReq Header = new VacationHeaderReq();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string Query = @"SELECT " +
                                        "HE.TITLE, " +
                                        "HE.NO_VAC_DAYS, " +
                                        "HE.NO_UNPAID_DAYS, " +
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
                            Header = new VacationHeaderReq();
                            Header.
                        }
                    }
                    }

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