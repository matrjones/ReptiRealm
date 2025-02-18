using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReptiRealm.Authentication;

namespace ReptiRealm.Controllers
{
    [Route("[Controller]")]
    [Roles(UserRoles.User)]
    public class ReptileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        public ReptileController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            return Ok(user.Reptiles);
        }
    }
}
