namespace FrontedByRezorView.ModelViews
{
    public class EmailSettingModelView : SettingsViewModel
    {
        public Dictionary<string, string> Settings { get; set; }=new Dictionary<string, string>();
        public EmailSettingModelView() 
        {
            Settings = new Dictionary<string, string>()
            {
                {"Email_Address",string.Empty},
                {"Email_Name",string.Empty},
                {"Email_Contact",string.Empty},
                {"Email_PersonName",string.Empty},
            };
        }
    }
}
