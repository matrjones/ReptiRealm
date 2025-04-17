using Microsoft.AspNetCore.Mvc;
using ReptiRealm.Services;
using ReptiRealm.Models;
using System.Security.Claims;

namespace ReptiRealm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public async Task<ActionResult<Subscription>> GetSubscription()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var subscription = await _subscriptionService.GetSubscriptionByUserIdAsync(userId);
            if (subscription == null)
            {
                return NotFound();
            }

            return Ok(subscription);
        }

        [HttpPost]
        public async Task<ActionResult<Subscription>> CreateSubscription([FromBody] CreateSubscriptionRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var subscription = await _subscriptionService.CreateSubscriptionAsync(
                userId,
                request.StripeCustomerId,
                request.StripeSubscriptionId,
                request.Plan,
                request.Status,
                request.CurrentPeriodEnd,
                request.CancelAtPeriodEnd
            );

            return CreatedAtAction(nameof(GetSubscription), new { id = subscription.Id }, subscription);
        }

        [HttpPut]
        public async Task<ActionResult<Subscription>> UpdateSubscription([FromBody] UpdateSubscriptionRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var subscription = await _subscriptionService.UpdateSubscriptionAsync(
                userId,
                request.Status,
                request.CurrentPeriodEnd,
                request.CancelAtPeriodEnd
            );

            return Ok(subscription);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSubscription()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            await _subscriptionService.DeleteSubscriptionAsync(userId);
            return NoContent();
        }
    }

    public class CreateSubscriptionRequest
    {
        public string StripeCustomerId { get; set; }
        public string StripeSubscriptionId { get; set; }
        public string Plan { get; set; }
        public string Status { get; set; }
        public DateTime CurrentPeriodEnd { get; set; }
        public bool CancelAtPeriodEnd { get; set; }
    }

    public class UpdateSubscriptionRequest
    {
        public string Status { get; set; }
        public DateTime CurrentPeriodEnd { get; set; }
        public bool CancelAtPeriodEnd { get; set; }
    }
} 