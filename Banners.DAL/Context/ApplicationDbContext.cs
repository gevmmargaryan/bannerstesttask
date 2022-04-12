using System;
using System.Collections.Generic;
using System.Text;
using Banners.DAL.Entities;
using Bogus;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Banners.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Banner> Banner { get; set; }
        public DbSet<BannerStat> BannerStat { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            /*new Faker<Banner>()
                .RuleFor(b => b.Title, f => f.Name.FirstName())
                .RuleFor(b => b.ImageURL, f => f.Image.Animals())
                ;

            builder.Entity<Banner>().HasData(
               new Banner
               {
                   Title = "Banner 1",
                   ImageURL = "",
                   LinkURL = "",
                   Online = true,
                   Order = 1,
                   AutoRotate = true
               }
           );*/
        }
        
    }
}
