using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Repositories.Implementations
{
    public class UserApplicationsRepository: BaseRepository<UserApplication>, IUserApplicationsRepository
    {
        public UserApplicationsRepository(JobOffersContext context): base(context)
        {

        }
    }
}
