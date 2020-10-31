using JobOffersMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Repositories.Abstraction
{
    public interface IBaseRepository<T> where T: BaseModel
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Save(T item);
        void Delete(int id);
    }
}
