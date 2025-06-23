using ReptiRealm.Data.DAL.Repository;
using ReptiRealm.Models;

namespace ReptiRealm.Data.DAL.WorkUnits
{
    public class SubscriptionWorkUnit : GenericWorkUnit
    {
        private GenericRepository<Subscription> subscriptionRepository;

        public SubscriptionWorkUnit(ApplicationDbContext context) : base(context)
        {
        }

        public GenericRepository<Subscription> SubscriptionRepository
        {
            get
            {
                if (subscriptionRepository == null)
                {
                    subscriptionRepository = new GenericRepository<Subscription>(_context);
                }
                return subscriptionRepository;
            }
        }
    }
}
