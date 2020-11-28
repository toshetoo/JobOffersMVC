using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.ViewModels.Emails
{
    public class EmailConfig
    {
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string EmailBody { get; set; }
        public string EmailSubject { get; set; }        
    }
}
