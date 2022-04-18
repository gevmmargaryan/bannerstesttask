#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Banners.DAL.Context;
using Banners.DAL.Entities;
using Banners.Models.ViewModels;
using Banners.Service.Services.Implementations;
using Banners.Service.Services.Interfaces;
using Banners.Infrastructure.Structures;
using Microsoft.AspNetCore.Authorization;

namespace Banners.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class BannerStatsController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IBannerStatService _bannerStatService;

        public BannerStatsController(IWebHostEnvironment env, IBannerStatService bannerStatService)
        {
            _env = env;
            _bannerStatService = bannerStatService;
        }


        // GET: Admin/BannerStats
        public async Task<IActionResult> Index([FromQuery]Pagination pagination)
        {
            var banners = await _bannerStatService.GetPaginatedAsync(pagination);
            return View(banners);
        }
    }
}
