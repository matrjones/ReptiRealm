using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReptiRealm_API.Application.Interfaces.Entity;
using ReptiRealm_API.Domain.DTOs;
using ReptiRealm_API.Domain.Entities;

namespace ReptiRealm_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController (
        IEntityService entityService
    ) : ControllerBase
    {
        private readonly IEntityService _entityService = entityService;

        [HttpPost("Create/{reptileId}")]
        public async Task<IActionResult> Create(Guid reptileId, [FromBody] AddScheduleDto scheduleDto)
        {
            var reptile = await _entityService.For<Reptile>()
                .GetByIdAsync(reptileId);

            if (reptile == null)
            {
                return NotFound("Reptile not found or not owned by user");
            }

            var schedule = new Schedule
            {
                ActivityType = scheduleDto.ActivityType,
                RecurrenceType = scheduleDto.Recurrence,
                Frequency = scheduleDto.Frequency,
                Start = scheduleDto.Start,
                End = scheduleDto.End,
                ReptileId = reptileId
            };

            await _entityService.For<Schedule>()
                .Add(schedule);

            return CreatedAtAction(
                nameof(GetById),
                new { id = schedule.Id },
                schedule
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var schedule = await _entityService.For<Schedule>()
                .GetByIdAsync(id);

            return Ok(schedule);
        }
    }
}
