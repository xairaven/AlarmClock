﻿using AlarmClock.Data;

namespace AlarmClock.Controllers;

public static class AlarmController
{
    public static List<AlarmRecord> AlarmList { get; } = [];

    public static AlarmRecord AddRecord(DateTime dateTime)
    {
        var alarm = new AlarmRecord(dateTime); 
        
        AlarmList.Add(alarm);
        return alarm;
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