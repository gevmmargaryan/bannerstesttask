using Banners.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Banners.Models.ViewModels
{
    public class InsertBannerViewModel : BannerViewModel
    {
        public int Id { get; set; }

        [Required]
        public IFormFile FormFile { get; set; }
    }

    public class UpdateBannerViewModel : BannerViewModel
    {
        [Required]
        public int Id { get; set; }

        public IFormFile? FormFile { get; set; }
    }

    public class ShowBannerViewModel : BannerViewModel
    {
        public int Id { get; set; }

        public IFormFile FormFile { get; set; }
    }
    
    public class BannerViewModel
    {
        [Required]
        public string Title { get; set; }
        public string? ImageURL { get; set; }

        [Required]
        public string LinkURL { get; set; }

        [Required]
        public bool Online { get; set; }

        [Required]
        public int Order { get; set; }

        public bool IsDeleted { get; set; }
        

        public List<BannerStatViewModel>? Statistics { get; set; }
    }
}
