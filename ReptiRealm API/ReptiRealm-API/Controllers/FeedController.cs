using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReptiRealm_API.Application.Interfaces.Entity;
using ReptiRealm_API.Application.Services.Entity;
using ReptiRealm_API.Domain.DTOs;
using ReptiRealm_API.Domain.Entities;
using ReptiRealm_API.Domain.Entities.Common;
using ReptiRealm_API.Domain.Enums;
using ReptiRealm_API.Infrastructure.Data;

namespace ReptiRealm_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FeedController(
        IEntityService entityService
    ) : ControllerBase
    {
        private readonly IEntityService _entityService = entityService;

        [HttpGet("{reptileId}")]
        public async Task<IActionResult> GetAll(Guid reptileId)
        {
            var reptile = await _entityService.For<Reptile>()
                .GetAll()
                .Include(r => r.Feeds)
                .ThenInclude(f => f.FoodType)
                .Include(r => r.Feeds)
                .ThenInclude(f => f.Regurgitation)
                .SingleOrDefaultAsync(r => r.Id == reptileId);

            if (reptile == null)
            {
                return NotFound("Reptile not found or not owned by user");
            }

            return Ok(reptile.Feeds);
        }

        [HttpPost("Create/{reptileId}")]
        public async Task<IActionResult> Create(Guid reptileId, [FromBody] AddFeedDto feedDto)
        {
            var reptile = await _entityService.For<Reptile>()
                .GetByIdAsync(reptileId);

            var feed = new Feed
            {
                Date = feedDto.Date ?? DateTime.UtcNow,
                Amount = feedDto.Amount ?? 1,
                IsEaten = feedDto.IsEaten ?? true,
                Notes = feedDto.Notes,
                FoodTypeId = feedDto.FoodTypeId,
                ReptileId = reptileId
            };

            if (reptile == null)
            {
                return NotFound("Reptile not found or not owned by user");
            }

            await _entityService.For<Feed>()
                .Add(feed);

            return CreatedAtAction(
                nameof(GetAll),
                new { reptileId },
                feed
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var feed = await _entityService.For<Feed>()
                .GetAll()
                .Include(f => f.Reptile)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (feed == null)
            {
                return NotFound();
            }

            await _entityService.For<Feed>()
                .Delete(feed);

            return NoContent();
        }

        [HttpPatch("{id}/regurgitation")]
        public async Task<IActionResult> ToggleRegurgitation(Guid id, [FromBody] AddRegurgitationDto regurgDto)
        {
            var feed = await _entityService.For<Feed>()
                .GetAll()
                .Include(f => f.Regurgitation)
                .SingleOrDefaultAsync(f => f.Id == id);

            if (feed == null)
            {
                return NotFound("Feed not found");
            }

            if (feed.Regurgitation != null)
            {
                await _entityService.For<Regurgitation>()
                    .Delete(feed.Regurgitation);
            }
            else
            {
                var regurgitation = new Regurgitation
                {
                    Notes = regurgDto.Notes
                };

                feed.Regurgitation = regurgitation;
                await _entityService.For<Regurgitation>()
                    .Add(regurgitation);
            }

            return NoContent();
        }
    }
}
