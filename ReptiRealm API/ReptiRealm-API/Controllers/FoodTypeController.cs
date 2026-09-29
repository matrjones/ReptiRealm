using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReptiRealm_API.Domain.DTOs;
using ReptiRealm_API.Domain.Entities.Common;
using ReptiRealm_API.Domain.Entities;
using ReptiRealm_API.Infrastructure.Data;
using ReptiRealm_API.Application.Interfaces.Entity;

namespace ReptiRealm_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FoodTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IEntityService _entityService;

        public FoodTypeController(ApplicationDbContext context, UserManager<User> userManager, IEntityService entityService)
        {
            _context = context;
            _userManager = userManager;
            _entityService = entityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDistinctAnimalTypes()
        {
            var animalTypes = await _entityService.For<FoodType>()
                .GetAll()
                .Select(x => x.AnimalType)
                .Distinct()
                .ToListAsync();
            
            return Ok(animalTypes);
        }

        [HttpGet("size/{animalType}")]
        public async Task<IActionResult> GetSizesByAnimalType(string animalType)
        {
            var sizesByAnimalType = await _entityService.For<FoodType>()
                .GetAll()
                .Where(x => x.AnimalType == animalType)
                .Select(x => new { x.Id, x.Size })
                .ToListAsync();

            return Ok(sizesByAnimalType);
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
                AnimalType = foodTypeDto.AnimalType,
                Size = foodTypeDto.Size,
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
