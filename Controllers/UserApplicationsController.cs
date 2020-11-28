using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobOffersMVC.Filters;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstraction;
using JobOffersMVC.Services;
using JobOffersMVC.Services.Abstractions;
using JobOffersMVC.ViewModels.Emails;
using JobOffersMVC.ViewModels.JobOffers;
using JobOffersMVC.ViewModels.UserApplications;
using JobOffersMVC.ViewModels.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    [ServiceFilter(typeof(AuthenticatedFilter))]
    public class UserApplicationsController : Controller
    {
        private readonly IJobOffersService jobOffersService;
        private readonly IUserApplicationsService userApplicationsService;
        private readonly IEmailService emailService;
        private readonly IUsersService usersService;

        public UserApplicationsController(IJobOffersService jobOffersService, 
            IUserApplicationsService userApplicationsService, 
            IEmailService emailService,
            IUsersService usersService)
        {
            this.jobOffersService = jobOffersService;
            this.userApplicationsService = userApplicationsService;
            this.emailService = emailService;
            this.usersService = usersService;
        }

        public IActionResult Apply(int jobOfferId)
        {
            JobOfferEditVM offer = jobOffersService.GetById(jobOfferId);
            UserDetailsVM user = usersService.GetDetails(HttpContext.Session.GetInt32("LoggedUserId").Value);

            if (offer == null)
            {
                return RedirectToAction("List", "JobOffers");
            }

            UserDetailsVM jobOfferCreator = usersService.GetDetails(offer.CreatorId);
            this.userApplicationsService.Save(new UserApplicationEditVM
            {
                JobOfferId = offer.ID,
                UserId = user.ID,
                Status = UserApplicationStatus.Pending
            });

            emailService.SendEmailAsync(new EmailConfig()
            {
                EmailBody = $"{user.FirstName} {user.LastName} applied for your job offer {offer.Title}",
                EmailSubject = "New job offer application",
                ReceiverEmail = jobOfferCreator.Email,
                ReceiverName = $"{jobOfferCreator.FirstName} {jobOfferCreator.LastName}"
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

            JobOfferDetailsVM jobOffer = jobOffersService.GetDetails(application.JobOfferId);
            UserDetailsVM applicant = usersService.GetDetails(application.UserId);

            application.Status = UserApplicationStatus.Accepted;
            userApplicationsService.Save(application);

            emailService.SendEmailAsync(new EmailConfig()
            {
                EmailSubject = "Your application has been accepted.",
                EmailBody = $"Your application for the job offer {jobOffer.Title} has been accepted at {DateTime.Now}",
                ReceiverEmail = applicant.Email,
                ReceiverName = $"{applicant.FirstName} {applicant.LastName}"
            });

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

            JobOfferDetailsVM jobOffer = jobOffersService.GetDetails(application.JobOfferId);
            UserDetailsVM applicant = usersService.GetDetails(application.UserId);

            emailService.SendEmailAsync(new EmailConfig()
            {
                EmailSubject = "Your application has been rejected.",
                EmailBody = $"Your application for the job offer {jobOffer.Title} has been rejected at {DateTime.Now}",
                ReceiverEmail = applicant.Email,
                ReceiverName = $"{applicant.FirstName} {applicant.LastName}"
            });

            return RedirectToAction("Details", "JobOffers", new { id = application.JobOfferId });
        }
    }
}
