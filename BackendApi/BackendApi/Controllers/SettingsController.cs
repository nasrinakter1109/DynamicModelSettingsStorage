using BackendApi.Models;
using BackendApi.Repositories;
using BackendApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISetting _settingService;

        public SettingsController(ISetting settingService)
        {
            _settingService = settingService;
        }
        // GET: api/settings
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Settings>>> GetSettings()
        //{
        //    var settings = await _settingService.GetAllSettingsAsync();
        //    return Ok(settings);
        //}
        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<Settings>>> GetSettingsByCategory(string category)
        {
            var settings = await _settingService.GetSettingsByCategoryAsync(category);
            return Ok(settings);
        }

        // POST: api/settings
        [HttpPost]
        public async Task<IActionResult> SaveSettings([FromBody] Dictionary<string, string> updatedSettings)
        {
            if (updatedSettings == null || !updatedSettings.Any())
            {
                return BadRequest("Settings data is required.");
            }

            // Save or update each setting
            foreach (var setting in updatedSettings)
            {
                await _settingService.SaveOrUpdateSettingAsync(setting.Key, setting.Value);
            }

            return NoContent(); // Return 204 No Content after successful update
        }
    }
}
