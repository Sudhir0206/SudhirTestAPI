using ManageEmployeApp.Modle;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManageEmployeApp.Repositories
{
    public class AuthSrices : IAuthSrivces
    {
        private readonly IConfiguration _configuration;

        public AuthSrices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string user)
        {
            
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user),
                    new Claim(ClaimTypes.Email, user),

                };
                var jwtKey = _configuration["Jwt:Key"]
                    ?? throw new InvalidOperationException("JWT key is not configured.");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(
                        _configuration.GetValue<int>("Jwt:ExpiresInMinutes", 60)),
                    signingCredentials: creds);
                return new JwtSecurityTokenHandler().WriteToken(token);
           
        }
    }
}
