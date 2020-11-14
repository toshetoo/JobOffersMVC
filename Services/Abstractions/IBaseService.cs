using JobOffersMVC.Models;
using JobOffersMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Services.Abstractions
{
    public interface IBaseService<TModel, TDetailsVM, TEditVM>
        where TModel: BaseModel
        where TDetailsVM: BaseViewModel
        where TEditVM: BaseViewModel
    {

        List<TDetailsVM> GetAll();

        TDetailsVM GetDetails(int id);
        TEditVM GetById(int id);
        void Insert(TEditVM model);
        void Update(TEditVM model);
        void Save(TEditVM model);
        void Delete(int id);
    }
}
