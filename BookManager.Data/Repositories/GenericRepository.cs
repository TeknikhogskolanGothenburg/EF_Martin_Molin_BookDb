using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManager.Data.Repositories
{
    public abstract class GenericRepository<C, T> where T : class where C : DbContext, new ()
    {
        private C _entities = new C();
        public C Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public virtual List<T> GetAll()
        {
            return _entities.Set<T>().ToList();
        }

        public async virtual Task<List<T>> GetAllAsync()
        {
            return await _entities.Set<T>().ToListAsync();
        }

        public virtual T GetById(int id)
        {
            return _entities.Set<T>().Find(id);
        }

        public async virtual Task<T> GetByIdAsync(int id)
        {
            return await _entities.Set<T>().FindAsync(id);
        }

        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        public async virtual Task AddAsync(T entity)
        {
             await _entities.Set<T>().AddAsync(entity);            
        }

        public virtual void Update(T entity)
        {
            _entities.Set<T>().Update(entity);
        }

        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }

        public async virtual Task SaveAsync()
        {
           await _entities.SaveChangesAsync();
        }
    }
}
