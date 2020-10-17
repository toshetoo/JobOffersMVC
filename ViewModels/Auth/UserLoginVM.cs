using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.ViewModels.Auth
{
    public class UserLoginVM
    {
        [Required(ErrorMessage = "Please, input a username.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "The Password is required.")]
        public string Password { get; set; }
    }
}
