using System;
using System.IO;
using UnityEngine;

public class LeaderboardFileManager : MonoBehaviour
{
    [SerializeField, Tooltip("Имя файла, находящегося в постоянном каталоге данных Application.persistentDataPath.")]
    private string _fileName;
    private string _FullFileName { get => $"{Application.persistentDataPath}/{_fileName}"; }

    private string[] _templateNames = new[]
    {
        "Атлет",
        "Моряк",
        "Строитель",
        "Инженер",
        "Потрясающе длинное имя, которое никто не узнает :("
    };

    public LeaderboardRecord[] ReadRecords(int recordsCount = 5)
    {
        LeaderboardRecord[] records = new LeaderboardRecord[recordsCount];
        string[] lines = null;
        try
        {
            lines = File.ReadAllLines(_FullFileName);
        }
        catch { Debug.Log($"Файла {_fileName} не существует. Будут созданы шаблоны записей."); }
        finally
        {
            if (lines != null)
            {
                int workingLines = Math.Min(lines.Length, recordsCount);
                for (int i = 0; i < workingLines; ++i)
                {
                    string line = lines[i];
                    int spaceIndex = line.LastIndexOf(' ');
                    string name = line[..spaceIndex];
                    int value = Convert.ToInt32(line[(spaceIndex + 1)..]);
                    records[i] = new LeaderboardRecord(name, value);
                }
                if (workingLines < recordsCount)
                {
                    for (int i = workingLines; i < recordsCount; ++i)
                    {
                        records[i] = new LeaderboardRecord(_templateNames[i % _templateNames.Length], 0);
                    }
                }
            }
            else
            {
                for (int i = 0; i < recordsCount; ++i)
                {
                    records[i] = new LeaderboardRecord(_templateNames[i % _templateNames.Length], 0);
                }
            }
        }
        return records;
    }

    public void WriteRecords(LeaderboardRecord[] records)
    {
        string[] recordStrings = new string[records.Length];
        for (int i = 0; i < records.Length; ++i)
        {
            recordStrings[i] = $"{records[i].Name} {records[i].Value}";
        }
        File.WriteAllLines(_FullFileName, recordStrings);
    }
}