using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;

namespace PES.Services
{
    public class VacationSubreqService
    {
        private PESDBContext dbContext = new PESDBContext();
        public VacationSubreqService()
        {
            dbContext = new PESDBContext();
        }

        public List<VacationSubreq> GetVacationSubreqByHeaderReqId(int headerId)
        {
            List<VacationSubreq> vacationsubreqs = new List<VacationSubreq>();
            VacationSubreq vacationsubreq = new VacationSubreq();

            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string Query = "SELECT " +
                                        "ID_SUBREQ, " +
                                        "ID_HEADER_REQ, " +
                                        "START_DATE, " +
                                        "END_DATE," +
                                        "RETURN_DATE, " +
                                        "HAVE_PROJECT, " +
                                        "LEAD_NAME " +
                                    "FROM " +
                                        "VACATION_SUBREQ " +
                                    "WHERE " +
                                        "ID_HEADER_REQ = :headerId " +
                                    "ORDER BY ID_SUBREQ ASC";
                    using (OracleCommand command = new OracleCommand(Query, db))
                    {
                        command.Parameters.Add(new OracleParameter("headerId", headerId));
                        command.ExecuteReader();
                        OracleDataReader Reader = command.ExecuteReader();
                        while (Reader.Read())
                        {
                            vacationsubreq = new VacationSubreq();
                            vacationsubreq.subreqId = Convert.ToInt32(Reader["ID_SUBREQ"]);
                            vacationsubreq.vacationHeaderReqId = Convert.ToInt32(Reader["ID_HEADER_REQ"]);
                            vacationsubreq.startDate = Convert.ToDateTime(Reader["START_DATE"]);
                            vacationsubreq.endDate = Convert.ToDateTime(Reader["END_DATE"]);
                            vacationsubreq.returnDate = Convert.ToDateTime(Reader["RETURN_DATE"]);
                            vacationsubreq.have_project = Convert.ToChar(Reader["HAVE_PROJECT"]);
                            vacationsubreq.lead_name = Convert.ToString(Reader["LEAD_NAME"]);
                            vacationsubreqs.Add(vacationsubreq); 
                        }
                    }

                    db.Close();
                }
            }

            catch (Exception ex)
            {
                throw;
            }

            return vacationsubreqs;
        }
    }
}