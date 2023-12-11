using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryCore.Models;
using LibraryCore.UnitOfWork;
using LibraryCore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.IdentityModel.Tokens;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginViewModel user)
        {
            try
            {
                var u = _unitOfWork.UserRepository.GetAll().FirstOrDefault(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password));
                if (u != null)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim("username", u.Username));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, u.UserId.ToString()));
                    claims.Add(new Claim(ClaimTypes.Role, u.UserRole));
                    var jwtToken = GenerateJwtToken(claims);
                    return Ok(new { jwtToken });
                }
            }
            catch (Exception e)
            {
                return NotFound();
            }

            return NotFound();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private string GenerateJwtToken(List<Claim> claims)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"]);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        
        

    }
}
