using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> SaveAsync(T entity);
    }
}
