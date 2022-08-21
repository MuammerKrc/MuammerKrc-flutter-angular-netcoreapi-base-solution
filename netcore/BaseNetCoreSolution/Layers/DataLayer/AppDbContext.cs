using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class AppDbContext:IdentityDbContext<AppUser,AppRole,int>
    {
        AppDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
