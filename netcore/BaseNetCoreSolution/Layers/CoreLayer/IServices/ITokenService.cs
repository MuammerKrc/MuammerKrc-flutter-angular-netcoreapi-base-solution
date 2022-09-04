using CoreLayer.Dtos.UserDtos;
using CoreLayer.Models.IdentityModels;
using SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.IServices
{
    public interface ITokenService
    {
        Task<QResponse<UserTokenDto>> CreateUserTokenAsync(AppUser dto);
        Task<QResponse<UserTokenDto>> CreateUserTokenByRefreshToken(string refreshToken);
        Task<QResponse<NoResponse>> RevokeRefreshToken(string refreshToken);
    }
}
