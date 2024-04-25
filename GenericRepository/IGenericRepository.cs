using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository
{
    public interface IGenericRepository<T, U> where T : class where U : class
    {
        Task<T> GetByKey(string id);
        Task<T> GetByKey(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
