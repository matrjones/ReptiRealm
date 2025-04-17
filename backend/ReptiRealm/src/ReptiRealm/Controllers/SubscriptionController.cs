using Microsoft.AspNetCore.Mvc;
using ReptiRealm.Services;
using ReptiRealm.Models;
using System.Security.Claims;
using ReptiRealm.Authentication;
using Microsoft.AspNetCore.Identity;

namespace ReptiRealm.Controllers
{
    [Route("[Controller]")]
    [Roles(UserRoles.User)]
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly UserManager<ApplicationUser> userManager;

        public SubscriptionController(ISubscriptionService subscriptionService, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            _subscriptionService = subscriptionService;
        }

        [Route("Get")]
        [HttpGet]
        public async Task<ActionResult<Subscription>> GetSubscription()
        {
            try
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);

                var subscription = await _subscriptionService.GetSubscriptionByUserIdAsync(user);
                if (subscription == null)
                {
                    return NotFound();
                }

                return Ok(subscription);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<Subscription>> CreateSubscription([FromBody] CreateSubscriptionRequest request)
        {
            try
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);

                var subscription = await _subscriptionService.CreateSubscriptionAsync(
                    user,
                    request.StripeCustomerId,
                    request.StripeSubscriptionId,
                    request.Plan,
                    request.Status,
                    request.CurrentPeriodEnd,
                    request.CancelAtPeriodEnd
                );

                return CreatedAtAction(nameof(GetSubscription), new { id = subscription.Id }, subscription);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult<Subscription>> UpdateSubscription([FromBody] UpdateSubscriptionRequest request)
        {
            try
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);

                var subscription = await _subscriptionService.UpdateSubscriptionAsync(
                    user,
                    request.Status,
                    request.CurrentPeriodEnd,
                    request.CancelAtPeriodEnd
                );

                return Ok(subscription);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSubscription()
        {
            try
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);

                await _subscriptionService.DeleteSubscriptionAsync(user);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
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