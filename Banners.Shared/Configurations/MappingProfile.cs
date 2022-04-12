using AutoMapper;
using Banners.DAL.Entities;
using Banners.Models.ViewModels;

namespace Banners.Shared.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Banner, BannerViewModel>();
            CreateMap<Banner, InsertBannerViewModel>();
            CreateMap<Banner, UpdateBannerViewModel>();
            CreateMap<Banner, ShowBannerViewModel>();
            CreateMap<BannerStat, BannerStatViewModel>();
            CreateMap<BannerStat, InsertBannerStatViewModel>();
            CreateMap<BannerStat, ShowBannerStatViewModel>();

            CreateMap<BannerViewModel, Banner>();
            CreateMap<InsertBannerViewModel, Banner>();
            CreateMap<UpdateBannerViewModel, Banner>();
            CreateMap<ShowBannerViewModel, Banner>();
            CreateMap<BannerStatViewModel, BannerStat>();
            CreateMap<InsertBannerStatViewModel, BannerStat>();
            CreateMap<ShowBannerStatViewModel, BannerStat>();
        }
    }
}
