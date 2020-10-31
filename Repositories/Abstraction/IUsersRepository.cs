using JobOffersMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Repositories.Abstraction
{
    public interface IUsersRepository: IBaseRepository<User>
    {
        User GetByUsernameAndPassword(string username, string password);
    }
}
