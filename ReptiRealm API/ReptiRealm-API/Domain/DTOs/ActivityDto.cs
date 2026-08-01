using ReptiRealm_API.Domain.Entities;
using ReptiRealm_API.Domain.Enums;

namespace ReptiRealm_API.Domain.DTOs
{
    public class ActivityDto
    {
        public required Reptile Reptile { get; set; }
        public ActivityType Type { get; set; }
        public bool IsOverdue { get; set; }
    }
}
