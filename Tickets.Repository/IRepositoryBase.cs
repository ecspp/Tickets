using System.Threading.Tasks;

namespace Tickets.Repository
{
    public interface IRepositoryBase
    {
         public void Add<T>(T entity) where T : class;


        public void Delete<T>(T entity) where T : class;


        public void DeleteRange<T>(T[] entityArray) where T : class;


        public Task<bool> SaveChangesAsync();

        public void Update<T>(T entity) where T : class;

    }
}