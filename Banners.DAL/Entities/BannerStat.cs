using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Banners.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace Banners.DAL.Entities
{
    public class BannerStat : BaseEntity
    {
        [Required]
        [ForeignKey("Banner")]
        public int BannerId { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        [Range(1, 24)]
        public int Hour { get; set; }
        public int Impressions { get; set; }
        public int Clicks { get; set; }

        public Banner Banner { get; set; }
    }
}
