
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using M223PunchclockDotnet.Model;

namespace M223PunchclockDotnet.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RSA _privateKey;
        private readonly DatabaseContext _context;

        public AuthController(DatabaseContext context)
        {
            _context = context;

            _privateKey = RSA.Create();
            _privateKey.ImportFromPem(
                System.IO.File.ReadAllText("Keys/private.key"));
        }

        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken(LoginRequest request)
        {
            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(u =>
                    u.UserName == request.UserName &&
                    u.Password == request.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new RsaSecurityKey(_privateKey);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    key, SecurityAlgorithms.RsaSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                Token = tokenHandler.WriteToken(token)
            });
        }
    }

    public class LoginRequest
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
