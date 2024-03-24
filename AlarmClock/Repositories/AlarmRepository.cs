﻿using System.IO;
using AlarmClock.Data;
using AlarmClock.Serializing;

namespace AlarmClock.Repositories;

public static class AlarmRepository
{
    public static List<AlarmRecord> AlarmList { get; } = [];
    private static string _jsonPath;

    static AlarmRepository()
    {
        var executableFolder = Path.GetDirectoryName(Environment.ProcessPath!);
        _jsonPath = Json.GetFilePath(executableFolder!, "UserAlarms");
        
        if (!File.Exists(_jsonPath))
        {
            InitializeJson();
        }
        
        AlarmList = Json.CustomDeserialize<List<AlarmRecord>>(_jsonPath);
    }
    
    private static void InitializeJson()
    {
        using (File.Create(_jsonPath)) {}
            
        Json.CustomSerialize(_jsonPath, AlarmList);
    }

    public static void UpdateJson()
    {
        Json.CustomSerialize(_jsonPath, AlarmList);
    }

    public static AlarmRecord AddRecord(string title, DateTime dateTime)
    {
        var alarm = new AlarmRecord(title, dateTime); 
        
        AlarmList.Add(alarm);
        
        UpdateJson();
        
        return alarm;
    }

    public static AlarmRecord EditRecord(int recordId, string title, DateTime dateTime, bool isAlarmEnabled)
    {
        var record = GetRecord(recordId);
        record.Title = title;
        record.DateTime = dateTime;
        record.IsAlarmEnabled = isAlarmEnabled;
        
        UpdateJson();

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
        
        UpdateJson();
        
        return alarm;
    }
}