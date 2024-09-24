namespace FrontedByRezorView.ModelViews
{
    public class CompanyProfileSettingsViewModel : SettingsViewModel
    {
      
        public Dictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();
        public CompanyProfileSettingsViewModel()
        {
            // Initialize with predefined setting keys
            Settings = new Dictionary<string, string>
        {
            { "CompanyProfile_companyName", string.Empty },
            { "CompanyProfile_companyAddress", string.Empty },
            { "CompanyProfile_companyContact", string.Empty }
            // Add more predefined keys as needed
        };
        }
    }
}
