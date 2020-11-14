using JobOffersMVC.Models;
using JobOffersMVC.ViewModels.UserApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Services.Abstractions
{
    public interface IUserApplicationsService: IBaseService<UserApplication, UserApplicationDetailsVM, UserApplicationEditVM>
    {
    }
}
