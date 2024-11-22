using DemoTest.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shengyi_WebAPI.Services.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;

        public JwtService(IJwtService jwtService,  IConfiguration configuration)
        {
            _jwtService = jwtService;
            _configuration = configuration;
        }

        public string GenerateJwtStr(int id,string name,string phone)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,id.ToString())
            };

            claims.AddRange(new Claim[]
            {
                new Claim(ClaimTypes.Name,name.ToString()),
                new Claim(ClaimTypes.MobilePhone,phone.ToString())
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expries = DateTime.Now.AddDays(1);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims, expires: expries,
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);


            return jwt;
        }

        public bool VerifyToken(string? token)
        {
            return _jwtService.VerifyToken(token);
        }
    }
}
