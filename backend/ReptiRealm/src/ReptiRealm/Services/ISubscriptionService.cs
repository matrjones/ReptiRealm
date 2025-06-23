using ReptiRealm.Authentication;
using ReptiRealm.Models;

namespace ReptiRealm.Services
{
    public interface ISubscriptionService
    {
        Task<Subscription> GetSubscriptionByUserIdAsync(ApplicationUser user);
        Task<Subscription> CreateSubscriptionAsync(ApplicationUser user, string email, string status, string interval, string planName);
        Task<Subscription> UpdateSubscriptionAsync(ApplicationUser user, string status, string interval, string planName);
        Task DeleteSubscriptionAsync(ApplicationUser user);
    }
} 