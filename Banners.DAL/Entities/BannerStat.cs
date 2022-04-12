using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Banners.Infrastructure.Enums;

namespace Banners.DAL.Entities
{
    public class BannerStat : BaseEntity
    {
        [Required]
        [ForeignKey("Banner")]
        public int BannerId { get; set; }
        public DateTime DateTimeOccurred { get; set; }
        public Event Event { get; set; }

        public Banner Banner { get; set; }
    }
}
