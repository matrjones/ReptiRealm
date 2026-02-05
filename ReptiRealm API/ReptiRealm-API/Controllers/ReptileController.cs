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
    public class ReptileController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        
        public ReptileController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var reptiles = await _context.Reptiles
                .Where(r => r.UserId == user!.Id)
                .Include(r => r.Species)
                .Include(r => r.Morphs)
                .Include(r => r.Feeds)
                .Include(r => r.Sheds)
                .Include(r => r.Weights)
                .Include(r => r.Defecations)
                .ToListAsync();

            return Ok(reptiles);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] AddReptileDto reptileDto)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);

            List<Morph> morphs = new List<Morph>();
            if(reptileDto.MorphIds?.Length > 0)
            {
                morphs = await _context.Morphs.Where(m => reptileDto.MorphIds.Contains(m.Id)).ToListAsync();
            }

            var reptile = new Reptile
            {
                Name = reptileDto.Name,
                Sex = reptileDto.Sex ?? Sex.Unknown,
                DateOfBirth = reptileDto.DateOfBirth,
                SpeciesId = reptileDto.SpeciesId,
                Morphs = morphs
            };
            reptile.UserId = user!.Id;

            _context.Reptiles.Add(reptile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = reptile.Id },
                reptile
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var reptile = await _context.Reptiles
                .Where(r => r.Id == id)
                .Include(r => r.Species)
                .Include(r => r.Morphs)
                .Include(r => r.Feeds)
                .Include(r => r.Sheds)
                .Include(r => r.Weights)
                .Include(r => r.Defecations)
                .SingleOrDefaultAsync();

            if (reptile == null)
                return NotFound();

            return Ok(reptile);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var reptile = await _context.Reptiles
                .Where(r => r.Id == id && r.UserId == user!.Id)
                .SingleOrDefaultAsync();

            if (reptile == null)
            {
                return NotFound();
            }

            _context.Reptiles.Remove(reptile);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
