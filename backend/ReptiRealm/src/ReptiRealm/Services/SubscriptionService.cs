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
            string email,
            string status,
            string interval,
            string planName)
        {
            var subscription = new Subscription
            {
                UserId = user.Id,
                Email = email,
                Status = status,
                Interval = interval,
                PlanName = planName
            };

            workUnit.SubscriptionRepository.Insert(subscription);
            user.Subscription = subscription;
            workUnit.Save();

            return subscription;
        }

        public async Task<Subscription> UpdateSubscriptionAsync(
            ApplicationUser user,
            string status,
            string interval,
            string planName)
        {
            var subscription = user.Subscription;

            if (subscription == null)
            {
                throw new KeyNotFoundException($"Subscription not found for user {user.Name}");
            }

            subscription.Status = status;
            subscription.Interval = interval;
            subscription.PlanName = planName;

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