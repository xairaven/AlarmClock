using System.Windows;

namespace AlarmClock.Views;

public partial class Settings : Window
{
    public Settings()
    {
        InitializeComponent();
    }

    private void Close(object sender, RoutedEventArgs e)
    {
        Close();
    }
}