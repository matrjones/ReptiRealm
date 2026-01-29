using Microsoft.AspNetCore.Identity;

namespace ReptiRealm_API.Entities.Common
{
    public class User : IdentityUser
    {
        public virtual ICollection<Reptile> Reptiles { get; set; } = new List<Reptile>();
        public virtual ICollection<Species> Species { get; set; } = new List<Species>();
        public virtual ICollection<FoodType> FoodTypes { get; set; } = new List<FoodType>();
    }
}
