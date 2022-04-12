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
        Task<T> FindAsync(int id);
        Task<IEnumerable<T>> GetPaginatedAsync(Pagination pagination);
        Task<IEnumerable<T>> AllAsync();
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int entity);
        
    }
}
