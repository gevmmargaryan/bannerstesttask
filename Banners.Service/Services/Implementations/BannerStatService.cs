using System;
using System.Threading.Tasks;
using Banners.DAL.Entities;
using Banners.DAL.Repositories;
using Banners.DAL.Repositories.Interfaces;
using Banners.Service.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Banners.DAL.Context;
using AutoMapper;
using Banners.Models.ViewModels;
using Banners.Infrastructure.Structures;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Banners.Service.Services.Implementations
{
    public class BannerStatService : IBannerStatService
    {
        private readonly IBannerStatRepository _bannerStatRepository;
        private readonly IMapper _mapper;

        public BannerStatService(IBannerStatRepository bannerStatRepository, IMapper mapper)
        {
            _bannerStatRepository = bannerStatRepository;
            _mapper = mapper;
        }

        public async Task<ShowBannerStatViewModel> AddOrUpdateAsync(InsertBannerStatViewModel bannerStatViewModel)
        {
            BannerStat? bannerStat = null;

            var date = DateTime.Now;
            var hour = date.Hour + 1;

            bannerStat = await _bannerStatRepository.All()
               .Where(bs => bs.BannerId == bannerStatViewModel.BannerId)
               .Where(bs => bs.Date == date)
               .Where(bs => bs.Hour == hour)
               .FirstOrDefaultAsync();

            if (bannerStat == null)
            {
                bannerStat = new BannerStat();
                bannerStat.BannerId = bannerStatViewModel.BannerId;
                bannerStat.Date = date;
                bannerStat.Hour = hour;
                bannerStat.IsDeleted = false;
                bannerStat.Impressions = 0;
                bannerStat.Clicks = 0;
            }

            if (bannerStatViewModel.Event == Infrastructure.Enums.Event.Display)
            {
                bannerStat.Impressions += 1;
            }
            else if (bannerStatViewModel.Event == Infrastructure.Enums.Event.Click)
            {
                bannerStat.Clicks += 1;
            }

            bannerStat = await _bannerStatRepository.UpdateAsync(bannerStat);


            return _mapper.Map<ShowBannerStatViewModel>(bannerStat);
        }

        public async Task<IPagedList<ShowBannerStatViewModel>> GetPaginatedAsync(Pagination pagination)
        {
            var bannerStatsCount = _bannerStatRepository.All().Count();

            var bannerStats = await _bannerStatRepository
                .GetPaginated(pagination)
                .Include(bs => bs.Banner)
                .ToListAsync();

            var showBannerStatViewModel = _mapper.Map<IList<ShowBannerStatViewModel>>(bannerStats);

            var showBannerViewModelPaged =
                new StaticPagedList<ShowBannerStatViewModel>(showBannerStatViewModel, pagination.Page,
                pagination.Count, bannerStatsCount);

            return showBannerViewModelPaged;
        }
    }
}
