using System;
using System.Threading.Tasks;
using CoreLayer.Dtos.UserDtos;
using CoreLayer.IServices;
using CoreLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Mvc;

namespace HostServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegisterDto dto)
        {
            var user = new AppUser();
            var result=await _accountService.RegisterUser(dto);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var result = await _accountService.LoginUser(dto);
            return Ok(result);
        }
    }
}
