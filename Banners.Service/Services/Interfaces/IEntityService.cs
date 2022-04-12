using Banners.DAL.Entities.Interfaces;
using Banners.Infrastructure.Structures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Banners.Service.Services
{
    public interface IEntityService<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> AllAsync();
        Task<IEnumerable<T>> GetPaginatedAsync(Pagination pagination);
        Task<T> FindAsync(int entityId);
        Task<T> RemoveAsync(int entityId);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
    }
}
