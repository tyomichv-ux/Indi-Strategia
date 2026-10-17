using UnityEngine;
using System;
using System.IO;

public class RecordManager : MonoBehaviour
{
    public static RecordManager Instance;

    [System.Serializable]
    public class GameRecord
    {
        public string date;
        public float time;
        public string result;
    }

    [System.Serializable]
    public class GameRecordList
    {
        public GameRecord[] results;
    }

    private string savePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        savePath = Application.persistentDataPath + "/records.json";
    }

    // Сохранить новый результат
    public void SaveRecord(float time, string result)
    {
        GameRecord newRecord = new GameRecord();
        newRecord.date = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
        newRecord.time = time;
        newRecord.result = result;

        GameRecordList list = LoadRecords();

        // Добавляем новую запись
        GameRecord[] newArray = new GameRecord[list.results.Length + 1];
        list.results.CopyTo(newArray, 0);
        newArray[newArray.Length - 1] = newRecord;
        list.results = newArray;

        // Сохраняем в файл
        string json = JsonUtility.ToJson(list, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Запись сохранена: " + newRecord.date + " | " + newRecord.time + " | " + newRecord.result);
    }

    // Загрузить все записи
    public GameRecordList LoadRecords()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            GameRecordList list = JsonUtility.FromJson<GameRecordList>(json);
            if (list != null && list.results != null)
                return list;
        }

        // Если файла нет — возвращаем пустой список
        return new GameRecordList { results = new GameRecord[0] };
    }

    // Получить отсортированный список (лучшее время первым, только победы)
    public GameRecord[] GetSortedRecords()
    {
        GameRecordList list = LoadRecords();
        if (list.results == null || list.results.Length == 0)
            return new GameRecord[0];

        // Сортируем по времени (по возрастанию — быстрее = лучше)
        GameRecord[] sorted = (GameRecord[])list.results.Clone();
        Array.Sort(sorted, (a, b) => a.time.CompareTo(b.time));
        return sorted;
    }
}