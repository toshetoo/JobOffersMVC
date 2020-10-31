using JobOffersMVC.Filters;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories;
using JobOffersMVC.Repositories.Abstraction;
using JobOffersMVC.Services;
using JobOffersMVC.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUsersRepository _repo;
        public AuthController(IUsersRepository repo)
        {
            _repo = repo;
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

            User user = _repo.GetByUsernameAndPassword(model.Username, model.Password);

            if (user != null)
            {
                AuthService.LoggedUser = user;
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

            User u = new User()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Username = model.Username
            };

            _repo.Save(u);
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            AuthService.LoggedUser = null;
            return RedirectToAction("Login");
        }
    }
}
