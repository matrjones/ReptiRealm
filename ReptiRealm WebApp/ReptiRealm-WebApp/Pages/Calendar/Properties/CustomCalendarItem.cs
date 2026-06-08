using Heron.MudCalendar;
using MudBlazor;
using ReptiRealm_WebApp.Models.DTOs;
using ReptiRealm_WebApp.Models.Enums;

namespace ReptiRealm_WebApp.Pages.Calendar.Properties;

public class CustomCalendarItem : CalendarItem
{
    public string Title => this.Type.ToString();
    public EventType Type { get; set; }
    public Color Color { get; set; } = MudBlazor.Color.Secondary;
    public List<Event> Events { get; set; } = new();
}
