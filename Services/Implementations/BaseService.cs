using AutoMapper;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstraction;
using JobOffersMVC.Services.Abstractions;
using JobOffersMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Services.Implementations
{
    public class BaseService<TModel, TDetailsVM, TEditVM> :IBaseService<TModel, TDetailsVM, TEditVM>
        where TModel : BaseModel
        where TDetailsVM : BaseViewModel
        where TEditVM : BaseViewModel
    {

        protected readonly IBaseRepository<TModel> repository;
        protected readonly IMapper mapper;

        public BaseService(IBaseRepository<TModel> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public List<TDetailsVM> GetAll()
        {
            return repository.GetAll().Select(item => mapper.Map<TModel, TDetailsVM>(item)).ToList();
        }

        public TDetailsVM GetDetails(int id)
        {
            TModel item = repository.GetById(id);
            return mapper.Map<TModel, TDetailsVM>(item);
        }

        public TEditVM GetById(int id)
        {
            TModel item = repository.GetById(id);

            return mapper.Map<TModel, TEditVM>(item);
        }

        public virtual void Insert(TEditVM item)
        {
            TModel model = mapper.Map<TModel>(item);
            repository.Create(model);
        }

        public virtual void Update(TEditVM item)
        {
            TModel model = mapper.Map<TModel>(item);
            repository.Update(model);
        }

        public void Save(TEditVM item)
        {
            if (item.ID != 0)
            {
                Update(item);
            }
            else
            {
                Insert(item);
            }
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
