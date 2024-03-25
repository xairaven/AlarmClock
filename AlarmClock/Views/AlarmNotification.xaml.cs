using System.IO;
using System.Windows;
using System.Windows.Media;
using AlarmClock.Data;
using AlarmClock.Model;

namespace AlarmClock.Views;

public partial class AlarmNotification : Window
{
    private SettingsContext _context;
    private MediaPlayer _mediaPlayer;
    private AlarmRecord _record;
    
    public AlarmNotification(SettingsContext context, AlarmRecord record)
    {
        _context = context;
        _record = record;
        
        _mediaPlayer = new MediaPlayer();
        Closed += OnClosed;
        
        OpenMusicFile(_context.Settings["MusicId"]);
        
        InitializeComponent();
        InitializeFields();
        
        StopMusicButton.IsEnabledChanged += StopMusicButtonOnIsEnabledChanged;
        
        NotificationMusicStart(_context.Settings["AlarmDuration"]);
    }

    private void StopMusicButtonOnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        StopMusicBorder.Background = new SolidColorBrush(Colors.DarkGray);
    }

    private void InitializeFields()
    {
        if (!_record.Title.Equals(""))
        {
            TitleLabel.Content = _record.Title;
        } else TitleLabel.Content = "None";
        
        var dateTime = _record.DateTime;
        TimeLabel.Content = $"{dateTime.Hour:00}:{dateTime.Minute:00}";
        DateLabel.Content = $"{dateTime.Day:00}.{dateTime.Month:00}.{dateTime.Year:0000}";
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

    private void StopMusic_OnClick(object sender, RoutedEventArgs e)
    {
        StopMusicButton.IsEnabled = false;
    }
}