using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstraction;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Repositories
{
    public class BaseRepository<T>: IBaseRepository<T> where T: BaseModel
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;
        
        public BaseRepository(JobOffersContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.FirstOrDefault(u => u.ID == id);
        }

        public void Create(T item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }        

        public void Update(T item)
        {
            T element = GetById(item.ID);

            if (element != null)
            {
                _context.Entry(element).State = EntityState.Detached;
            }

            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Save(T item)
        {
            if (item.ID != 0)
            {
                Update(item);
            } 
            else
            {
                Create(item);
            }
        }

        public void Delete(int id)
        {
            var entity = GetById(id);

            if (entity == null) return;

            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
