using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;
using PES.ViewModels;

namespace PES.Services
{
    public class ResendRequestService 
    {
        private PESDBContext dbContext = new PESDBContext();
        public ResendRequestService()
        {
            dbContext = new PESDBContext();
        }

        public ResendRequest GetRequestInformation(int headerReq)
        {
            ResendRequest resendRequest = new ResendRequest(); 

            try
            {

                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string selectAllHolidays = @"Select TITLE , ID_REQ_STATUS, NO_VAC_DAYS ,  REPLAY_COMMENT, START_DATE , END_DATE , RETURN_DATE, HAVE_PROJECT , LEAD_NAME
                                                FROM PE.VACATION_HEADER_REQ  INNER JOIN PE.VACATION_SUBREQ  ON PE.VACATION_HEADER_REQ.ID_HEADER_REQ = PE.VACATION_SUBREQ.ID_HEADER_REQ
                                                WHERE PE.VACATION_HEADER_REQ.ID_HEADER_REQ = :HeaderReqId";

                    using (OracleCommand command = new OracleCommand(selectAllHolidays, db))
                    {
                        command.Parameters.Add(new OracleParameter("HeaderReqId", headerReq));
                        OracleDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {

                            resendRequest.RequestIdResend   = headerReq;
                            resendRequest.TitleResend       = Convert.ToString(reader["TITLE"]);
                            resendRequest.StatusResend      = Convert.ToString(reader["ID_REQ_STATUS"]);
                            resendRequest.NovacDaysResend   = Convert.ToInt32(reader["NO_VAC_DAYS"]);
                            resendRequest.StartDateResend   = Convert.ToDateTime(reader["START_DATE"]);
                            resendRequest.EndDateResend     = Convert.ToDateTime(reader["END_DATE"]);
                            resendRequest.ReturnDateResend  = Convert.ToDateTime(reader["RETURN_DATE"]);
                            resendRequest.LeadNameResend    = Convert.ToString(reader["LEAD_NAME"]);
                            resendRequest.HaveProjectResend = Convert.ToString(reader["HAVE_PROJECT"]);
                            resendRequest.CommentsResend    = Convert.ToString(reader["REPLAY_COMMENT"]);
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception xe)
            {
                throw;
            }

            return resendRequest;
        }

    }
}