using System.Windows.Threading;

namespace AlarmClock.Services;

public class TimerService
{
    private DispatcherTimer _timer;
    
    public TimerService(EventHandler tick)
    {
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += tick;
    }

    public DispatcherTimer Start(bool timeSynchronization=false)
    {
        if (timeSynchronization)
        {
            var now = DateTime.Now;
            var interval = TimeSpan.FromSeconds(1);
            var delay = interval.Subtract(TimeSpan.FromMilliseconds(now.Millisecond));
            Thread.Sleep(delay);
        }
        
        _timer.Start();

        return _timer;
    }
}