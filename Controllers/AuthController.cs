using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    public class AuthController : Controller
    {
        string connectionString = @"Data Source=localhost;
Initial Catalog=JobOffersDB;
Integrated Security=True;
Connect Timeout=30;
Encrypt=False;
TrustServerCertificate=False;
ApplicationIntent=ReadWrite;
MultiSubnetFailover=False";

        public AuthController()
        {
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User model)
        {
            UsersRepository repo = new UsersRepository();
            User user = repo.GetByUsernameAndPassword(model.Username, model.Password);

            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            UsersRepository repo = new UsersRepository();
            repo.Insert(model);
            return RedirectToAction("Login");
        }
    }
}
