using ReptiRealm.Data.DAL.Repository;
using ReptiRealm.Models;

namespace ReptiRealm.Data.DAL.WorkUnits
{
    public class ReptileWorkUnit : GenericWorkUnit
    {
        private GenericRepository<Reptile> reptileRepository;
        private GenericRepository<Species> speciesRepository;
        private GenericRepository<Morph> morphRepository;

        public ReptileWorkUnit(ApplicationDbContext context) : base(context)
        {
        }

        public GenericRepository<Reptile> ReptileRepository
        {
            get
            {
                if (reptileRepository == null)
                {
                    reptileRepository = new GenericRepository<Reptile>(_context);
                }
                return reptileRepository;
            }
        }

        public GenericRepository<Morph> MorphRepository
        {
            get
            {
                if (morphRepository == null)
                {
                    morphRepository = new GenericRepository<Morph>(_context);
                }
                return morphRepository;
            }
        }

        public GenericRepository<Species> SpeciesRepository
        {
            get
            {
                if (speciesRepository == null)
                {
                    speciesRepository = new GenericRepository<Species>(_context);
                }
                return speciesRepository;
            }
        }
    }
}
