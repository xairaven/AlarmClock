namespace AlarmClock.Model;

public class AlarmRecord
{
    public Guid Id { get; init; }
    public string Title { get; set; }
    public bool IsAlarmEnabled { get; set; }
    public DateTime DateTime { get; set; }

    public AlarmRecord(string title, DateTime dateTime)
    {
        Id = Guid.NewGuid();
        Title = title;
        IsAlarmEnabled = true;
        DateTime = dateTime;
    }
}