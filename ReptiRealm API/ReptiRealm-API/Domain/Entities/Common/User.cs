using Microsoft.AspNetCore.Identity;
using ReptiRealm_API.Domain.Entities;

namespace ReptiRealm_API.Domain.Entities.Common
{
    public class User : IdentityUser
    {
        public virtual ICollection<Reptile> Reptiles { get; set; } = new List<Reptile>();
        public virtual ICollection<Species> Species { get; set; } = new List<Species>();
        public virtual ICollection<FoodType> FoodTypes { get; set; } = new List<FoodType>();
    }
}
