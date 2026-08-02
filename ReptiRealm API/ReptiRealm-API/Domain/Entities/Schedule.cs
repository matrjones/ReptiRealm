using ReptiRealm_API.Domain.Entities.Common;
using ReptiRealm_API.Domain.Enums;
using System.Text.Json.Serialization;

namespace ReptiRealm_API.Domain.Entities
{
    public class Schedule : BaseEntity
    {
        #region Variables
        public ActivityType ActivityType { get; set; }
        public required Recurrence RecurrenceType { get; set; }
        public required int Frequency { get; set; }
        public required DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public bool IsActive { get; set; } = true;
        #endregion

        #region Foreign Keys
        public required Guid ReptileId { get; set; }
        #endregion

        #region Navigation Properties
        [JsonIgnore]
        public virtual Reptile? Reptile { get; set; }
        #endregion

        public DateTime GetNextDueDate(DateTime? lastCompleted)
        {
            if (!lastCompleted.HasValue)
            {
                return Start;
            }

            return RecurrenceType switch
            {
                Recurrence.Days => lastCompleted.Value.AddDays(Frequency),
                Recurrence.Weeks => lastCompleted.Value.AddDays(Frequency * 7),
                Recurrence.Months => lastCompleted.Value.AddMonths(Frequency),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
