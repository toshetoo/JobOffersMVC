using System.Collections.Generic;
using JobOffersMVC.Filters;
using JobOffersMVC.Services.Abstractions;
using JobOffersMVC.ViewModels.JobOffers;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    [ServiceFilter(typeof(AuthenticatedFilter))]
    public class JobOffersController : Controller
    {
        private IJobOffersService service;

        public JobOffersController(IJobOffersService service)
        {
            this.service = service;
        }

        public IActionResult List(int? id)
        {
            List<JobOfferDetailsVM> items;
            if (id.HasValue)
            {
                items = service.GetByCreatorId(id.Value);
            } 
            else
            {
                items = service.GetAll();
            }

            return View(items);
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
            var item = service.GetById(id);

            if (item == null)
            {
                return View(new JobOfferEditVM());
            }

            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(JobOfferEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            service.Save(model);

            return RedirectToAction("List");
        }

        public IActionResult Details(int id)
        {
            var item = service.GetDetails(id);
            
            if (item == null)
            {
                return RedirectToAction("List");
            }

            return View(item);
        }

        public IActionResult Delete(int id)
        {
            service.DeleteAllForJobOffer(id);

            return RedirectToAction("List");
        }
    }
}
