namespace FrontedByRezorView.ModelViews
{
    public class SettingsViewModel
    {
        public Dictionary<string, string> Settings { get; set; }

        public SettingsViewModel()
        {
            Settings = new Dictionary<string, string>();
        }


    }
}
