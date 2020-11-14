using AutoMapper;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstraction;
using JobOffersMVC.Services.Abstractions;
using JobOffersMVC.ViewModels.JobOffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Services.Implementations
{
    public class JobOffersService: BaseService<JobOffer, JobOfferDetailsVM, JobOfferEditVM>, IJobOffersService
    {
        public JobOffersService(IJobOffersRepository repo, IMapper mapper): base(repo, mapper)
        {

        }

        public void DeleteAllForJobOffer(int id)
        {
            ((IJobOffersRepository)repository).DeleteAllForJobOffer(id);
        }

        public List<JobOfferDetailsVM> GetByCreatorId(int id)
        {
            List<JobOffer> items = ((IJobOffersRepository)repository).GetByCreatorId(id).ToList();
            
            return items.Select(item => mapper.Map<JobOffer, JobOfferDetailsVM>(item)).ToList();
        }
    }
}
