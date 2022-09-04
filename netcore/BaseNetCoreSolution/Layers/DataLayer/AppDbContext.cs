using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Models.IdentityModels;
using CoreLayer.Models.JwtModels;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>().HasOne(i => i.RefreshToken).WithOne(i => i.AppUser)
                .HasForeignKey<RefreshToken>(i => i.AppUserId);

        }
    }
}
