using System.Windows;

namespace AlarmClock.Views;

public partial class AddAlarmWindow : Window
{
    public AddAlarmWindow()
    {
        InitializeComponent();
        DatePicker.MinDate = DateTime.Now;
        TimePicker.MinTime = DateTime.Now.TimeOfDay;
    }

    private void CancelAddAlarm(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void SaveAlarm(object sender, RoutedEventArgs e)
    {
        
    }

    private void DatePicker_OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var date = DatePicker.Value;
        if (date is not null && date.Value.CompareTo(DateTime.Now) == 1)
        {
            TimePicker.MinTime = new TimeSpan(00,00,00);
        }
    }
}