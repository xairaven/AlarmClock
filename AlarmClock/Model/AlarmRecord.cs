namespace AlarmClock.Model;

public class AlarmRecord
{
    private static int _idCounter;
    public int Id { get; init; }
    public string Title { get; set; }
    public bool IsAlarmEnabled { get; set; }
    public DateTime DateTime { get; set; }

    public AlarmRecord(string title, DateTime dateTime)
    {
        Id = _idCounter++;
        Title = title;
        IsAlarmEnabled = true;
        DateTime = dateTime;
    }
}