using System.Windows;

namespace AlarmClock.Views;

public partial class AlarmsList : Window
{
    public AlarmsList()
    {
        InitializeComponent();
    }

    private void AddAlarmShow(object sender, RoutedEventArgs e)
    {
        new AddAlarmWindow().Show();
    }
}