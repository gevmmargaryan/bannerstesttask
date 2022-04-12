using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banners.DAL.Context;
using Banners.DAL.Entities;
using Banners.DAL.Entities.Interfaces;
using Banners.Infrastructure.Structures;
using Microsoft.EntityFrameworkCore;

namespace Banners.DAL.Repositories.Implementations
{
    public class BannerStatRepository : IBannerStatRepository
    {
        private readonly ApplicationDbContext _context;

        public BannerStatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<BannerStat>> AllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BannerStat> DeleteAsync(int entity)
        {
            throw new NotImplementedException();
        }

        public Task<BannerStat> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BannerStat>> GetPaginatedAsync(Pagination pagination)
        {
            return await _context.BannerStat.Skip((pagination.Page - 1) * pagination.Count)
                .Take(pagination.Count)
                .Include(bs => bs.Banner)
                .ToListAsync();
        }

        public Task<BannerStat> InsertAsync(BannerStat entity)
        {
            throw new NotImplementedException();
        }

        public Task<BannerStat> UpdateAsync(BannerStat entity)
        {
            throw new NotImplementedException();
        }
    }
}
