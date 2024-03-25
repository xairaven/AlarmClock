using System.IO;
using System.Windows;
using System.Windows.Media;
using AlarmClock.Data;

namespace AlarmClock.Views;

public partial class AlarmNotification : Window
{
    private SettingsContext _context;
    private MediaPlayer _mediaPlayer;
    
    public AlarmNotification(SettingsContext context)
    {
        _context = context;
        _mediaPlayer = new MediaPlayer();
        Closed += OnClosed;
        
        OpenMusicFile(_context.Settings["MusicId"]);
        
        InitializeComponent();
        
        NotificationMusicStart(_context.Settings["AlarmDuration"]);
    }

    private void OnClosed(object? sender, EventArgs e)
    {
        _mediaPlayer.Stop();
    }


    private void OpenMusicFile(int musicId)
    {
        var filePath = $"Assets\\Music\\{((Music)musicId).ToString()}.mp3";

        var uri = new Uri(filePath, UriKind.Relative);

        if (!File.Exists(Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)!, uri.ToString())))
        {
            MessageBox.Show(messageBoxText:"Something wrong with music file uploading.", 
                caption: "Error!", 
                button: MessageBoxButton.OK, 
                icon: MessageBoxImage.Error, 
                defaultResult: MessageBoxResult.OK);
            
            Application.Current.Shutdown();
        }
        
        _mediaPlayer.Open(uri);
    }

    private async void NotificationMusicStart(int duration)
    {
        PlayMusic(null, null);
        _mediaPlayer.MediaEnded += PlayMusic;

        await Task.Run(() => Thread.Sleep(TimeSpan.FromSeconds(duration)));
        _mediaPlayer.Stop();
    }
    
    private void PlayMusic(object? sender, EventArgs? e)
    {
        _mediaPlayer.Position = TimeSpan.Zero;
        _mediaPlayer.Play();
    }
}