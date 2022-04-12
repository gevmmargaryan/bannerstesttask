using System.Threading.Tasks;
using System.Collections.Generic;
using Banners.DAL.Context;
using AutoMapper;
using Banners.Models.ViewModels;
using Banners.Infrastructure.Structures;

namespace Banners.Service.Services.Interfaces
{
    public interface IBannerStatService
    {
        Task<List<ShowBannerStatViewModel>> GetPaginatedAsync(Pagination pagination);
        Task<ShowBannerStatViewModel> AddAsync(InsertBannerStatViewModel entity);
    }
}
