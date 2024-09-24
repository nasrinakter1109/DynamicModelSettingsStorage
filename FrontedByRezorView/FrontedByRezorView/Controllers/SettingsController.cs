using FrontedByRezorView.Models;
using FrontedByRezorView.ModelViews;
using FrontedByRezorView.Repositories;
using FrontedByRezorView.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrontedByRezorView.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ISetting _settingService;

        public SettingsController(ISetting settingService)
        {
            _settingService = settingService;
        }

        //// GET: /Settings/Index
        //[HttpGet]
        //public async Task<IActionResult> Index(string category)
        //{
        //    List<Settings> settings;

        //    try
        //    {
        //        settings = await _settingService.GetSettingsByCategoryAsync(category) ?? new List<Settings>();

        //        if (!settings.Any())
        //        {
        //            return View(new SettingsViewModel()); // Return an empty model if no settings
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        return View(new SettingsViewModel()); // Return an empty model
        //    }

        //    var model = new SettingsViewModel();

        //    foreach (var setting in settings)
        //    {
        //        model.Settings[setting.ConfigKey] = setting.ConfigValue;
        //    }

        //    return View(model);
        //}

        //// POST: /Settings/SaveSettings
        //[HttpPost]
        //public async Task<IActionResult> SaveSettings(SettingsViewModel model,string category)
        //{
        //    foreach (var setting in model.Settings)
        //    {
        //        await _settingService.SaveOrUpdateSettingAsync(setting.Key, setting.Value);
        //    }

        //    return RedirectToAction("Index", new { category = category });
        //}

        //// GET: /Settings/Index
        //[HttpGet]
        //public async Task<IActionResult> EmailSetting(string category)
        //{
        //    List<Settings> settings;

        //    try
        //    {
        //        settings = await _settingService.GetSettingsByCategoryAsync(category) ?? new List<Settings>();

        //        if (!settings.Any())
        //        {
        //            return View(new EmailSettingModelView()); // Return an empty model if no settings
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        return View(new EmailSettingModelView()); // Return an empty model
        //    }

        //    var model = new EmailSettingModelView();

        //    foreach (var setting in settings)
        //    {
        //        model.Settings[setting.ConfigKey] = setting.ConfigValue;
        //    }

        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> SaveEmailSettings(EmailSettingModelView model, string category)
        //{
        //    foreach (var setting in model.Settings)
        //    {
        //        await _settingService.SaveOrUpdateSettingAsync(setting.Key, setting.Value);
        //    }

        //    return RedirectToAction("EmailSetting", new { category = category });
        //}

        [HttpGet]
        public async Task<IActionResult> Index(string category)
        {
            List<Settings> settings;

            try
            {
                settings = await _settingService.GetSettingsByCategoryAsync(category) ?? new List<Settings>();

                if (!settings.Any())
                {
                    return View(GetViewName(category), CreateEmptyViewModel(category)); // Return an empty model if no settings
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return View(GetViewName(category), CreateEmptyViewModel(category)); // Return an empty model
            }

            var model = CreateViewModel(category);

            foreach (var setting in settings)
            {
                model.Settings[setting.ConfigKey] = setting.ConfigValue;
            }

            return View(GetViewName(category), model);
        }

        // POST: /Settings/SaveSettings
        [HttpPost]
        public async Task<IActionResult> SaveSettings(SettingsViewModel model, string category)
        {
            foreach (var setting in model.Settings)
            {
                await _settingService.SaveOrUpdateSettingAsync(setting.Key, setting.Value);
            }

            return RedirectToAction("Index", new { category = category });
        }


        //// Helper method to dynamically select view model based on category
        private SettingsViewModel CreateViewModel(string category)
        {
            var model = category switch
            {
                "CompanyProfile" => new CompanyProfileSettingsViewModel(),
                "Email" => new EmailSettingModelView(),
                
                _=> new SettingsViewModel() // Default case if category doesn't match
            };

            return model;
        }

        // Helper method to create an empty view model
        private SettingsViewModel CreateEmptyViewModel(string category)
        {
            return CreateViewModel(category); // Can be customized if needed
        }

        // Helper method to dynamically select view name based on category
        private string GetViewName(string category)
        {
            return category switch
            {
                "CompanyProfile" => "Index",
                "Email" => "EmailSetting",

                _ => "Settings" // Default view name if category doesn't match
            };
        }
    }
}
