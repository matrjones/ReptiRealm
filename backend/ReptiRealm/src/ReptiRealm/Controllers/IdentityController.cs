using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReptiRealm.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ReptiRealm.Controllers
{
    public class IdentityController : Controller
    {
        private readonly ILogger<IdentityController> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        public IdentityController(ILogger<IdentityController> logger, UserManager<ApplicationUser> userManager, IConfiguration configuration) 
        {
            this.logger = logger;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }.Concat(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: HttpContext.Connection.RemoteIpAddress == null ? "Unknown" : HttpContext.Connection.RemoteIpAddress?.ToString(),
                    expires: DateTime.Now.AddDays(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    username = user.UserName,
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var userExists = await userManager.FindByEmailAsync(model.Email);
                if (userExists != null)
                {
                    return BadRequest("User already Exists.");
                }

                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email,
                };

                var result = await userManager.CreateAsync(user, model.Password);
                var newUser = await userManager.FindByEmailAsync(model.Email);
                var roleResult = await userManager.AddToRoleAsync(newUser, UserRoles.User);


                if (!result.Succeeded || !roleResult.Succeeded)
                {
                    return BadRequest("User creation failed! Please check user details and try again.");
                }

                return Ok("User created successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
