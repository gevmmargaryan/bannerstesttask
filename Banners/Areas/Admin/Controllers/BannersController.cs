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
using PagedList;

namespace Banners.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class BannersController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IBannerService _bannerService;

        public BannersController(IWebHostEnvironment env, IBannerService bannerService)
        {
            _env = env;
            _bannerService = bannerService;
        }


        // GET: Admin/Banners
        public async Task<IActionResult> Index([FromQuery]Pagination pagination)
        {
            var banners = await _bannerService.GetPaginatedAsync(pagination);

            return View(banners);
        }

        // GET: Admin/Banners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = new Banner();
            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }

        // GET: Admin/Banners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Image,LinkURL,Online,Order,FormFile")] InsertBannerViewModel bannerViewModel)
        {
            if (ModelState.IsValid)
            {
                await _bannerService.CreateAsync(bannerViewModel, _env.WebRootPath);
                return RedirectToAction(nameof(Index));
            }
            return View(bannerViewModel);
        }

        // GET: Admin/Banners/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var banner = await _bannerService.EditAsync(id);
            if (banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }

        // POST: Admin/Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,Title,ImageURL,LinkURL,Online,Order,AutoRotate,FormFile")] UpdateBannerViewModel bannerViewModel)
        {
            if (id != bannerViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bannerService.UpdateAsync(bannerViewModel, _env.WebRootPath);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerExists(bannerViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bannerViewModel);
        }

        // GET: Admin/Banners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _bannerService.FindAsync((int)id);

            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }

        // POST: Admin/Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bannerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BannerExists(int id)
        {
            return true;
            //return _context.Banner.Any(e => e.Id == id);
        }
    }
}
