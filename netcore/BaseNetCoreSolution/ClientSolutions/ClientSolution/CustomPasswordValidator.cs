using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;

namespace ClientSolution
{
    public class CustomPasswordValidator:IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
