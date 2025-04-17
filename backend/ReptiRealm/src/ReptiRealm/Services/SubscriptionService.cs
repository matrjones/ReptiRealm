using ReptiRealm.Models;
using ReptiRealm.Data;
using Microsoft.EntityFrameworkCore;
using ReptiRealm.Data.DAL.Repository;
using ReptiRealm.Authentication;
using ReptiRealm.Data.DAL.WorkUnits;

namespace ReptiRealm.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly SubscriptionWorkUnit workUnit;

        public SubscriptionService(SubscriptionWorkUnit workUnit)
        { 
            this.workUnit = workUnit;
        }

        public async Task<Subscription> GetSubscriptionByUserIdAsync(ApplicationUser user)
        {
            return user.Subscription;
        }

        public async Task<Subscription> CreateSubscriptionAsync(
            ApplicationUser user,
            string stripeCustomerId,
            string stripeSubscriptionId,
            string plan,
            string status,
            DateTime currentPeriodEnd,
            bool cancelAtPeriodEnd)
        {
            var subscription = new Subscription
            {
                StripeCustomerId = stripeCustomerId,
                StripeSubscriptionId = stripeSubscriptionId,
                Plan = plan,
                Status = status,
                CurrentPeriodEnd = currentPeriodEnd,
                CancelAtPeriodEnd = cancelAtPeriodEnd
            };

            workUnit.SubscriptionRepository.Insert(subscription);
            user.Subscription = subscription;
            workUnit.Save();

            return subscription;
        }

        public async Task<Subscription> UpdateSubscriptionAsync(
            ApplicationUser user,
            string status,
            DateTime currentPeriodEnd,
            bool cancelAtPeriodEnd)
        {
            var subscription = user.Subscription;

            if (subscription == null)
            {
                throw new KeyNotFoundException($"Subscription not found for user {user.Name}");
            }

            subscription.Status = status;
            subscription.CurrentPeriodEnd = currentPeriodEnd;
            subscription.CancelAtPeriodEnd = cancelAtPeriodEnd;

            workUnit.SubscriptionRepository.Update(subscription);
            workUnit.Save();

            return subscription;
        }

        public async Task DeleteSubscriptionAsync(ApplicationUser user)
        {
            var subscription = user.Subscription;
            user.Subscription = null;
            workUnit.SubscriptionRepository.Delete(subscription);
            workUnit.Save();
        }
    }
} 