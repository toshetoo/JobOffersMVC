using AutoMapper;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstraction;
using JobOffersMVC.Services.Abstractions;
using JobOffersMVC.ViewModels.JobOffers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Services.Implementations
{
    public class JobOffersService: BaseService<JobOffer, JobOfferDetailsVM, JobOfferEditVM>, IJobOffersService
    {
        private IHttpContextAccessor contextAccessor;
        public JobOffersService(IJobOffersRepository repo, IMapper mapper, IHttpContextAccessor contextAccessor): base(repo, mapper)
        {
            this.contextAccessor = contextAccessor;
        }

        public override void Insert(JobOfferEditVM item)
        {
            item.CreatorId = contextAccessor.HttpContext.Session.GetInt32("LoggedUserId").Value;
            base.Insert(item);
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
