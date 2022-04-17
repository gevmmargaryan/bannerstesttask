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
    public class BannerStatRepository : EntityRepository<BannerStat>, IBannerStatRepository
    {
       private readonly ApplicationDbContext _context;

        public BannerStatRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<BannerStat> GetPaginatedAsync(Pagination pagination)
        {
            return _context.BannerStat.Skip((pagination.Page - 1) * pagination.Count)
                .Take(pagination.Count)
                .Include(bs => bs.Banner);
        }
    }
}
