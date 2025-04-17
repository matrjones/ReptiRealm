using ReptiRealm.Authentication;
using ReptiRealm.Models;

namespace ReptiRealm.Services
{
    public interface ISubscriptionService
    {
        Task<Subscription> GetSubscriptionByUserIdAsync(ApplicationUser user);
        Task<Subscription> CreateSubscriptionAsync(ApplicationUser user, string stripeCustomerId, string stripeSubscriptionId, string plan, string status, DateTime currentPeriodEnd, bool cancelAtPeriodEnd);
        Task<Subscription> UpdateSubscriptionAsync(ApplicationUser user, string status, DateTime currentPeriodEnd, bool cancelAtPeriodEnd);
        Task DeleteSubscriptionAsync(ApplicationUser user);
    }
} 