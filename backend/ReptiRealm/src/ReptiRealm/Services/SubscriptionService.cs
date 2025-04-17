using ReptiRealm.Models;
using ReptiRealm.Data;
using Microsoft.EntityFrameworkCore;

namespace ReptiRealm.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Subscription> GetSubscriptionByUserIdAsync(string userId)
        {
            return await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task<Subscription> CreateSubscriptionAsync(
            string userId,
            string stripeCustomerId,
            string stripeSubscriptionId,
            string plan,
            string status,
            DateTime currentPeriodEnd,
            bool cancelAtPeriodEnd)
        {
            var subscription = new Subscription
            {
                UserId = userId,
                StripeCustomerId = stripeCustomerId,
                StripeSubscriptionId = stripeSubscriptionId,
                Plan = plan,
                Status = status,
                CurrentPeriodEnd = currentPeriodEnd,
                CancelAtPeriodEnd = cancelAtPeriodEnd
            };

            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            return subscription;
        }

        public async Task<Subscription> UpdateSubscriptionAsync(
            string userId,
            string status,
            DateTime currentPeriodEnd,
            bool cancelAtPeriodEnd)
        {
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (subscription == null)
            {
                throw new KeyNotFoundException($"Subscription not found for user {userId}");
            }

            subscription.Status = status;
            subscription.CurrentPeriodEnd = currentPeriodEnd;
            subscription.CancelAtPeriodEnd = cancelAtPeriodEnd;

            await _context.SaveChangesAsync();

            return subscription;
        }

        public async Task DeleteSubscriptionAsync(string userId)
        {
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
                await _context.SaveChangesAsync();
            }
        }
    }
} 