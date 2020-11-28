using JobOffersMVC.Models;
using JobOffersMVC.ViewModels.JobOffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Services.Abstractions
{
    public interface IJobOffersService: IBaseService<JobOffer, JobOfferDetailsVM, JobOfferEditVM>
    {
        List<JobOfferDetailsVM> GetByCreatorId(int id);

        JobOfferDetailsVM GetDetailsFullById(int id);

        void DeleteAllForJobOffer(int id);
    }
}
