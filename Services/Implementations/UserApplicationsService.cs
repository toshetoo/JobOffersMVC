using AutoMapper;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstraction;
using JobOffersMVC.Services.Abstractions;
using JobOffersMVC.ViewModels.UserApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Services.Implementations
{
    public class UserApplicationsService: BaseService<UserApplication, UserApplicationDetailsVM, UserApplicationEditVM>, IUserApplicationsService
    {
        public UserApplicationsService(IUserApplicationsRepository repo, IMapper mapper): base(repo, mapper)
        {

        }
    }
}
