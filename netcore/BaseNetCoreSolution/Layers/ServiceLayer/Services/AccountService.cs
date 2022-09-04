using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Dtos;
using CoreLayer.Dtos.UserDtos;
using CoreLayer.Exceptions;
using CoreLayer.IServices;
using CoreLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using SharedLayer.Dtos;

namespace ServiceLayer.Services
{
    public class AccountService : IAccountService
    {
        private UserManager<AppUser> _userManager;
        private ITokenService _tokenService;

        public AccountService(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<QResponse<UserTokenDto>> LoginUser(UserLoginDto dto)
        {
            AppUser user;
            if (dto.EmailOrUserName.Contains("@"))
                user = await _userManager.FindByEmailAsync(dto.EmailOrUserName);
            else
                user = await _userManager.FindByNameAsync(dto.EmailOrUserName);

            if (user == null) return QResponse<UserTokenDto>.ErrorResponse("User not found");

            var isCurrentPassword = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!isCurrentPassword) return QResponse<UserTokenDto>.ErrorResponse("User not found");

            return await _tokenService.CreateUserTokenAsync(user);
        }

        public async Task<QResponse<NoResponse>> RegisterUser(UserRegisterDto dto)
        {
            var user = new AppUser();
            var identityResult = await _userManager.CreateAsync(dto.CreateUser(), dto.Password);

            if (!identityResult.Succeeded)
                return QResponse<NoResponse>.ErrorResponse(identityResult.Errors.ToList().Select(i => i.Description)
                    .ToList());
            else
                return QResponse<NoResponse>.SuccessResponse();
        }
    }
}
