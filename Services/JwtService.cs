using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using food_order_dotnet.Models;
using Microsoft.IdentityModel.Tokens;

namespace food_order_dotnet.Services
{
    public class JwtService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"] ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Userid", user.UserId.ToString()), 
                new Claim("Username", user.Username ?? "")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));
            var expires = int.Parse(_configuration["Jwt:Expires"] ?? "");
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(expires),
                signingCredentials: signIn
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User GetUserData()
        {
            var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            return new User
            {
                UserId = int.Parse(identity?.FindFirst("UserId")?.Value ?? ""),
                Username = identity?.FindFirst("Username")?.Value
            };
        }
    }
}