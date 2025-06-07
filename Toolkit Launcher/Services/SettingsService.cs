using Toolkit_Launcher.Models;
namespace Toolkit_Launcher.Services;

using Newtonsoft.Json;
using System;
using System.IO;

public class SettingsService
{
    private readonly string _settingsFilePath;
    public ListInfo CurrentSettings { get; private set; }
    public ListInfo TempSettings { get; set; } // Used for editing in settings UI

    public SettingsService(string settingsFileName = "setting.json")
    {
        _settingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, settingsFileName);
        CurrentSettings = LoadSettingsFromFile();
        TempSettings = JsonConvert.DeserializeObject<ListInfo>(JsonConvert.SerializeObject(CurrentSettings)); // Deep copy for temporary edits
    }

    private ListInfo LoadSettingsFromFile()
    {
        if (!File.Exists(_settingsFilePath))
        {
            return new ListInfo(); // Return default/empty settings
        }

        try
        {
            var jsonData = File.ReadAllText(_settingsFilePath);
            return JsonConvert.DeserializeObject<ListInfo>(jsonData) ?? new ListInfo();
        }
        catch (Exception ex)
        {
            // Consider logging this error appropriately
            Console.WriteLine($"Error loading settings: {ex.Message}");
            return new ListInfo(); // Return default/empty on error
        }
    }

    public void SaveSettings()
    {
        try
        {
            // Save TempSettings to CurrentSettings and then to file
            CurrentSettings = JsonConvert.DeserializeObject<ListInfo>(JsonConvert.SerializeObject(TempSettings)); // Deep copy back
            var jsonData = JsonConvert.SerializeObject(CurrentSettings, Formatting.Indented);
            File.WriteAllText(_settingsFilePath, jsonData);
        }
        catch (Exception ex)
        {
            // Consider logging this error appropriately
            Console.WriteLine($"Error saving settings: {ex.Message}");
        }
    }

    public void ReloadTempSettings()
    {
        // Reload TempSettings from CurrentSettings (e.g., when settings dialog is cancelled)
        TempSettings = JsonConvert.DeserializeObject<ListInfo>(JsonConvert.SerializeObject(CurrentSettings));
    }
}
