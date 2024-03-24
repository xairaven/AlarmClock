using System.Globalization;
using System.Windows;
using AlarmClock.Repositories;
using AlarmClock.Views.Controls;

namespace AlarmClock.Views;

public partial class AlarmsList : Window
{
    public AlarmsList()
    {
        InitializeComponent();
        
        UpdateList();
    }

    private void AddAlarmShow(object sender, RoutedEventArgs e)
    {
        new AddAlarmWindow(this).Show();
    }
    
    private void UpdateList()
    {
        ListPanel.Children.Clear();
        foreach (var record in AlarmRepository.AlarmList)
        {
            var element = new AlarmElement();

            element.Id = record.Id;
            element.Title = record.Title;
            
            var datetime = record.DateTime;
            element.Time = $"{datetime.Hour:00}:{datetime.Minute:00}";
            
            var monthName = datetime.ToString("MMM", CultureInfo.InvariantCulture);
            element.Date = $"{datetime.DayOfWeek.ToString()[..3]}, {datetime.Day:00} {monthName}";
            
            ListPanel.Children.Add(element);
        }
    }
}