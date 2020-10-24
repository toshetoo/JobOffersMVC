using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.ViewModels.JobOffers
{
    public class JobOffersListVM
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        // creatorId TODO fix name
        public string CreatorName { get; set; }
    }
}
