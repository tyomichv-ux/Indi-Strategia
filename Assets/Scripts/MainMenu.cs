using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Настройки сложности")]
    public string[] difficultyNames = { "Легко", "Средне", "Сложно" };
    private int currentDifficultyIndex = 0;

    [Header("UI Элементы")]
    public TextMeshProUGUI difficultyButtonText; // Текст на кнопке сложности
    public GameObject recordsPanel;              // Панель с рекордами (бывшая PlayersPanel)
    public Transform recordsContent;             // Content внутри ScrollView

    void Start()
    {
        UpdateDifficultyText();

        // Прячем панель рекордов при старте
        if (recordsPanel != null)
            recordsPanel.SetActive(false);
    }

    //логика сложности
    public void OnClickDifficulty()
    {
        currentDifficultyIndex++;
        if (currentDifficultyIndex >= difficultyNames.Length)
            currentDifficultyIndex = 0;

        UpdateDifficultyText();

        PlayerPrefs.SetInt("GameDifficulty", currentDifficultyIndex);
        PlayerPrefs.Save();
    }

    void Update()
    {
        // Если нажата клавиша Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Если панель рекордов открыта - закрываем её
            if (recordsPanel != null && recordsPanel.activeSelf)
            {
                recordsPanel.SetActive(false);
            }
        }
    }

    private void UpdateDifficultyText()
    {
        if (difficultyButtonText != null)
            difficultyButtonText.text = difficultyNames[currentDifficultyIndex];
    }

//панель рекордов
    public void OnClickRecords()
    {
        if (recordsPanel != null)
        {
            recordsPanel.SetActive(!recordsPanel.activeSelf);

            if (recordsPanel.activeSelf)
                RefreshRecordsList();
        }
    }

    private void RefreshRecordsList()
    {
        // Очищаем старый список
        foreach (Transform child in recordsContent)
            Destroy(child.gameObject);

        // Проверяем, есть ли RecordManager
        if (RecordManager.Instance == null)
        {
            CreateRecordEntry("Ошибка", "RecordManager не найден");
            return;
        }

        // Загружаем отсортированные записи
        RecordManager.GameRecord[] records = RecordManager.Instance.GetSortedRecords();

        // Если записей нет
        if (records.Length == 0)
        {
            CreateRecordEntry("Пока нет записей", "");
            return;
        }

        // Показываем топ-10
        int maxShow = Mathf.Min(records.Length, 10);
        for (int i = 0; i < maxShow; i++)
        {
            RecordManager.GameRecord r = records[i];

            // Форматируем строку: место. дата | время | результат
            string rank = (i + 1) + ".";
            string date = r.date;
            string time = TimerManager.FormatTime(r.time);
            string result = r.result;

            CreateRecordEntry(rank + " " + date, time + "  " + result);
        }
    }

    private void CreateRecordEntry(string date, string score)
    {
        // Создаём строку с текстом
        GameObject newEntry = new GameObject("Record");
        newEntry.transform.SetParent(recordsContent, false);

        // Добавляем компонент текста
        TextMeshProUGUI textComp = newEntry.AddComponent<TextMeshProUGUI>();
        textComp.text = date + "   |   " + score;
        textComp.fontSize = 24;

        textComp.color = Color.black; // Делаем текст чёрным, чтобы был виден на белом фоне

        textComp.alignment = TextAlignmentOptions.Left; // Выравниваем по левому краю
        textComp.margin = new Vector4(10, 0, 0, 0); // Небольшой отступ слева

        RectTransform rect = newEntry.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(0, 30); // Ширина будет управляться Layout Group, высота 30

        LayoutElement le = newEntry.AddComponent<LayoutElement>();
        le.preferredHeight = 30;
    }

    //кнопки действий
    public void OnClickPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}