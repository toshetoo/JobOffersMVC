using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobOffersMVC.Filters;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstraction;
using JobOffersMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    [ServiceFilter(typeof(AuthenticatedFilter))]
    public class UserApplicationsController : Controller
    {
        private readonly IJobOffersRepository jobOffersRepository;
        private readonly IUserApplicationsRepository userApplicationsRepository;

        public UserApplicationsController(IJobOffersRepository repo, IUserApplicationsRepository userApplicationsRepository)
        {
            this.jobOffersRepository = repo;
            this.userApplicationsRepository = userApplicationsRepository;
        }

        public IActionResult Apply(int jobOfferId)
        {

            JobOffer offer = jobOffersRepository.GetById(jobOfferId);

            if (offer == null)
            {
                return RedirectToAction("List", "JobOffers");
            }

            this.userApplicationsRepository.Save(new UserApplication
            {
                JobOfferId = offer.ID,
                UserId = AuthService.LoggedUser.ID,
                Status = UserApplicationStatus.Pending
            });

            return RedirectToAction("Details", "JobOffers", new { id = offer.ID });
        }

        public IActionResult Accept(int applicationId)
        {
            UserApplication application = userApplicationsRepository.GetById(applicationId);

            if (application == null)
            {
                return RedirectToAction("List", "JobOffers");
            }

            if (application.Status == UserApplicationStatus.Accepted)
            {
                return RedirectToAction("Details", "JobOffers", new { id = application.JobOfferId });
            }

            application.Status = UserApplicationStatus.Accepted;
            userApplicationsRepository.Save(application);

            return RedirectToAction("Details", "JobOffers", new { id = application.JobOfferId });
        }

        public IActionResult Reject(int applicationId)
        {
            UserApplication application = userApplicationsRepository.GetById(applicationId);

            if (application == null)
            {
                return RedirectToAction("List", "JobOffers");
            }

            if (application.Status == UserApplicationStatus.Rejected)
            {
                return RedirectToAction("Details", "JobOffers", new { id = application.JobOfferId });
            }

            application.Status = UserApplicationStatus.Rejected;
            userApplicationsRepository.Save(application);

            return RedirectToAction("Details", "JobOffers", new { id = application.JobOfferId });
        }
    }
}
