using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Dtos;
using SharedLayer.Dtos;

namespace CoreLayer.IServices
{
    public interface IUserService
    {
        Task<QResponse<UserDto>> CreateUserAsync(CreateUserDto userDto);
        Task<QResponse<UserDto>> GetUserById(string userId);
    }
}
