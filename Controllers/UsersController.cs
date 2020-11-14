using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobOffersMVC.Filters;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories;
using JobOffersMVC.Repositories.Abstraction;
using JobOffersMVC.Services.Abstractions;
using JobOffersMVC.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    [ServiceFilter(typeof(AuthenticatedFilter))]
    public class UsersController : Controller
    {
        private readonly IUsersService service;

        public UsersController(IUsersService service)
        {
            this.service = service;
        }

        public IActionResult List()
        {
            List<UserDetailsVM> users = service.GetAll();
            return View(users);
        }

        public IActionResult Edit(int id)
        {
            var user = service.GetById(id);

            // create
            if (user == null)
            {
                return View(new UserEditVM());
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(UserEditVM model)
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
            var user = service.GetDetails(id);

            if (user == null)
            {
                return RedirectToAction("List");
            }

            return View(user);
        }

        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return RedirectToAction("List");
        }
    }
}
