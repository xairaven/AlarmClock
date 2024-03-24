using System.Windows;
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

    private void Close(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void MusicBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        MusicBox.ItemsSource = Enum.GetValues(typeof(Music)).Cast<Music>();

        var musicId = int.Parse(_context.Settings["MusicId"]);
        MusicBox.SelectedItem = (Music)musicId;
    }
}