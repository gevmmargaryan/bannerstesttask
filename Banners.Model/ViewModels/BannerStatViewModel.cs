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
    }

    public class BannerStatViewModel
    {
        public int BannerId { get; set; }
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public int Impressions { get; set; }
        public int Clicks { get; set; }
        public Event Event { get; set; }
    }
}
