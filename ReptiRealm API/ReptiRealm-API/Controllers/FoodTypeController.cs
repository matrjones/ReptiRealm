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
    public class FoodTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public FoodTypeController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var foodTypes = await _context.FoodTypes
                .Where(f => f.UserId == user!.Id)
                .ToListAsync();

            return Ok(foodTypes);
        }

        [HttpGet("{foodTypeId}")]
        public async Task<IActionResult> GetById(Guid foodTypeId)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var foodType = await _context.FoodTypes.SingleOrDefaultAsync(f => f.UserId == user!.Id && f.Id == foodTypeId);

            return Ok(foodType);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] AddFoodTypeDto foodTypeDto)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var foodType = new FoodType
            {
                UserId = user!.Id,
                Name = foodTypeDto.Name,
                Notes = foodTypeDto.Notes
            };

            _context.FoodTypes.Add(foodType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { foodTypeId = foodType.Id },
                foodType
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
            var foodType = await _context.FoodTypes
                .Where(f => f.Id == id && f.UserId == user!.Id)
                .SingleOrDefaultAsync();

            if (foodType == null)
            {
                return NotFound();
            }

            _context.FoodTypes.Remove(foodType);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
