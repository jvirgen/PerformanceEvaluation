using Microsoft.Exchange.WebServices.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PES.Models
{
    public class User
    {
        //Get UserName and Password 
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string Profile, Name;

        //Authentication Office 365
        public bool Authentication(string username, string pass)
        {
            try
            {
                ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2013);
                service.Credentials = new WebCredentials(username, pass, null);
                service.Url = new Uri("https://outlook.office365.com/ews/exchange.asmx");
                ItemView view = new ItemView(1);
                if (service.FindItems(WellKnownFolderName.Inbox, view).TotalCount >= 0)
                {

                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;

        }

        //Get ID_Profile from the DB 
        public string UserProfile(string UserEmail)
        {
            OracleConnection Connection = new OracleConnection();
            Connection.ConnectionString = "data source=localhost;user id=system;password=4colima";
            Connection.Open();
            string QueryProfile = "SELECT ID_PROFILE FROM PES.EMPLOYEE WHERE EMAIL="+"'"+UserEmail+"'";
            string QueryName = "SELECT FIRST_NAME FROM PES.EMPLOYEE WHERE EMAIL=" + "'" + UserEmail + "'";
            OracleCommand Comand = new OracleCommand(QueryProfile, Connection);
            OracleDataReader Read = Comand.ExecuteReader();
           
            while (Read.Read())
            {
                Profile = Convert.ToString(Read["ID_PROFILE"]); 
            }
            Comand = new OracleCommand(QueryName, Connection);
            Read = Comand.ExecuteReader();
            while (Read.Read())
            {
                Name = Convert.ToString(Read["FIRST_NAME"]);
            }
            
            Connection.Close();

            return Profile;
       }
        
        public enum ProfileUser
        {
            None = 0,
            Resource = 1,
            Manager = 2,
            Director = 3
        }

        //Get User Name to show in the Resource view 
        public string UserName(string UserEmail)
        {
            return Name;
        }
    }
}