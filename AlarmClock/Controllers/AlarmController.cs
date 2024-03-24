using System.Windows.Threading;

namespace AlarmClock.Controllers;

public static class AlarmController
{
    public static void Start()
    {
        var timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(30)
        };
        timer.Tick += CheckAlarms;
        timer.Start();
    }

    private static void CheckAlarms(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}