using JobOffersMVC.ViewModels.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Services.Abstractions
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailConfig config);
    }
}
