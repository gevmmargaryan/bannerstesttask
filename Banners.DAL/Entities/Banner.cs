using System.ComponentModel.DataAnnotations;

namespace Banners.DAL.Entities
{
    public class Banner : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string? ImageURL { get; set; } 

        [Required]
        public string LinkURL { get; set; }

        [Required]
        public bool Online { get; set; }

        [Required]
        public int Order { get; set; }

        public List<BannerStat>? Statistics { get; set; }
    }
}
