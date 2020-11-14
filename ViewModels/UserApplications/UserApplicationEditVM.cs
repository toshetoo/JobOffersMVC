using JobOffersMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.ViewModels.UserApplications
{
    public class UserApplicationEditVM: BaseViewModel
    {
        public int UserId { get; set; }
        public int JobOfferId { get; set; }

        public UserApplicationStatus Status { get; set; }
    }
}
