using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Exchange.WebServices.Data;

namespace PES.Models
{
    public class User
    {
        [Required]
        [Display (Name= "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name= "Password")]
        public string Password { get; set; }

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
    }
}