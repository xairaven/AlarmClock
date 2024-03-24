using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using AlarmClock.Data;
using AlarmClock.Model;
using AlarmClock.Repositories;
using AlarmClock.Views.Controls;

namespace AlarmClock.Views;

public partial class EditAlarmWindow : Window
{
    private AlarmRecord _record;
    private Panel _stackPanel;

    public EditAlarmWindow(Panel stackPanel, AlarmRecord record)
    {
        InitializeComponent();

        _record = record;
        _stackPanel = stackPanel;

        TitlePicker.Text = _record.Title;
        DatePicker.MinDate = DateTime.Now.Date;
        TimePicker.Value = DateTime.Now;
    }

    private void CancelEditingAlarm(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void SaveAlarm(object sender, RoutedEventArgs e)
    {
        var title = TitlePicker.Text.Trim();
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
        
        AlarmRepository.EditRecord(_record.Id, title, resultDatetime, _record.IsAlarmEnabled);
        
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
        _stackPanel.Children.Clear();
        foreach (var record in AlarmRepository.AlarmList)
        {
            var element = new AlarmElement();

            element.Id = record.Id;
            element.Title = record.Title;
            
            var datetime = record.DateTime;
            element.Time = $"{datetime.Hour:00}:{datetime.Minute:00}";
            
            var monthName = datetime.ToString("MMM", CultureInfo.InvariantCulture);
            element.Date = $"{datetime.DayOfWeek.ToString()[..3]}, {datetime.Day:00} {monthName}";
            
            _stackPanel.Children.Add(element);
        }
    }
}