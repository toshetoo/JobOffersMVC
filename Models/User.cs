﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Models
{
    public class User: BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public virtual List<JobOffer> JobOffers { get; set; }
        public virtual List<UserApplication> UserApplications { get; set; }

    }
}
