using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banners.DAL.Context;
using Banners.DAL.Entities.Interfaces;
using Banners.Infrastructure.Structures;
using Microsoft.EntityFrameworkCore;

namespace Banners.DAL.Repositories.Implementations
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity
    {
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<T> Entities;
        string _errorMessage = string.Empty;

        public EntityRepository(ApplicationDbContext context)
        {
            _context = context;
            Entities = context.Set<T>();
        }
        public async Task<IEnumerable<T>> AllAsync()
        {
            return await Entities.Where(e => !e.IsDeleted).ToListAsync();
        }
        public async Task<IEnumerable<T>> GetPaginatedAsync(Pagination pagination)
        {
            return await Entities.Skip((pagination.Page - 1) * pagination.Count)
                .Take(pagination.Count)
                .ToListAsync();
        }

        public async Task<T> FindAsync(int id)
        {
            return await Entities.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        }
        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            Entities.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> DeleteAsync(int entityId)
        {
            var result = await FindAsync(entityId);
            if (result != null)
            {
                result.IsDeleted = true;
                await UpdateAsync(result);
                return result;
            }
            throw new Exception("Id is not valid");
        }

    }
}
