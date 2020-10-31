using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace JobOffersMVC.Repositories
{
    public class UsersRepository: BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(JobOffersContext context): base(context)
        {
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            return _dbSet.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
