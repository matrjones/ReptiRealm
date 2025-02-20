namespace ReptiRealm.Services
{
    public interface IHangfireService
    {
        bool ScheduleTaskDelayMinutes(Task task, int mins);
        bool ScheduleTaskDelayHours(Task task, int hours);
        bool ScheduleTaskDelayDays(Task task, int days);
        bool ScheduleTaskAtDateTime(Task task, DateTime dateTime);
    }
}
