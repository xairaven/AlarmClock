using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AlarmClock.Views.Controls;

public partial class AlarmElement : UserControl
{
    [Description("Title. Example: School"), Category("Alarm")] 
    public string Title {
        get => TitleTextBlock.Text;
        set => TitleTextBlock.Text = value;
    }
    
    [Description("Time. Example: 00:00"), Category("Alarm")] 
    public string Time {
        get => (string) TimeLabel.Content;
        set => TimeLabel.Content = value;
    }
    
    [Description("Date. Example: Fri, 23 Mar"), Category("Alarm")] 
    public string Date {
        get => (string) DateLabel.Content;
        set => DateLabel.Content = value;
    }

    [Description("Status of alarm, On/Off"), Category("Alarm")]
    public bool IsAlarmEnabled = true;
    
    public AlarmElement()
    {
        InitializeComponent();
        GetImageCorrespondingStatus();
    }

    private void GetImageCorrespondingStatus()
    {
        var offImage = new Uri("../../Assets/Icons/SwitchOff.png", UriKind.Relative);
        var onImage = new Uri("../../Assets/Icons/SwitchOn.png", UriKind.Relative);

        var imagePath = IsAlarmEnabled ? onImage : offImage; 
        
        var icon = new BitmapImage(imagePath)
        {
            CacheOption = BitmapCacheOption.OnLoad
        };
        SwitchImage.Source = icon;
    }

    private void SwitchStatus(object sender, RoutedEventArgs e)
    {
        IsAlarmEnabled = !IsAlarmEnabled;
        GetImageCorrespondingStatus();
    }
}