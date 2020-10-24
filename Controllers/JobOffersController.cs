using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobOffersMVC.Filters;
using JobOffersMVC.Repositories;
using JobOffersMVC.Services;
using JobOffersMVC.ViewModels.JobOffers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;

namespace JobOffersMVC.Controllers
{
    [ServiceFilter(typeof(AuthenticatedFilter))]
    public class JobOffersController : Controller
    {
        public IActionResult List()
        {
            var repo = new JobOffersRepository();
            var items = repo.GetAll();

            var vms = new List<JobOffersListVM>();

            foreach (var item in items)
            {
                vms.Add(new JobOffersListVM()
                {
                    ID = item.ID,
                    Title = item.Title,
                    Description = item.Description,
                    CreatorName = $"{AuthService.LoggedUser.FirstName} {AuthService.LoggedUser.LastName}"
                });
            }

            return View(vms);
        }

        public IActionResult Edit(int id)
        {
            var repo = new JobOffersRepository();
            var item = repo.GetById(id);

            if (item == null)
            {
                return View(new JobOfferEditVM());
            }

            var editVM = new JobOfferEditVM()
            {
                ID = item.ID,
                CreatorId = item.ID,
                Title = item.Title,
                Description = item.Description
            };

            return View(editVM);
        }

        [HttpPost]
        public IActionResult Edit(JobOfferEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var repo = new JobOffersRepository();
            var item = repo.GetById(model.ID);

            if (item == null)
            {
                item = new Models.JobOffer();
                item.CreatorId = AuthService.LoggedUser.ID;
            }

            item.Title = model.Title;
            item.Description = model.Description;
            repo.Save(item);

            return RedirectToAction("List");
        }

        public IActionResult Details(int id)
        {
            var repo = new JobOffersRepository();
            var item = repo.GetById(id);
            
            if (item == null)
            {
                return RedirectToAction("List");
            }

            var detailsVM = new JobOfferDetailsVM()
            {
                ID = item.ID,
                Title = item.Title,
                Description = item.Description,
                CreatorName = $"{AuthService.LoggedUser.FirstName} {AuthService.LoggedUser.LastName}"
            };

            return View(detailsVM);
        }

        public IActionResult Delete(int id)
        {
            var repo = new JobOffersRepository();
            repo.Delete(id);

            return RedirectToAction("List");
        }
    }
}
