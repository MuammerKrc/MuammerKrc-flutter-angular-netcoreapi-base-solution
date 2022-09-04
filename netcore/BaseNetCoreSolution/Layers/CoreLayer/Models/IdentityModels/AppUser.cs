using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Models.JwtModels;
using Microsoft.AspNetCore.Identity;

namespace CoreLayer.Models.IdentityModels
{
    public class AppUser : IdentityUser<int>
    {
        public RefreshToken? RefreshToken { get; set; }
    }
}
