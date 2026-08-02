using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReptiRealm_API.Application.Interfaces.Entity;
using ReptiRealm_API.Domain.DTOs;
using ReptiRealm_API.Domain.Entities;
using ReptiRealm_API.Domain.Enums;

namespace ReptiRealm_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController(
        IEntityService entityService
    ) : Controller
    {
        private readonly IEntityService _entityService = entityService;

        [HttpGet("Today")]
        public async Task<IActionResult> GetTodayActivities()
        {
            var schedules = await _entityService.For<Schedule>()
                .GetAll()
                .Include(x => x.Reptile)
                .Where(x => x.IsActive)
                .ToListAsync();

            var activities = await GenerateActivities(schedules);

            return Ok(activities.OrderBy(x => x.Reptile.Name));
        }

        private async Task<List<ActivityDto>> GenerateActivities(IEnumerable<Schedule> schedules)
        {
            var activities = new List<ActivityDto>();

            foreach (var group in schedules.GroupBy(x => x.ActivityType))
            {
                var latestActivities = await GetLatestActivityDates(
                    group.Key,
                    group.Select(x => x.ReptileId));

                foreach (var schedule in group)
                {
                    latestActivities.TryGetValue(schedule.ReptileId, out var lastCompleted);
                    var nextDue = schedule.GetNextDueDate(lastCompleted);

                    if (nextDue.Date <= DateTime.Today)
                    {
                        activities.Add(new ActivityDto
                        {
                            Reptile = schedule.Reptile!,
                            Type = schedule.ActivityType,
                            IsOverdue = nextDue.Date < DateTime.Today
                        });
                    }
                }
            }

            return activities;
        }

        private async Task<Dictionary<Guid, DateTime?>> GetLatestActivityDates(
            ActivityType type,
            IEnumerable<Guid> reptileIds)
        {
            return type switch
            {
                ActivityType.Feed =>
                    await _entityService.For<Feed>()
                        .GetAll()
                        .Where(x => reptileIds.Contains(x.ReptileId))
                        .GroupBy(x => x.ReptileId)
                        .Select(x => new
                        {
                            ReptileId = x.Key,
                            Date = x.Max(y => y.Date)
                        })
                        .ToDictionaryAsync(x => x.ReptileId, x => (DateTime?)x.Date),

                ActivityType.Weight =>
                    await _entityService.For<Weight>()
                        .GetAll()
                        .Where(x => reptileIds.Contains(x.ReptileId))
                        .GroupBy(x => x.ReptileId)
                        .Select(x => new
                        {
                            ReptileId = x.Key,
                            Date = x.Max(y => y.Date)
                        })
                        .ToDictionaryAsync(x => x.ReptileId, x => (DateTime?)x.Date),

                _ => new Dictionary<Guid, DateTime?>()
            };
        }
    }
}
