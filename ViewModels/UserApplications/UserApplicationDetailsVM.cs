using JobOffersMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.ViewModels.UserApplications
{
    public class UserApplicationDetailsVM
    {
        public int Id { get; set; }
        public string ApplicantName { get; set; }
        public string JobOfferName { get; set; }

        public UserApplicationStatus Status { get; set; }
    }
}
