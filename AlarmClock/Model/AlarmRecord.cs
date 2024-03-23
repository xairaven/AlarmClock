namespace AlarmClock.Data;

public class AlarmRecord
{
    private static int _idCounter;
    public int Id { get; init; }
    public bool IsAlarmEnabled { get; set; }
    public DateTime DateTime { get; set; }

    public AlarmRecord(DateTime dateTime)
    {
        Id = _idCounter++;
        IsAlarmEnabled = true;
        DateTime = dateTime;
    }
}