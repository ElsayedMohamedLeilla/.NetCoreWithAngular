
using System.Threading.Tasks;
using Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Data;

namespace Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly UsersContext _context;

        public GenericRepository(UsersContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T> SaveAsync(T entity)
        {
            await _context.SaveChangesAsync();
            return entity;
        }


    }
}
