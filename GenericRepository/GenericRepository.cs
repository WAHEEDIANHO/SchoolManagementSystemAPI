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

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByKey(string id)
        {
            return await _context.Set<T>().FindAsync(id.ToString());
        }

        public async void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
