using AutoMapper;
using JobOffersMVC.Models;
using JobOffersMVC.ViewModels.Auth;
using JobOffersMVC.ViewModels.JobOffers;
using JobOffersMVC.ViewModels.UserApplications;
using JobOffersMVC.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Services.AutoMapper
{
    public class AutoMapperConfiguration: Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<User, UserDetailsVM>();
            CreateMap<User, UserEditVM>();

            CreateMap<JobOffer, JobOfferDetailsVM>()
                .ForMember(detailsVM => detailsVM.CreatorName, model => model.MapFrom(offer => $"{offer.Creator.FirstName} {offer.Creator.LastName}"))
                .ForMember(detailsVM => detailsVM.UserApplications, model => model.MapFrom(offer => offer.UserApplications));
            CreateMap<JobOffer, JobOfferEditVM>();

            CreateMap<UserApplication, UserApplicationEditVM>();
            CreateMap<UserApplication, UserApplicationDetailsVM>()
                .ForMember(detailsVM => detailsVM.ApplicantName, model => model.MapFrom(app => $"{app.User.FirstName} {app.User.LastName}"))
                .ForMember(detailsVm => detailsVm.JobOfferName, model => model.MapFrom(app => app.JobOffer.Title))
                .ForMember(detailsVm => detailsVm.Status, model => model.MapFrom(app => app.Status));

            CreateMap<UserEditVM, User>();
            CreateMap<UserRegisterVM, User>();

            CreateMap<JobOfferEditVM, JobOffer>()
                .ForMember(model => model.CreatorId, vm => vm.MapFrom(editVm => editVm.CreatorId))
                .ForMember(model => model.UserApplications, vm => vm.Ignore())
                .ForMember(model => model.Creator, vm => vm.Ignore());

            CreateMap<UserApplicationEditVM, UserApplication>();
        }
    }
}
