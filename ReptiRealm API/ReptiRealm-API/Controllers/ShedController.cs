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
    public class ShedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ShedController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("{reptileId}")]
        public async Task<IActionResult> GetAll(Guid reptileId)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var reptile = await _context.Reptiles
                .Include(r => r.Sheds)
                .SingleOrDefaultAsync(r => r.Id == reptileId && r.UserId == user!.Id);

            if (reptile == null)
            {
                return NotFound("Reptile not found or owned by user");
            }

            return Ok(reptile.Sheds);
        }

        [HttpPost("Create/{reptileId}")]
        public async Task<IActionResult> Create(Guid reptileId, [FromBody] AddShedDto shedDto)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var reptile = await _context.Reptiles.FirstOrDefaultAsync(r => r.Id == reptileId && r.UserId == user!.Id);
            var shed = new Shed
            {
                Date = shedDto.Date ?? DateTime.UtcNow,
                Rating = shedDto.Rating ?? ShedRating.Good,
                Notes = shedDto.Notes,
                ReptileId = reptileId
            };

            if (reptile == null)
            {
                return NotFound("Reptile not found or owned by user");
            }

            _context.Sheds.Add(shed);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAll),
                new { reptileId },
                shed
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var shed = await _context.Sheds
                .Include(s => s.Reptile)
                .Where(s => s.Id == id && s.Reptile.UserId == user!.Id)
                .SingleOrDefaultAsync();

            if (shed == null)
            {
                return NotFound();
            }

            _context.Sheds.Remove(shed);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
