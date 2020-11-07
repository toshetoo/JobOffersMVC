using JobOffersMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Repositories.Abstraction
{
    public interface IJobOffersRepository: IBaseRepository<JobOffer>
    {
        IEnumerable<JobOffer> GetByCreatorId(int id);
        JobOffer GetByIdFull(int id);

        void DeleteAllForJobOffer(int id);
    }
}
