using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreLayer.Dtos;
using CoreLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ClientSolution.Controllers
{
    public class AccountController : _baseUserController
    {

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager):base(userManager,signInManager)
        {

        }

        public IActionResult Login(string returnUrl)
        {
            TempData["ReturnUrl"] = returnUrl;
            return View(new UserLoginDto());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            string failedExceptionDesc = "Kullanıcı adı veya şifre hatalı";
            bool lockoutUserWhenFailed = false;
            AppUser user;
            if (!ModelState.IsValid)
                return View(dto);

            if (dto.EmailOrUserName.Contains("@"))
                user = await _userManager.FindByEmailAsync(dto.EmailOrUserName);
            else
                user = await _userManager.FindByNameAsync(dto.EmailOrUserName);

            if (user == null)
            {
                _addModelError(failedExceptionDesc);
                return View(dto);
            }

            SignInResult logInResult =
                await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, lockoutUserWhenFailed);

            if (!logInResult.Succeeded)
            {
                _addModelError(failedExceptionDesc);
                return View(dto);
            }

            if (TempData["ReturnUrl"] != null && !string.IsNullOrEmpty(TempData["ReturnUrl"].ToString()))
            {
                return Redirect(TempData["ReturnUrl"].ToString());
            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View(new UserRegisterDto());
        }
      
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var identityResult =await _userManager.CreateAsync(dto.CreateUser(),dto.Password);

            if (!identityResult.Succeeded)
            {
                _addModelError(identityResult.Errors.Select(i => i.Description).ToList());
                return View(dto);
            }

            return RedirectToAction("Login");
        }

        public IActionResult ForgetPassword()
        {
            return View(new ForgotPasswordDto());
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgotPasswordDto model)
        {
            string failedExceptionDesc = "Bu mail adresine bağlı kullanıcı bulunamadı";

            if (!ModelState.IsValid) View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                _addModelError(failedExceptionDesc);
                return View(model);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Action("ResetPasswordConfirm", "Account", new
            {
                user = user.Id,
                token = token

            }, HttpContext.Request.Scheme);

            //will send email;
            model.Success = true;
            return View(model);
        }
        public IActionResult ResetPasswordConfirm(string user, string token)
        {
            var model = new ResetPasswordConfirmDto() { UserId = user, Token = token };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm(ResetPasswordConfirmDto model,string returnUrl)
        {
            string failedExceptionDesc = "Bu mail adresine bağlı kullanıcı bulunamadı";

            if (!ModelState.IsValid) View(model);

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                _addModelError(failedExceptionDesc);
                return View(model);
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.PasswordNew);
            if (!result.Succeeded)
            {
                _addModelError(result.Errors.Select(i => i.Description).ToList());
                return View(model);
            }

            await _userManager.UpdateSecurityStampAsync(user);
            model.Success = true;
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
