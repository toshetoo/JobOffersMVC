using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobOffersMVC.Filters;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstraction;
using JobOffersMVC.Services;
using JobOffersMVC.Services.Abstractions;
using JobOffersMVC.ViewModels.JobOffers;
using JobOffersMVC.ViewModels.UserApplications;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    [ServiceFilter(typeof(AuthenticatedFilter))]
    public class UserApplicationsController : Controller
    {
        private readonly IJobOffersService jobOffersService;
        private readonly IUserApplicationsService userApplicationsService;

        public UserApplicationsController(IJobOffersService jobOffersService, IUserApplicationsService userApplicationsService)
        {
            this.jobOffersService = jobOffersService;
            this.userApplicationsService = userApplicationsService;
        }

        public IActionResult Apply(int jobOfferId)
        {

            JobOfferEditVM offer = jobOffersService.GetById(jobOfferId);

            if (offer == null)
            {
                return RedirectToAction("List", "JobOffers");
            }

            this.userApplicationsService.Save(new UserApplicationEditVM
            {
                JobOfferId = offer.ID,
                UserId = AuthService.LoggedUser.ID,
                Status = UserApplicationStatus.Pending
            });

            return RedirectToAction("Details", "JobOffers", new { id = offer.ID });
        }

        public IActionResult Accept(int applicationId)
        {
            UserApplicationEditVM application = userApplicationsService.GetById(applicationId);

            if (application == null)
            {
                return RedirectToAction("List", "JobOffers");
            }

            if (application.Status == UserApplicationStatus.Accepted)
            {
                return RedirectToAction("Details", "JobOffers", new { id = application.JobOfferId });
            }

            application.Status = UserApplicationStatus.Accepted;
            userApplicationsService.Save(application);

            return RedirectToAction("Details", "JobOffers", new { id = application.JobOfferId });
        }

        public IActionResult Reject(int applicationId)
        {
            UserApplicationEditVM application = userApplicationsService.GetById(applicationId);

            if (application == null)
            {
                return RedirectToAction("List", "JobOffers");
            }

            if (application.Status == UserApplicationStatus.Rejected)
            {
                return RedirectToAction("Details", "JobOffers", new { id = application.JobOfferId });
            }

            application.Status = UserApplicationStatus.Rejected;
            userApplicationsService.Save(application);

            return RedirectToAction("Details", "JobOffers", new { id = application.JobOfferId });
        }
    }
}
