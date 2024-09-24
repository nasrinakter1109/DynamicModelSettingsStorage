using BackendApi.Models;
using BackendApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Services
{
    public class SettingService:ISetting
    {
        private readonly ApplicationDbContext _context;

        public SettingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Settings>> GetSettingsByCategoryAsync(string category)
        {
            //return await _context.Settings.ToListAsync();
            // Assuming _dbContext is your database context
            return await _context.Settings
                .Where(s => s.ConfigKey.StartsWith(category))
                .ToListAsync();
        }

        public async Task SaveOrUpdateSettingAsync(string key, string value)
        {
            var setting = await _context.Settings.FirstOrDefaultAsync(s => s.ConfigKey == key);

            if (setting == null)
            {
                // Insert new setting
                setting = new Settings { ConfigKey = key, ConfigValue = value };
                _context.Settings.Add(setting);
            }
            else
            {
                // Update existing setting
                setting.ConfigValue = value;
            }

            await _context.SaveChangesAsync();
        }
    }
}
