using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReptiRealm_API.Application.Interfaces.Entity;
using ReptiRealm_API.Domain.DTOs;
using ReptiRealm_API.Domain.Entities;
using ReptiRealm_API.Domain.Entities.Common;
using ReptiRealm_API.Domain.Enums;

namespace ReptiRealm_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReptileController(
        UserManager<User> userManager,
        IEntityService entityService
    ) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IEntityService _entityService = entityService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            if (user == null) return Unauthorized();

            var reptiles = await _entityService.For<Reptile>()
                .GetAll()
                .Where(r => r.UserId == user.Id)
                .Include(r => r.Species)
                .Include(r => r.Morphs)
                .ToListAsync();

            return Ok(reptiles);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] AddReptileDto reptileDto)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            if (user == null) return Unauthorized();

            var morphs = new List<Morph>();
            if (reptileDto.MorphIds?.Any() == true)
            {
                morphs = await _entityService.For<Morph>()
                    .GetAll()
                    .Where(m => reptileDto.MorphIds.Contains(m.Id))
                    .ToListAsync();
            }

            var reptile = new Reptile
            {
                Name = reptileDto.Name,
                Sex = reptileDto.Sex ?? Sex.Unknown,
                DateOfBirth = reptileDto.DateOfBirth,
                SpeciesId = reptileDto.SpeciesId,
                Morphs = morphs,
                UserId = user.Id
            };

            _entityService.For<Reptile>()
                .Add(reptile);

            await _entityService.For<Reptile>().SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = reptile.Id },
                reptile
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var reptile = await _entityService.For<Reptile>()
                .GetAll()
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
            if (user == null) return Unauthorized();

            var reptile = await _entityService.For<Reptile>()
                .GetAll()
                .Where(r => r.Id == id && r.UserId == user.Id)
                .SingleOrDefaultAsync();

            if (reptile == null)
                return NotFound();

            _entityService.For<Reptile>()
                .Delete(reptile);
                
            await _entityService.For<Reptile>().SaveChangesAsync();

            return NoContent();
        }
    }
}
