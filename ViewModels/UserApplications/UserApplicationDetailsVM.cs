using JobOffersMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.ViewModels.UserApplications
{
    public class UserApplicationDetailsVM: BaseViewModel
    {
        public string ApplicantName { get; set; }
        public string JobOfferName { get; set; }

        public bool CanEdit { get; set; }
        public bool CanAccept { get; set; }
        public UserApplicationStatus Status { get; set; }
    }
}
