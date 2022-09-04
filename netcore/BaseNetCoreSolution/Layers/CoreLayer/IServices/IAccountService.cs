using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Dtos;
using CoreLayer.Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;
using SharedLayer.Dtos;

namespace CoreLayer.IServices
{
    public interface IAccountService
    {
        Task<QResponse<UserTokenDto>> LoginUser(UserLoginDto dto);
        Task<QResponse<NoResponse>> RegisterUser(UserRegisterDto dto);
    }
}
