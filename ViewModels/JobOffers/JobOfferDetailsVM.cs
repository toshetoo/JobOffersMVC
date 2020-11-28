using JobOffersMVC.ViewModels.UserApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.ViewModels.JobOffers
{
    public class JobOfferDetailsVM: BaseViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatorName { get; set; }

        public bool CanApply { get; set; }

        public bool CanEdit { get; set; }

        public List<UserApplicationDetailsVM> UserApplications { get; set; }
    }
}
