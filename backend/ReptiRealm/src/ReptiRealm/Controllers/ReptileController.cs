using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReptiRealm.Authentication;
using ReptiRealm.Data.DAL.WorkUnits;
using ReptiRealm.Models;

namespace ReptiRealm.Controllers
{
    [Route("[Controller]")]
    [Roles(UserRoles.User)]
    public class ReptileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ReptileWorkUnit workUnit;
        public ReptileController(UserManager<ApplicationUser> userManager, ReptileWorkUnit workUnit)
        {
            this.userManager = userManager;
            this.workUnit = workUnit;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                return Ok(user.Reptiles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody]Reptile reptile)
        {
            try
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                workUnit.ReptileRepository.Insert(reptile);
                workUnit.Save();
                user.Reptiles.Add(reptile);
                await userManager.UpdateAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] Reptile reptile)
        {
            try
            {
                var user = await userManager.Users
                    .Include(u => u.Reptiles)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

                if (!user.Reptiles.Any(r => r.Id == reptile.Id))
                {
                    return Unauthorized();
                }

                workUnit.ReptileRepository.Update(reptile);
                workUnit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var reptile = workUnit.ReptileRepository.GetByID(id);
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                if(!user.Reptiles.Contains(reptile))
                {
                    return Unauthorized();
                }

                user.Reptiles.Remove(reptile);
                await userManager.UpdateAsync(user);
                workUnit.ReptileRepository.Delete(reptile);
                workUnit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
