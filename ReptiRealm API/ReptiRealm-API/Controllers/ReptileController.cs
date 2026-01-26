using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ReptiRealm_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReptileController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetSecretReptiles()
        {
            return Ok(new[] { "Ra", "Kaiba", });
        }
    }
}
