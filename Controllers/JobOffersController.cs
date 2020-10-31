using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobOffersMVC.Filters;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories;
using JobOffersMVC.Repositories.Abstraction;
using JobOffersMVC.Services;
using JobOffersMVC.ViewModels.JobOffers;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;

namespace JobOffersMVC.Controllers
{
    [ServiceFilter(typeof(AuthenticatedFilter))]
    public class JobOffersController : Controller
    {
        private IJobOffersRepository _repo;

        public JobOffersController(IJobOffersRepository repo)
        {
            _repo = repo;
        }

        public IActionResult List(int? id)
        {
            List<JobOffer> items;
            if (id.HasValue)
            {
                items = _repo.GetByCreatorId(id.Value).ToList();
            } 
            else
            {
                items = _repo.GetAll().ToList();
            }
            

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

        //public IActionResult ListForUser()
        //{
        //    var entities = _repo.GetByCreatorId(AuthService.LoggedUser.ID);
        //    var viewModels = new List<JobOffersListVM>();

        //    foreach(var item in entities)
        //    {
        //        viewModels.Add(new JobOffersListVM()
        //        {
        //            ID = item.ID,
        //            Title = item.Title,
        //            Description = item.Description,
        //            CreatorName = $"{AuthService.LoggedUser.FirstName} {AuthService.LoggedUser.LastName}"
        //        });
        //    }

        //    return View(vms);
        //}

        public IActionResult Edit(int id)
        {
            var item = _repo.GetById(id);

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

            var item = _repo.GetById(model.ID);

            if (item == null)
            {
                item = new Models.JobOffer();
                item.CreatorId = AuthService.LoggedUser.ID;
            }

            item.Title = model.Title;
            item.Description = model.Description;
            _repo.Save(item);

            return RedirectToAction("List");
        }

        public IActionResult Details(int id)
        {
            var item = _repo.GetById(id);
            
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
            _repo.Delete(id);

            return RedirectToAction("List");
        }
    }
}
