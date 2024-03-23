using System.Globalization;
using System.Windows;
using AlarmClock.Controllers;
using AlarmClock.Data;
using AlarmClock.Views.Controls;

namespace AlarmClock.Views;

public partial class AddAlarmWindow : Window
{
    private AlarmsList _listWindow;
    
    public AddAlarmWindow(AlarmsList window)
    {
        InitializeComponent();
        DatePicker.MinDate = DateTime.Now.Date;
        TimePicker.Value = DateTime.Now;

        _listWindow = window;
    }

    private void CancelAddAlarm(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void SaveAlarm(object sender, RoutedEventArgs e)
    {
        var time = TimePicker.Value!.Value.TimeOfDay;
        var date = DatePicker.Value!.Value.Date;

        if (!CheckIsAlarmReal(time, date))
        {
            MessageBox.Show(messageBoxText:"Error! Setting alarms for the past is strictly prohibited.", 
                caption: "Error!", 
                button: MessageBoxButton.OK, 
                icon: MessageBoxImage.Error, 
                defaultResult: MessageBoxResult.OK);
            return;
        }

        var resultDatetime = date.Date.Add(time);

        AlarmController.AddRecord("", resultDatetime);
        
        UpdateList();

        Close();
    }

    private bool CheckIsAlarmReal(TimeSpan time, DateTime date)
    {
        // If date on DatePicker is later than datetime Now, than any time is good
        if (date.CompareTo(DateTime.Now.Date) == 1)
        {
            return true;
        }

        // But if date on DatePicker is equal (real) or earlier (not real) than datetime.now: 
        // Compare TimePicker and Datetime.now.
        // If TimePicker is later (> 0) than DateTime.now, its real alarm
        return time.CompareTo(DateTime.Now.TimeOfDay) > 0;
    }

    private void UpdateList()
    {
        var listPanel = _listWindow.ListPanel;

        listPanel.Children.Clear();
        foreach (var record in AlarmController.AlarmList)
        {
            var element = new AlarmElement();

            element.Id = record.Id;
            element.Title = record.Title;
            
            var datetime = record.DateTime;
            element.Time = $"{datetime.Hour:00}:{datetime.Minute}";
            
            var monthName = datetime.ToString("MMM", CultureInfo.InvariantCulture);
            element.Date = $"{datetime.DayOfWeek.ToString()[..3]}, {datetime.Day:00} {monthName}";
            
            listPanel.Children.Add(element);
        }
    }
}