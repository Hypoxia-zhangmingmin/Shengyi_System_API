using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Shengyi_WebAPI.Models.Out;
using Shengyi_WebAPI.Services.User;
using Shengyi_WebAPI.Models.In;
using Shengyi_WebAPI.Services.Jwt;
using Shengyi_WebAPI.EFCore;

namespace Shengyi_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService )
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginInfo info)
        {
            var result = await _userService.Login(info.Phone, info.Password);
            return result == null ? Ok(new OutputModel<object>().Failed("Login Failed")) : Ok(new OutputModel<object>().Success("Successful"));
        }

        //[HttpGet("token")]
        //public async Task<object> Get(int id ,string name,string phone)
        //{
        //    var token = _jwtService.GenerateJwtStr(id,name,phone);
        //    return new OutputModel<object>().Success(token);
        //}


    }
}
