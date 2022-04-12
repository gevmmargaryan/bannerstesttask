using Banners.DAL.Context;
using Banners.DAL.Entities;
using Banners.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banners.DAL.Repositories.Implementations
{
    public class BannerRepository : EntityRepository<Banner>, IBannerRepository
    {
        public BannerRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
