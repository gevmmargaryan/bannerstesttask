using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banners.DAL.Context;
using Banners.DAL.Entities.Interfaces;
using Banners.Infrastructure.Structures;
using Microsoft.EntityFrameworkCore;
using Banners.DAL.Entities;

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
        public IQueryable<T> All()
        {
            return Entities.Where(e => !e.IsDeleted);
        }

        public IQueryable<T> GetPaginated(Pagination pagination)
        {
            return Entities.Where(e => !e.IsDeleted)
                .Skip((pagination.Page - 1) * pagination.Count)
                .Take(pagination.Count);
        }

        public IQueryable<T> Find(int id)
        {
            return Entities.Where(e => e.Id == id && !e.IsDeleted);
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
        public async Task<T> DeleteAsync(int id)
        {
            var result = Find(id).FirstOrDefault();
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