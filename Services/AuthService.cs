using JobOffersMVC.Models;
using JobOffersMVC.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Services
{
    public class AuthService
    {
        public static UserDetailsVM LoggedUser { get; set; }
    }
}
