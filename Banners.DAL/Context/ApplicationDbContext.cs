using System;
using System.Collections.Generic;
using System.Text;
using Banners.DAL.Entities;
using Bogus;
using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Banners.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Triggers<Banner>.Inserting += entry =>
            {
                Console.WriteLine($"Person: {entry.Entity}");
            };
        }

        public DbSet<Banner> Banner { get; set; }
        public DbSet<BannerStat> BannerStat { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BannerStat>()
                .HasIndex(b => new {b.BannerId, b.Date, b.Hour})
                .IsUnique();

            //Seeding a  'Administrator' role to AspNetRoles table
            builder.Entity<IdentityRole>().HasData(new IdentityRole 
            { 
                Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", 
                Name = "Administrator", 
                NormalizedName = "ADMINISTRATOR".ToUpper() 
            });

            //Seeding the User to AspNetUsers table
            builder.Entity<User>().HasData(
                new User
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                    UserName = "admin",
                    Email = "admin@example.com",
                    NormalizedUserName = "admin",
                    EmailConfirmed = true,
                    PasswordHash = "AFekBrmsAFKTmZk37Ep80MoSsDLKLNLtEWu4PgtdgoLM4/U0rT1hnWwWzTQ4WnIk2Q==" //Pa$$w0rd
                }
            );


            //Seeding the relation between our user and role to AspNetUserRoles table
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                }
            );
        }
        
    }
}
