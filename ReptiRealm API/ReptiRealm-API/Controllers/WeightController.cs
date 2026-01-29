using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReptiRealm_API.Data;
using ReptiRealm_API.DTOs;
using ReptiRealm_API.Entities;
using ReptiRealm_API.Entities.Common;

namespace ReptiRealm_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WeightController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public WeightController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("{reptileId}")]
        public async Task<IActionResult> GetAll(Guid reptileId)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var reptile = await _context.Reptiles
                .Include(r => r.Weights)
                .SingleOrDefaultAsync(r => r.Id == reptileId && r.UserId == user!.Id);

            if (reptile == null)
            {
                return NotFound("Reptile not found or owned by user");
            }

            return Ok(reptile.Weights);
        }

        [HttpPost("Create/{reptileId}")]
        public async Task<IActionResult> Create(Guid reptileId, [FromBody] AddWeightDto weightDto)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var reptile = await _context.Reptiles.FirstOrDefaultAsync(r => r.Id == reptileId && r.UserId == user!.Id);
            var weight = new Weight
            {
                Date = weightDto.Date ?? DateTime.UtcNow,
                Value = weightDto.Value,
                Unit = weightDto.Unit ?? "g",
                Notes = weightDto.Notes,
                ReptileId = reptileId
            };

            if (reptile == null)
            {
                return NotFound("Reptile not found or owned by user");
            }

            _context.Weights.Add(weight);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAll),
                new { reptileId },
                weight
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var weight = await _context.Weights
                .Include(w => w.Reptile)
                .Where(w => w.Id == id && w.Reptile.UserId == user!.Id)
                .SingleOrDefaultAsync();

            if (weight == null)
            {
                return NotFound();
            }

            _context.Weights.Remove(weight);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
