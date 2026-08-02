using ReptiRealm_API.Domain.Enums;

namespace ReptiRealm_API.Domain.DTOs
{
    public class AddScheduleDto
    {
        public required ActivityType ActivityType { get; set; }
        public required Recurrence Recurrence { get; set; }
        public required int Frequency { get; set; }
        public required DateTime Start { get; set; }
        public DateTime? End { get; set; }

    }
}
