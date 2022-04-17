using Banners.DAL.Entities.Interfaces;
using Banners.DAL.Repositories;
using Banners.Infrastructure.Structures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Banners.Service.Services
{
    public class EntityService<T> : IEntityService<T> where T : class, IEntity
    {
        private readonly IEntityRepository<T> _entityRepository;
        public EntityService(IEntityRepository<T> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<T> FindAsync(int entityId)
        {
            return await _entityRepository.Find(entityId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> AllAsync()
        {
            return await _entityRepository.All().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetPaginatedAsync(Pagination pagination)
        {
            return await _entityRepository.GetPaginated(pagination).ToListAsync();
        }
        

        public async Task<T> RemoveAsync(int entityId)
        {
            return await _entityRepository.DeleteAsync(entityId);
        }
        public async Task<T> AddAsync(T entity)
        {
            return await _entityRepository.InsertAsync(entity);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            return await _entityRepository.UpdateAsync(entity);
        }
    }
}
