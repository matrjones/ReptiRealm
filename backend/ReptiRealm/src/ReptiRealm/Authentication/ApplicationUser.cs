using Microsoft.AspNetCore.Identity;
using ReptiRealm.Models;

namespace ReptiRealm.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Reptile>? Reptiles { get; set; }
    }
}
