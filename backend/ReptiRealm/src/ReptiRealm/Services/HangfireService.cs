using Hangfire;

namespace ReptiRealm.Services
{
    public class HangfireService : IHangfireService
    {
        private ILogger logger;
        public HangfireService(ILogger logger) 
        { 
            this.logger = logger;
        }

        public bool ScheduleTaskDelayMinutes(Task task, int mins)
        {
            try
            {
                BackgroundJob.Schedule(() => task, TimeSpan.FromMinutes(mins));
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        public bool ScheduleTaskDelayHours(Task task, int hours)
        {
            try
            {
                BackgroundJob.Schedule(() => task, TimeSpan.FromHours(hours));
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        public bool ScheduleTaskDelayDays(Task task, int days)
        {
            try
            {
                BackgroundJob.Schedule(() => task, TimeSpan.FromDays(days));
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        public bool ScheduleTaskAtDateTime(Task task, DateTime dateTime)
        {
            try
            {
                BackgroundJob.Schedule(() => task, dateTime - DateTime.Now);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
