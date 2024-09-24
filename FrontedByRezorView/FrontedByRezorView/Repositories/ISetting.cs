using FrontedByRezorView.Models;

namespace FrontedByRezorView.Repositories
{
    public interface ISetting
    {
        Task<List<Settings>> GetSettingsByCategoryAsync(string category);
        Task SaveOrUpdateSettingAsync(string key, string value);
    }
}
