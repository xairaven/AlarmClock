using AlarmClock.Data;

namespace AlarmClock.Repositories;

public static class AlarmRepository
{
    public static List<AlarmRecord> AlarmList { get; } = [];

    public static AlarmRecord AddRecord(string title, DateTime dateTime)
    {
        var alarm = new AlarmRecord(title, dateTime); 
        
        AlarmList.Add(alarm);
        return alarm;
    }

    public static AlarmRecord EditRecord(int recordId, string title, DateTime dateTime)
    {
        var record = GetRecord(recordId);
        record.Title = title;
        record.DateTime = dateTime;

        return record;
    }

    public static AlarmRecord GetRecord(int id)
    {
        var alarm = AlarmList.Find(x => x.Id == id);

        if (alarm is null) 
            throw new ArgumentException("Alarm with this record is not exist");
        
        return alarm;
    }

    public static AlarmRecord RemoveRecord(int id)
    {
        var alarm = AlarmList.Find(x => x.Id == id);

        if (alarm is null) 
            throw new ArgumentException("Alarm with this record is not exist");
        
        AlarmList.Remove(alarm);
        
        return alarm;
    }
}