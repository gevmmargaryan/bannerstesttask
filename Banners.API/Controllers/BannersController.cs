using AutoMapper;
using Banners.Models.ViewModels;
using Banners.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Banners.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BannersController : ControllerBase
    {
        private readonly ILogger<BannersController> _logger;
        private readonly IBannerStatService _bannerStatService;
        private readonly IMapper _mapper;

        public BannersController(ILogger<BannersController> logger, 
            IBannerStatService bannerStatService
            )
        {
            _logger = logger;
            _bannerStatService = bannerStatService;
        }

        //need validation for banner id and event
        [HttpPost(Name = "StatsEvent")]
        public async Task<IActionResult> StatsEvent(InsertBannerStatViewModel bannerStatViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var showBannerStatViewModel = await _bannerStatService.AddOrUpdateAsync(bannerStatViewModel);
                return Ok(bannerStatViewModel);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(ModelState);
            }
        }
    }
}