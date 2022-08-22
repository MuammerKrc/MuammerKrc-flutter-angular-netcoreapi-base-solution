using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;

namespace ClientSolution.Controllers
{
    public class _baseUserController : _baseHelperController
    {
        protected UserManager<AppUser> _userManager;
        protected SignInManager<AppUser> _signInManager;
        protected RoleManager<AppRole> _roleManager;

        public _baseUserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        protected AppUser curentUser =>
            User.Identity.Name != null ? _userManager.FindByEmailAsync(User.Identity.Name).Result : null;
    }
}
