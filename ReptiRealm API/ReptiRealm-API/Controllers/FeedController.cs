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
    public class FeedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public FeedController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("{reptileId}")]
        public async Task<IActionResult> GetAll(Guid reptileId)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var reptile = await _context.Reptiles
                .Include(r => r.Feeds)
                .ThenInclude(f => f.FoodType)
                .Include(r => r.Feeds)
                .ThenInclude(f => f.Regurgitation)
                .SingleOrDefaultAsync(r => r.Id == reptileId && r.UserId == user!.Id);

            if (reptile == null)
            {
                return NotFound("Reptile not found or not owned by user");
            }

            return Ok(reptile.Feeds);
        }

        [HttpPost("Create/{reptileId}")]
        public async Task<IActionResult> Create(Guid reptileId, [FromBody] AddFeedDto feedDto)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var reptile = await _context.Reptiles.FirstOrDefaultAsync(r => r.Id == reptileId && r.UserId == user!.Id);
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

            _context.Feeds.Add(feed);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAll),
                new { reptileId },
                feed
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var feed = await _context.Feeds
                .Include(f => f.Reptile)
                .Where(f => f.Id == id && f.Reptile.UserId == user!.Id)
                .SingleOrDefaultAsync();

            if (feed == null)
            {
                return NotFound();
            }

            _context.Feeds.Remove(feed);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}/regurgitation")]
        public async Task<IActionResult> ToggleRegurgitation(Guid id, [FromBody] AddRegurgitationDto regurgDto)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var feed = await _context.Feeds
                .Include(f => f.Reptile)
                .Include(f => f.Regurgitation)
                .SingleOrDefaultAsync(f => f.Id == id && f.Reptile.UserId == user!.Id);
            
            if (feed == null)
            {
                return NotFound();
            }

            if (feed.Regurgitation != null)
            {
                _context.Regurgitations.Remove(feed.Regurgitation);
            }
            else
            {
                feed.Regurgitation = new Regurgitation
                {
                    Notes = regurgDto.Notes
                };
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
