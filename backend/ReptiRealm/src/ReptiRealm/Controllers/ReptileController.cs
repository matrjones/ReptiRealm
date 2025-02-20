using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReptiRealm.Authentication;
using ReptiRealm.Data.DAL.WorkUnits;
using ReptiRealm.Models;
using ReptiRealm.Services;

namespace ReptiRealm.Controllers
{
    [Route("[Controller]")]
    [Roles(UserRoles.User)]
    public class ReptileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ReptileWorkUnit workUnit;
        private readonly IHangfireService hangfireService;

        public ReptileController(UserManager<ApplicationUser> userManager, ReptileWorkUnit workUnit, IHangfireService hangfireService)
        {
            this.userManager = userManager;
            this.workUnit = workUnit;
            this.hangfireService = hangfireService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                return Ok(user.Reptiles.First(r => r.Id == id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
                var user = await userManager.FindByNameAsync(User!.Identity!.Name!);

                reptile.Morphs = reptile.Morphs?.Select(m => workUnit.MorphRepository.Get(x => x.Name == m.Name).FirstOrDefault() ?? m).ToList();
                reptile.Species = workUnit.SpeciesRepository.Get(x => x.Name == reptile.Species!.Name).FirstOrDefault() ?? reptile.Species;

                workUnit.ReptileRepository.Insert(reptile);
                workUnit.Save();
                user!.Reptiles?.Add(reptile);
                await userManager.UpdateAsync(user!);
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

                reptile.Morphs = reptile.Morphs?.Select(m => workUnit.MorphRepository.Get(x => x.Name == m.Name).FirstOrDefault() ?? m).ToList();
                reptile.Species = workUnit.SpeciesRepository.Get(x => x.Name == reptile.Species!.Name).FirstOrDefault() ?? reptile.Species;

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
