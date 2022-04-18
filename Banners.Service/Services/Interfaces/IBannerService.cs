using Banners.DAL.Entities;
using Banners.Infrastructure.Structures;
using Banners.Models.ViewModels;
using X.PagedList;

namespace Banners.Service.Services.Interfaces
{
    public interface IBannerService
    {
        Task<ShowBannerViewModel> CreateAsync(InsertBannerViewModel bannerViewModel, string webRootPath);
        Task<List<ShowBannerViewModel>> AllAsync();
        Task<IPagedList<ShowBannerViewModel>> GetPaginatedAsync(Pagination pagination);
        Task<ShowBannerViewModel> FindAsync(int id);
        Task<UpdateBannerViewModel> EditAsync(int id);
        Task<BannerViewModel> DeleteAsync(int id);
        Task<BannerViewModel> AddAsync(BannerViewModel entity);
        Task<ShowBannerViewModel> UpdateAsync(UpdateBannerViewModel bannerViewModel, string webrootFolder);
    }
}
