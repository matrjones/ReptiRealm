using Microsoft.AspNetCore.Identity;

namespace ReptiRealm_API.Entities.Common
{
    public class User : IdentityUser
    {
        public virtual ICollection<Reptile> Reptiles { get; set; } = new List<Reptile>();
    }
}
