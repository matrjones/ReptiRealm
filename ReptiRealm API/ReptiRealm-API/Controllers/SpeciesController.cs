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
    public class SpeciesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public SpeciesController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var species = await _context.Species
                .Where(s => s.UserId == user!.Id)
                .ToListAsync();

            return Ok(species);
        }

        [HttpGet("{speciesId}")]
        public async Task<IActionResult> GetById(Guid speciesId)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var species = await _context.Species.SingleOrDefaultAsync(s => s.UserId == user!.Id && s.Id == speciesId);

            return Ok(species);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] AddSpeciesDto speciesDto)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var species = new Species
            {
                UserId = user!.Id,
                Name = speciesDto.Name,
                Notes = speciesDto.Notes
            };

            _context.Species.Add(species);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { speciesId = species.Id },
                species
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var species = await _context.Species
                .Where(s => s.Id == id && s.UserId == user!.Id)
                .SingleOrDefaultAsync();

            if (species == null)

            {
                return NotFound();
            }

            _context.Species.Remove(species);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
