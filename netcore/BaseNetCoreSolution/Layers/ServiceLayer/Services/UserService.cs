using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Dtos;
using CoreLayer.IServices;
using SharedLayer.Dtos;

namespace ServiceLayer.Services
{
    public class UserService:IUserService
    {
        public Task<QResponse<UserDto>> CreateUserAsync(CreateUserDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task<QResponse<UserDto>> GetUserById(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
