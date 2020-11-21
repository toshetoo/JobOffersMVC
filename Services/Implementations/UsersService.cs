using AutoMapper;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories;
using JobOffersMVC.Repositories.Abstraction;
using JobOffersMVC.Services.Abstractions;
using JobOffersMVC.Settings;
using JobOffersMVC.ViewModels.Auth;
using JobOffersMVC.ViewModels.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Services.Implementations
{
    public class UsersService: BaseService<User, UserDetailsVM, UserEditVM>, IUsersService
    {
        private readonly ApplicationSettings settings;
        private readonly IWebHostEnvironment environment;
        private readonly IFileHelperService fileHelperService;
        private IHttpContextAccessor contextAccessor;
        public UsersService(IUsersRepository repo, 
            IMapper mapper, 
            ApplicationSettings settings,
            IWebHostEnvironment environment,
            IFileHelperService fileHelperService,
            IHttpContextAccessor accessor) : base(repo, mapper)
        {
            this.settings = settings;
            this.environment = environment;
            this.fileHelperService = fileHelperService;
            contextAccessor = accessor;
        }
        

        public UserDetailsVM GetByUsernameAndPassword(string username, string password)
        {
            User user = ((IUsersRepository)repository).GetByUsernameAndPassword(username, password);
            return mapper.Map<User, UserDetailsVM>(user);
        }

        public void Register(UserRegisterVM model)
        {
            User u = mapper.Map<User>(model);
            repository.Save(u);
        }

        public void AttachImage(int userId, IFormFile image)
        {
            // int currentUserId = this.contextAccessor.HttpContext.Session.GetInt32("LoggedUserId").Value;
            User u = repository.GetById(userId);
            string fileExtension = image.FileName.Substring(image.FileName.LastIndexOf(".")).ToLower();

            u.ImagePath = $"{u.ID}{fileExtension}";

            string folderPath = Path.Combine(environment.WebRootPath, settings.FileUploadSettings.FileUploadFolder);
            string filePath = fileHelperService.BuildFilePath(folderPath, u.ImagePath);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            fileHelperService.CreateFile(image, filePath);

            repository.Save(u);
        }
    }
}
