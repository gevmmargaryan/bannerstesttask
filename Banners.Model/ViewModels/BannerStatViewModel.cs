using Banners.Infrastructure.Enums;
using Banners.Models.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banners.Models.ViewModels
{
    public class InsertBannerStatViewModel : BannerStatViewModel
    {
    }
    public class ShowBannerStatViewModel : BannerStatViewModel
    {
        [DisplayName("Banner")]
        public BannerViewModel Banner { get; set; }
        public DateTime DateTimeOccurred { get; set; }
    }

    public class BannerStatViewModel
    {
        public int BannerId { get; set; }
        public Event Event { get; set; }

    }
}
