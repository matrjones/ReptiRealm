using Microsoft.AspNetCore.Identity;
using ReptiRealm.Models;

namespace ReptiRealm.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public virtual ICollection<Reptile>? Reptiles { get; set; }
        public virtual Subscription? Subscription { get; set; }
    }
}
