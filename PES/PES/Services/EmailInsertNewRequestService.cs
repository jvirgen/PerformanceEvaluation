using Oracle.ManagedDataAccess.Client;
using PES.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Net;
using PES.ViewModels;
using System.Web.Routing;

namespace PES.Services
{
    public class EmailInsertNewRequestService
    {
        private PESDBContext dbContext = new PESDBContext();
        private SmtpClient client;
        public EmailInsertNewRequestService()
        {
            dbContext = new PESDBContext();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            client = new SmtpClient("smtp-mail.outlook.com");
        }


        public bool SendEmail(string email, string subject, string bodyMessage /*, string myfile*/)
        {

            //::::::::::::::::::::::EMAIL:::::::::::::::::::::::::::::::::::::
            MailMessage msje = new MailMessage();
            msje.From = new MailAddress(Globals.SMTPOutlookEmail);
            //to who 
            msje.To.Add(email);
            //Subject
            msje.Subject = subject;
            //Email Body
            msje.Body = bodyMessage;

            //msje.Attachments.Add(new Attachment(myfile));
            //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::



            client.Port = 587;
            client.Credentials = new NetworkCredential(Globals.SMTPOutlookEmail, Globals.SMTPOutlookPass);
            client.EnableSsl = true;
            try
            {
                this.client.Send(msje);
            }
            catch (Exception ex)
            {
                throw;
            }

            return true;
        }

        public bool SendEmails(List<string> emails, string subject, string bodyMessage /*, string myfile*/)
        {
            foreach (var email in emails)
            {
                this.SendEmail(email, subject, bodyMessage/*,  myfile*/ );
            }

            return true;
        }

        public List<InsertNewRequestViewModel> GetEmail(int HeaderRequestId)
        {
            List<InsertNewRequestViewModel> dataRequests;
            InsertNewRequestViewModel data;

            try
            {

                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string query = @"with abc as (SELECT REPLAY_COMMENT , EMAIL , id_manager
                                                FROM PE.VACATION_HEADER_REQ 
                                                INNER JOIN  PE.EMPLOYEE ON  VACATION_HEADER_REQ.ID_EMPLOYEE = EMPLOYEE.ID_EMPLOYEE 
                                                WHERE ID_HEADER_REQ = '" + HeaderRequestId + "' ) " +
                                    "Select abc.REPLAY_COMMENT, abc.EMAIL, emp.EMAIL as \"managerEmail\" " +
                                    "From PE.EMPLOYEE emp, abc " +
                                    "where emp.id_employee = abc.id_manager ";



                    using (OracleCommand command = new OracleCommand(query, db))
                    {
                        OracleDataReader reader = command.ExecuteReader();
                        dataRequests = new List<InsertNewRequestViewModel>();
                        while (reader.Read())
                        {
                            data = new InsertNewRequestViewModel();
                            data.EmployeeEmail = Convert.ToString(reader["EMAIL"]);
                            data.ManagerEmail = Convert.ToString(reader["managerEmail"]);

                            dataRequests.Add(data);
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception xe)
            {
                throw;
            }

            return dataRequests;
        }
    }
}

