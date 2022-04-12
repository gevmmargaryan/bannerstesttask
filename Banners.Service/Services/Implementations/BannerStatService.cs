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

        public async Task<ShowBannerStatViewModel> AddAsync(InsertBannerStatViewModel bannerStatViewModel)
        {
            BannerStat bannerStat = _mapper.Map<BannerStat>(bannerStatViewModel);
            bannerStat.DateTimeOccurred = DateTime.Now;
            bannerStat = await _bannerStatRepository.InsertAsync(bannerStat);

            return _mapper.Map<ShowBannerStatViewModel>(bannerStat);
        }

        public async Task<List<ShowBannerStatViewModel>> GetPaginatedAsync(Pagination pagination)
        {
            var bannerStats = await _bannerStatRepository.GetPaginatedAsync(pagination);
            return _mapper.Map<List<ShowBannerStatViewModel>>(bannerStats);
        }
    }
}
