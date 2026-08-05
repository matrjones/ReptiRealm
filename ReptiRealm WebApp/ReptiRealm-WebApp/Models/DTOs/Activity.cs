using ReptiRealm_WebApp.Models.Enums;

namespace ReptiRealm_WebApp.Models.DTOs
{
    public abstract class Activity
    {
        public required Reptile Reptile { get; set; }
        public ActivityType Type { get; set; }
        public ActivityState State { get; set; }
        public bool IsOverdue { get; set; }
    }
}
