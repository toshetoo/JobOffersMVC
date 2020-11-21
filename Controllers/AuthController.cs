using JobOffersMVC.Filters;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories;
using JobOffersMVC.Repositories.Abstraction;
using JobOffersMVC.Services;
using JobOffersMVC.Services.Abstractions;
using JobOffersMVC.ViewModels.Auth;
using JobOffersMVC.ViewModels.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUsersService service;
        public AuthController(IUsersService service)
        {
            this.service = service;
        }

        [ServiceFilter(typeof(NonAuthenticatedFilter))]
        public IActionResult Login()
        {
            return View();
        }

        [ServiceFilter(typeof(NonAuthenticatedFilter))]
        [HttpPost]
        public IActionResult Login(UserLoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserDetailsVM user = service.GetByUsernameAndPassword(model.Username, model.Password);

            if (user != null)
            {
                HttpContext.Session.SetInt32("LoggedUserId", user.ID);
                return RedirectToAction("List", "Users");
            }

            return View(model);
        }

        [ServiceFilter(typeof(NonAuthenticatedFilter))]
        public IActionResult Register()
        {
            return View();
        }

        [ServiceFilter(typeof(NonAuthenticatedFilter))]
        [HttpPost]
        public IActionResult Register(UserRegisterVM model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            service.Register(model);
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
