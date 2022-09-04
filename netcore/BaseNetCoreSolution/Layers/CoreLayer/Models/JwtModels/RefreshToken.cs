using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Models.BaseModels;
using CoreLayer.Models.IdentityModels;

namespace CoreLayer.Models.JwtModels
{
    public class RefreshToken:BaseModel<int>
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
