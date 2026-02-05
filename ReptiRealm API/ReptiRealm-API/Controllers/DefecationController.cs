using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReptiRealm_API.Data;
using ReptiRealm_API.DTOs;
using ReptiRealm_API.Enums;
using ReptiRealm_API.Entities;
using ReptiRealm_API.Entities.Common;

namespace ReptiRealm_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DefecationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public DefecationController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("{reptileId}")]
        public async Task<IActionResult> GetAll(Guid reptileId)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var reptile = await _context.Reptiles
                .Include(r => r.Defecations)
                .SingleOrDefaultAsync(r => r.Id == reptileId && r.UserId == user!.Id);

            if (reptile == null)
            {
                return NotFound("Reptile not found or owned by user");
            }

            return Ok(reptile.Defecations);
        }

        [HttpPost("Create/{reptileId}")]
        public async Task<IActionResult> Create(Guid reptileId, [FromBody] AddDefecationDto defecationDto)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var reptile = await _context.Reptiles.FirstOrDefaultAsync(r => r.Id == reptileId && r.UserId == user!.Id);
            var defecation = new Defecation
            {
                Date = defecationDto.Date ?? DateTime.UtcNow,
                Type = defecationDto.Type ?? DefecationType.Faeces,
                Notes = defecationDto.Notes,
                ReptileId = reptileId
            };

            if (reptile == null)
            {
                return NotFound("Reptile not found or owned by user");
            }

            _context.Defecations.Add(defecation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAll),
                new { reptileId },
                defecation
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var defecation = await _context.Defecations
                .Include(d => d.Reptile)
                .Where(d => d.Id == id && d.Reptile.UserId == user!.Id)
                .SingleOrDefaultAsync();

            if (defecation == null)
            {
                return NotFound();
            }

            _context.Defecations.Remove(defecation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
