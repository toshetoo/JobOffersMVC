using JobOffersMVC.Models;
using JobOffersMVC.Repositories;
using JobOffersMVC.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    public class AuthController : Controller
    {

        public AuthController()
        {
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UsersRepository repo = new UsersRepository();
            User user = repo.GetByUsernameAndPassword(model.Username, model.Password);

            if (user != null)
            {
                return RedirectToAction("List", "Users");
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

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

            UsersRepository repo = new UsersRepository();
            repo.Save(u);
            return RedirectToAction("Login");
        }
    }
}
