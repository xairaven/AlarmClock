using System.IO;
using AlarmClock.Serializing;

namespace AlarmClock;

public class SettingsContext
{
    public Dictionary<string, int> Settings { get; }
    private readonly string _settingsPath;
    
    public SettingsContext()
    {
        var executableFolder = Path.GetDirectoryName(Environment.ProcessPath!);
        _settingsPath = Json.GetFilePath(executableFolder!, "UserSettings");

        if (!File.Exists(_settingsPath))
        {
            InitializeSettings();
        }
        
        Settings = Json.CustomDeserialize<Dictionary<string, int>>(_settingsPath);
    }
    
    private void InitializeSettings()
    {
        using (File.Create(_settingsPath)) {}

        var temp = new Dictionary<string, int>
        {
            {"MusicId", 1},
            {"AlarmDuration", 1}
        };
            
        Json.CustomSerialize(_settingsPath, temp);
    }

    public void UpdateJson()
    {
        Json.CustomSerialize(_settingsPath, Settings);
    }
}