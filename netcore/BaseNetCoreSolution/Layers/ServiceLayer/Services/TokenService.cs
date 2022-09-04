using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Configurations;
using CoreLayer.Dtos.UserDtos;
using CoreLayer.Exceptions;
using CoreLayer.HelperMethods;
using CoreLayer.IRepositories;
using CoreLayer.IServices;
using CoreLayer.IUnitOfWorks;
using CoreLayer.Models.IdentityModels;
using CoreLayer.Models.JwtModels;
using DataLayer.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLayer.Dtos;

namespace ServiceLayer.Services
{
    public class TokenService : ITokenService
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private readonly IUnitOfWork _unitOfWorks;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private TokenOption _tokenOption;

        public TokenService(IUnitOfWork unitOfWorks, IRefreshTokenRepository refreshTokenRepository, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IOptions<TokenOption> tokenOptions)
        {
            _unitOfWorks = unitOfWorks;
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenOption = tokenOptions.Value;
        }
      
        public async Task<QResponse<UserTokenDto>> CreateUserTokenAsync(AppUser user)
        {
            
            var token = CreateToken(user);
            var userRefreshToken = await _refreshTokenRepository.WhereQueryable((x => x.AppUserId == user.Id)).FirstOrDefaultAsync();
            if (userRefreshToken != null)
            {
                userRefreshToken.Token = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }
            else
            {
                _refreshTokenRepository.Add(new RefreshToken()
                {
                    AppUserId = user.Id,
                    Expiration = token.RefreshTokenExpiration,
                    Token = token.RefreshToken,
                });
            }
            await _unitOfWorks.SaveChangeAsync();

            return QResponse<UserTokenDto>.SuccessResponse(token);
        }

        public async Task<QResponse<UserTokenDto>> CreateUserTokenByRefreshToken(string refreshToken)
        {
            var errorResponse = QResponse<UserTokenDto>.ErrorResponse("Refresh token not useable");
            var existRefreshToken = await _refreshTokenRepository.WhereQueryable(i => i.Token == refreshToken)
                .FirstOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return errorResponse;
            }

            if (existRefreshToken.Expiration > DateTime.Now)
            {
                return errorResponse;
            }

            var user = await _userManager.FindByIdAsync(existRefreshToken.Id.ToString());
            if (user == null)
            {
                return errorResponse;
            }
            var token = CreateToken(user);

            existRefreshToken.Expiration = token.RefreshTokenExpiration;
            existRefreshToken.Token = token.RefreshToken;

            await _unitOfWorks.SaveChangeAsync();

            return QResponse<UserTokenDto>.SuccessResponse(token);
        }

        public async Task<QResponse<NoResponse>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _refreshTokenRepository.WhereQueryable(i => i.Token == refreshToken)
                .FirstOrDefaultAsync();

            if (existRefreshToken != null)
            {
                existRefreshToken.Token = string.Empty;
                await _unitOfWorks.SaveChangeAsync();
            }
            return QResponse<NoResponse>.SuccessResponse();
        }
        private UserTokenDto CreateToken(AppUser appUser)
        {

            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
            var symmetricSecurityKey = HelperClass.GetSymmetricSecurityKey(_tokenOption.SecurityKey);
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration, notBefore: DateTime.Now,
                claims: CreateClaimForUser(user: appUser, _tokenOption.Audience),
                signingCredentials: signingCredentials);
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtTokenHandler.WriteToken(jwtSecurityToken);
            var tokenDto = new UserTokenDto()
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration
            };
            return tokenDto;
        }

        private string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        private IEnumerable<Claim> CreateClaimForUser(AppUser user, List<string> audience)
        {
            var userList = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            userList.AddRange(audience.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return userList;
        }

    }
}

