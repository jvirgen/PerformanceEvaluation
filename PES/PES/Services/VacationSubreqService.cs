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

        public bool InsertSubReq(VacationSubreq vacSubReq)
        {
            bool status = false;

            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string query = @"INSERT INTO 
                                    VACATION_SUBREQ 
                                        (ID_HEADER_REQ, 
                                        START_DATE, 
                                        END_DATE, 
                                        RETURN_DATE, 
                                        LEAD_NAME, 
                                        HAVE_PROJECT) 
                                VALUES
                                    (:IdHeaderReq, 
                                    :StartDate, 
                                    :EndDate, 
                                    :ReturnDate, 
                                    :LeadName, 
                                    :HaveProject)";

                using (OracleCommand command = new OracleCommand(query, db))
                {
                    command.Parameters.Add(new OracleParameter("IdHeaderReq", vacSubReq.vacationHeaderReqId));
                    command.Parameters.Add(new OracleParameter("StartDate", vacSubReq.startDate));
                    command.Parameters.Add(new OracleParameter("EndDate", vacSubReq.endDate));
                    command.Parameters.Add(new OracleParameter("ReturnDate", vacSubReq.returnDate));
                    command.Parameters.Add(new OracleParameter("LeadName", vacSubReq.lead_name));
                    command.Parameters.Add(new OracleParameter("HaveProject", vacSubReq.have_project));

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
            return status;
        }
    }
}