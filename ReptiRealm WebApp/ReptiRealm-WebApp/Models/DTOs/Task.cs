using ReptiRealm_WebApp.Models.Enums;

namespace ReptiRealm_WebApp.Models.DTOs
{
    public abstract class Task
    {
        public required Reptile Reptile { get; set; }
        public TaskType Type { get; set; }
        public TaskState State { get; set; }
        public bool IsOverdue { get; set; }
    }
}
