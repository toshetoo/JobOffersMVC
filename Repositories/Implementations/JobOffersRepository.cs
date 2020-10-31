using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstraction;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Repositories
{
    public class JobOffersRepository: BaseRepository<JobOffer>, IJobOffersRepository
    {
        public JobOffersRepository(JobOffersContext context): base(context)
        {
        }

        public IEnumerable<JobOffer> GetByCreatorId(int id)
        {
            return _dbSet.Where(jo => jo.CreatorId == id);
        }
    }
}
