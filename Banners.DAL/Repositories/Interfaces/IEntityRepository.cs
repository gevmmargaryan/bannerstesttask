using Banners.DAL.Entities.Interfaces;
using Banners.Infrastructure.Structures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Banners.DAL.Repositories
{
    public interface IEntityRepository<T> where T : class, IEntity
    {
        IQueryable<T> Find(int id);
        IQueryable<T> GetPaginated(Pagination pagination);
        IQueryable<T> All();
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);
        
    }
}
