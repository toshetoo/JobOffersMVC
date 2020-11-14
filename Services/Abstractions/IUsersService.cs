using JobOffersMVC.Models;
using JobOffersMVC.ViewModels.Auth;
using JobOffersMVC.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Services.Abstractions
{
    public interface IUsersService:IBaseService<User, UserDetailsVM, UserEditVM>
    {
        void Register(UserRegisterVM model);
        UserDetailsVM GetByUsernameAndPassword(string username, string password);
    }
}
