using ReptiRealm.Models;

namespace ReptiRealm.Services
{
    public interface ISubscriptionService
    {
        Task<Subscription> GetSubscriptionByUserIdAsync(string userId);
        Task<Subscription> CreateSubscriptionAsync(string userId, string stripeCustomerId, string stripeSubscriptionId, string plan, string status, DateTime currentPeriodEnd, bool cancelAtPeriodEnd);
        Task<Subscription> UpdateSubscriptionAsync(string userId, string status, DateTime currentPeriodEnd, bool cancelAtPeriodEnd);
        Task DeleteSubscriptionAsync(string userId);
    }
} 