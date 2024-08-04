using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository
{
    public class GenericRepository<T, U> : IGenericRepository<T, U> where T : class where U : DbContext
    {
        private readonly U _context;
        public GenericRepository(U context) {
            _context = context;
        }
        public async Task Add(T entity)
        {
            _context.Set<T>().Add(entity);
           await _context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
           _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAll(Dictionary<string, string>? fields)
        {
            try
            {
                var query = _context.Set<T>().AsQueryable();
                if (fields != null)
                {
                    foreach (var field in fields.Keys)
                    {
                        query = query.Where(x => EF.Property<string>(x, field) == fields[field]);
                    }

                    return await query.ToListAsync();
                }

                return await _context.Set<T>().ToListAsync();
            }
            catch(Exception ex)
            {
                if (ex is InvalidOperationException)
                {
                    return Enumerable.Empty<T>();
                }
                throw;
            }
        }

        public async Task<T> GetByKey(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByKey(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
