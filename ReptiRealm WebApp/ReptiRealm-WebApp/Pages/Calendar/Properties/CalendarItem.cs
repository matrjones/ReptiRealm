using ReptiRealm_WebApp.Models.DTOs;
using ReptiRealm_WebApp.Models.Enums;

namespace ReptiRealm_WebApp.Pages.Calendar.Properties;

public class CalendarItem
{
    public string Title => this.Type.ToString();
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public bool AllDay { get; set; }
    public TaskType Type { get; set; }
    public List<Event> Events { get; set; } = new();
}
