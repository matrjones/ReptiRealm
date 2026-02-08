namespace ReptiRealm_WebApp.Components.HelperClass
{
    public class HeaderData
    {
        public string Heading { get; set; } = "";
        public string SubHeading { get; set; } = "";
        public string ButtonText { get; set; } = "";
        public string ButtonIcon { get; set; } = "";
        public Func<Task>? ButtonAction { get; set; }
    }
    public class HeaderState
    {
        public HeaderData CurrentHeader { get; private set; } = new HeaderData();

        public event Action? OnChange;

        public void SetHeader(HeaderData header)
        {
            CurrentHeader = header;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

}
