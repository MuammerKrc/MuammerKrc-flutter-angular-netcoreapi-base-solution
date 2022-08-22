using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace ClientSolution.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            //var baseClaims = new List<Claim>()
            //{
            //    new Claim(ClaimTypes.Name, "Bob"),

            //    new Claim(ClaimTypes.Email, "muammer03karaca@gmail.com"),
            //};
            //var licenseClaims = new List<Claim>()
            //{
            //    new Claim(ClaimTypes.Name, "other"),

            //    new Claim(ClaimTypes.Email, "muammer03karaca@gmail.com"),
            //};
            //var grandmaIdentity = new ClaimsIdentity(baseClaims,"grandma Identity");
            //var licenceIdentity = new ClaimsIdentity(licenseClaims, "licence Identity");
            //var userPrincipal = new ClaimsPrincipal(new []{grandmaIdentity, licenceIdentity });
            //HttpContext.SignInAsync(userPrincipal);


            return View();
        }
    }
}
