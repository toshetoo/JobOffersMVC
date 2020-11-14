using AutoMapper;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories;
using JobOffersMVC.Repositories.Abstraction;
using JobOffersMVC.Services.Abstractions;
using JobOffersMVC.ViewModels.Auth;
using JobOffersMVC.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Services.Implementations
{
    public class UsersService: BaseService<User, UserDetailsVM, UserEditVM>, IUsersService
    {
        public UsersService(IUsersRepository repo, IMapper mapper): base(repo, mapper)
        {

        }

        public UserDetailsVM GetByUsernameAndPassword(string username, string password)
        {
            User user = ((IUsersRepository)repository).GetByUsernameAndPassword(username, password);
            return mapper.Map<User, UserDetailsVM>(user);
        }

        public void Register(UserRegisterVM model)
        {
            User u = mapper.Map<User>(model);
            repository.Save(u);
        }
    }
}
