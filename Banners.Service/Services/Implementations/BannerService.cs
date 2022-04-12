using AutoMapper;
using Banners.DAL.Entities;
using Banners.DAL.Repositories;
using Banners.DAL.Repositories.Interfaces;
using Banners.Infrastructure.Structures;
using Banners.Service.Services.Interfaces;
using Banners.Shared.Configurations;
using Banners.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Banners.Service.Services.Implementations
{
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;
        private readonly IMapper _mapper;

        public BannerService(IEntityRepository<Banner> entityRepository,
            IBannerRepository bannerRepository,
            IMapper mapper)
        {
            _bannerRepository = bannerRepository;
            _mapper = mapper;
        }

        public async Task<ShowBannerViewModel> CreateAsync(InsertBannerViewModel bannerViewModel, string webrootFolder)
        {
            Banner banner = _mapper.Map<Banner>(bannerViewModel);
            banner.ImageURL = UploadedFile(bannerViewModel.FormFile, webrootFolder);

            banner = await _bannerRepository.InsertAsync(banner);

            return _mapper.Map<ShowBannerViewModel>(banner);
        }

        public async Task<ShowBannerViewModel> UpdateAsync(UpdateBannerViewModel bannerViewModel, string webrootFolder)
        {
            Banner banner = _mapper.Map<Banner>(bannerViewModel);

            if(bannerViewModel.FormFile != null)
            {
                var imagePath = Path.Combine(webrootFolder, Uploads.BannerImagesUrl, bannerViewModel.ImageURL);
                if (bannerViewModel.ImageURL != null && File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
                banner.ImageURL = UploadedFile(bannerViewModel.FormFile, webrootFolder);
            }

            banner = await _bannerRepository.UpdateAsync(banner);

            return _mapper.Map<ShowBannerViewModel>(banner);
        }

        private string UploadedFile(IFormFile formFile, string webrootFolder)
        {
            string uniqueFileName = null;
            string uploadsFolder = Path.Combine(webrootFolder, Uploads.BannerImagesUrl);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }

            return uniqueFileName;
        }

        public  async Task<List<ShowBannerViewModel>> GetPaginatedAsync(Pagination pagination)
        {
            var banners = await _bannerRepository.GetPaginatedAsync(pagination);
            banners = banners.OrderBy(b => b.Order);
            return _mapper.Map<List<ShowBannerViewModel>>(banners);
        }

        public async Task<List<ShowBannerViewModel>> AllAsync()
        {
            var banners = await _bannerRepository.AllAsync();
            return _mapper.Map<List<ShowBannerViewModel>>(banners);
        }

        public async Task<ShowBannerViewModel> FindAsync(int id)
        {
            var banner = await _bannerRepository.FindAsync(id);
            return _mapper.Map<ShowBannerViewModel>(banner);
        }

        public async Task<UpdateBannerViewModel> EditAsync(int id)
        {
            var banner = await _bannerRepository.FindAsync(id);
            return _mapper.Map<UpdateBannerViewModel>(banner);
        }

        public Task<BannerViewModel> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BannerViewModel> AddAsync(BannerViewModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
