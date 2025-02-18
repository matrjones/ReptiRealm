using ReptiRealm.Data.DAL.Repository;
using ReptiRealm.Models;

namespace ReptiRealm.Data.DAL.WorkUnits
{
    public class ReptileWorkUnit : GenericWorkUnit
    {
        private GenericRepository<Reptile> reptileRepository;

        public ReptileWorkUnit(ApplicationDbContext context) : base(context)
        {
        }

        public GenericRepository<Reptile> ReptileRepository
        {
            get
            {
                if(reptileRepository == null)
                {
                    reptileRepository = new GenericRepository<Reptile>(_context);
                }
                return reptileRepository;
            }
        }
    }
}
