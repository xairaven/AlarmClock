using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lab3;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        var timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += TimerTick;
        timer.Start();
    }

    private void TimerTick(object? sender, EventArgs e)
    {
        var dateTime = DateTime.Now;
        
        HourLabel.Content = $"{dateTime.Hour:00}";
        MinuteLabel.Content = $"{dateTime.Minute:00}";
        SecondLabel.Content = $"{dateTime.Second:00}";
        DayLabel.Content = $"{dateTime.DayOfWeek.ToString()[..3]}";
        DateLabel.Content = $"{dateTime.Day:00}/{dateTime.Month:00}/{dateTime.Year:0000}";
    }

    private void App_Exit(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}