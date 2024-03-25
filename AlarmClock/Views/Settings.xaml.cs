using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using AlarmClock.Data;

namespace AlarmClock.Views;

public partial class Settings : Window
{
    private SettingsContext _context;
    
    public Settings(SettingsContext context)
    {
        InitializeComponent();

        _context = context;
    }

    private void CloseWindow(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void MusicBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        MusicBox.ItemsSource = Enum.GetValues(typeof(Music)).Cast<Music>();

        var musicId = _context.Settings["MusicId"];
        MusicBox.SelectedItem = (Music)musicId;
        
        var duration = _context.Settings["AlarmDuration"];
        AlarmDurationSlider.Value = duration;
    }

    private void SaveSettings(object sender, RoutedEventArgs e)
    {
        var musicId = (int) MusicBox.SelectedItem;
        var alarmDuration = (int) AlarmDurationSlider.Value;

        _context.Settings["MusicId"] = musicId;
        _context.Settings["AlarmDuration"] = alarmDuration;
        
        _context.UpdateJson();
        
        Close();
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri)
        {
            UseShellExecute = true
        });
        e.Handled = true;
    }
}