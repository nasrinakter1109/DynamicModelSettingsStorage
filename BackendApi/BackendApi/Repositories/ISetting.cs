using BackendApi.Models;

namespace BackendApi.Repositories
{
    public interface ISetting
    {
        Task<List<Settings>> GetSettingsByCategoryAsync(string category);
        Task SaveOrUpdateSettingAsync(string key, string value);
    }
}
