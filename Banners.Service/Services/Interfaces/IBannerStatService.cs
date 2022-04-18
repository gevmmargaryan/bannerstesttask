using System.Threading.Tasks;
using System.Collections.Generic;
using Banners.DAL.Context;
using AutoMapper;
using Banners.Models.ViewModels;
using Banners.Infrastructure.Structures;
using X.PagedList;

namespace Banners.Service.Services.Interfaces
{
    public interface IBannerStatService
    {
        Task<IPagedList<ShowBannerStatViewModel>> GetPaginatedAsync(Pagination pagination);
        Task<ShowBannerStatViewModel> AddOrUpdateAsync(InsertBannerStatViewModel bannerStatViewModel);
    }
}
