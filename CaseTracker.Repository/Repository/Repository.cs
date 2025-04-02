using CaseTracker.Data.ContextModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseTracker.Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntityWithKey
    {
        private readonly ApplicationContext _context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(ApplicationContext hospitalContext)
        {
            _context = hospitalContext;
            entities = hospitalContext.Set<T>();
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            return await entities.AnyAsync(s => s.Id == id);
        }

        public T GetById(Guid id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }
        
        //public ApplicationUser GetByUsername(string userName)
        //{
        //    var userData = _context.UserData.FirstOrDefault(x => x.Username.Equals(userName));
        //    return userData;
        //}

        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            SaveChanges();
        }
    }
}
