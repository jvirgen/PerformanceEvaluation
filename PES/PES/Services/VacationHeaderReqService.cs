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

        public VacHeadReqViewModel GetAllVacRequestInfoByVacReqId(int headerId)
        {
            VacHeadReqViewModel header = new VacHeadReqViewModel();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string Query = @"SELECT HE.ID_HEADER_REQ" +
                                           ",HE.ID_EMPLOYEE" +
                                           ",HE.TITLE" + 
                                           ",HE.NO_VAC_DAYS" +
                                           ",HE.COMMENTS" +
                                           ",HE.ID_REQ_STATUS" +
                                           ",HE.REPLAY_COMMENT" +
                                           ",HE.LEAD_NAME" +
                                           ",HE.HAVE_PROJECT" +
                                           ",HE.NO_UNPAID_DAYS" +
                                           ",ST.REQ_STATUS" +
                                           ",SUB.START_DATE" +
                                           ",SUB.END_DATE" +
                                           ",SUB.RETURN_DATE " +
                                    " FROM  VACATION_HEADER_REQ HE" +
                                          ",VACATION_SUBREQ SUB " + 
                                          ", VACATION_REQ_STATUS ST" +
                                    " WHERE HE.ID_HEADER_REQ = SUB.ID_HEADER_REQ " +
                                      " AND HE.ID_HEADER_REQ = :headerId" +
                                      " AND HE.ID_REQ_STATUS = ST.ID_REQ_STATUS" +
                                    " ORDER BY HE.ID_HEADER_REQ";
                    using (OracleCommand command = new OracleCommand(Query, db))
                    {
                        command.Parameters.Add(new OracleParameter("headerId", headerId));
                        command.ExecuteReader();
                        OracleDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            header.vacationHeaderReqId = Convert.ToInt32(reader["ID_HEADER_REQ"]);
                            header.employeeId = Convert.ToInt32(reader["ID_EMPLOYEE"]);                            
                            header.title = Convert.ToString(reader["TITLE"]);
                            header.noVacDays = Convert.ToInt32(reader["NO_VAC_DAYS"]);
                            header.comments = Convert.ToString(reader["COMMENTS"]);
                            header.ReqStatusId = Convert.ToInt32(reader["ID_REQ_STATUS"]);
                            header.replayComment = Convert.ToString(reader["REPLAY_COMMENT"]);
                            header.lead_name = Convert.ToString(reader["LEAD_NAME"]);
                            header.have_project = Convert.ToChar(reader["HAVE_PROJECT"]);
                            header.noUnpaidDays = Convert.ToInt32(reader["NO_UNPAID_DAYS"]);
                            header.status = Convert.ToString(reader["REQ_STATUS"]);
                            header.start_date = Convert.ToDateTime(reader["START_DATE"]);
                            header.end_date = Convert.ToDateTime(reader["END_DATE"]);
                            header.return_date = Convert.ToDateTime(reader["RETURN_DATE"]);
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return header;
        }
    }
}