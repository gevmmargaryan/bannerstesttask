using Banners.Models;
using Banners.Service.Services.Interfaces;
using Banners.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Banners.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBannerService _bannerService;

        public HomeController(ILogger<HomeController> logger, IBannerService bannerService)
        {
            _logger = logger;
            _bannerService = bannerService;
        }

        public async Task<IActionResult> Index()
        {
            var banners = new List<ShowBannerViewModel>();
            try
            {
                banners = await _bannerService.AllAsync();
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            
            return View(banners);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}