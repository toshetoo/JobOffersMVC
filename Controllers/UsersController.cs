using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobOffersMVC.Filters;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories;
using JobOffersMVC.Repositories.Abstraction;
using JobOffersMVC.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    [ServiceFilter(typeof(AuthenticatedFilter))]
    public class UsersController : Controller
    {
        private readonly IUsersRepository _repo;

        public UsersController(IUsersRepository repo)
        {
            _repo = repo;
        }

        public IActionResult List()
        {
            var users = _repo.GetAll();

            var usersVMs = new List<UsersListVM>();

            foreach (var user in users)
            {
                usersVMs.Add(new UsersListVM()
                {
                    Email = user.Email,
                    ID = user.ID,
                    Name = $"{user.FirstName} {user.LastName}",
                    Username = user.Username
                });
            }

            return View(usersVMs);
        }

        public IActionResult Edit(int id)
        {
            var user = _repo.GetById(id);

            // create
            if (user == null)
            {
                return View(new UserEditVM());
            }

            // edit
            var editVM = new UserEditVM()
            {
                ID = user.ID,
                Email = user.Email,
                Password = user.Password,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return View(editVM);
        }

        [HttpPost]
        public IActionResult Edit(UserEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _repo.GetById(model.ID);

            // create
            if (user == null)
                user = new User();

            user.Username = model.Username;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Password = model.Password;
            user.Email = model.Email;

            _repo.Save(user);

            return RedirectToAction("List");
        }

        public IActionResult Details(int id)
        {
            var user = _repo.GetById(id);

            if (user == null)
            {
                return RedirectToAction("List");
            }

            UserDetailsVM model = new UserDetailsVM()
            {
                ID = user.ID,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            _repo.Delete(id);

            return RedirectToAction("List");
        }
    }
}
