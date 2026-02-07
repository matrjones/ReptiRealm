using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReptiRealm_API.Domain.DTOs;
using ReptiRealm_API.Domain.Entities.Common;
using ReptiRealm_API.Domain.Entities;
using ReptiRealm_API.Infrastructure.Data;

namespace ReptiRealm_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MorphController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public MorphController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("GetBySpecies/{speciesId}")]
        public async Task<IActionResult> GetBySpecies(Guid speciesId)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var species = await _context.Species
                .Include(s => s.Morphs)
                .SingleOrDefaultAsync(s => s.UserId == user!.Id && s.Id == speciesId);

            if (species == null)
            {
                return NotFound();
            }

            return Ok(species.Morphs);
        }

        [HttpGet("GetById/{morphId}")]
        public async Task<IActionResult> GetById(Guid morphId)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);

            var morph = await _context.Morphs
                .Include(m => m.Species)
                .Where(m => m.Id == morphId && m.Species.UserId == user!.Id)
                .SingleOrDefaultAsync();

            if (morph == null)
            {
                return NotFound("Morph not found or not owned by user");
            }

            return Ok(morph);
        }

        [HttpPost("Create/{speciesId}")]
        public async Task<IActionResult> Create(Guid speciesId, [FromBody] AddMorphDto morphDto)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var species = await _context.Species.FirstOrDefaultAsync(s => s.Id == speciesId && s.UserId == user!.Id);
            var morph = new Morph
            {
                SpeciesId = speciesId,
                Name = morphDto.Name,
                Notes = morphDto.Notes
            };

            if (species == null)
            {
                return NotFound("Reptile not found or not owned by user");
            }

            _context.Morphs.Add(morph);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { morphId = morph.Id },
                morph
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var morph = await _context.Morphs
                .Include(m => m.Species)
                .Where(m => m.Id == id && m.Species.UserId == user!.Id)
                .SingleOrDefaultAsync();

            if (morph == null)
            {
                return NotFound();
            }

            _context.Morphs.Remove(morph);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
