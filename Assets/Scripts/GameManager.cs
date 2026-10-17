using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float checkInterval = 0.5f;
    bool playerHadCastle = false;
    bool enemyHadCastle = false;
    string resultText = "";
    bool gameOver = false;
    float finalTime = 0f;  // ← НОВОЕ: сохраняем итоговое время

    float checkTimer = 0.0f;

    void Start()
    {
        if (TimerManager.Instance != null)
            TimerManager.Instance.StartTimer();
    }
    void Update()
    {
        if (gameOver) return;
        checkTimer += Time.deltaTime;
        if (checkTimer < checkInterval) return;
        checkTimer = 0.0f;

        int playerCastles = CountPlayerCastles();
        int enemyCastles = CountEnemyCastles();

        if (playerCastles > 0) playerHadCastle = true;
        if (enemyCastles > 0) enemyHadCastle = true;

        if (playerHadCastle && playerCastles == 0)
        {
            EndGame("YOU LOSE");
        }
        else if (enemyHadCastle && enemyCastles == 0)
        {
            EndGame("YOU WIN");
        }
    }

    void EndGame(string text)
    {
        gameOver = true;
        resultText = text;

        float finalTime = 0f;

        // Останавливаем таймер
        if (TimerManager.Instance != null)
        {
            TimerManager.Instance.StopTimer();
            finalTime = TimerManager.Instance.GetTime();
        }

        // ← НОВОЕ: сохраняем в JSON
        if (RecordManager.Instance != null)
        {
            RecordManager.Instance.SaveRecord(finalTime, text);
        }

        Time.timeScale = 0f;
    }

    int CountPlayerCastles()
    {
        int count = 0;
        // ← ИСПРАВЛЕНО: устаревший метод заменён
        foreach (var c in FindObjectsByType<CastlePlayer>(FindObjectsSortMode.None))
        {
            if (c.gameObject.tag == "Player") count++;
        }
        return count;
    }

    int CountEnemyCastles()
    {
        // ← ИСПРАВЛЕНО
        return FindObjectsByType<CastleNpc>(FindObjectsSortMode.None).Length;
    }

    void OnGUI()
    {
        if (!gameOver) return;

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 60;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = (resultText == "YOU WIN") ? Color.green : Color.red;

        // Надпись по центру
        GUI.Label(new Rect(0, Screen.height / 2 - 60, Screen.width, 120), resultText, style);

        // Ширина кнопок и отступ между ними
        float btnWidth = 200f;
        float btnHeight = 50f;
        float gap = 50f; // Расстояние между кнопками

        // Считаем, где начинать, чтобы вся группа была ровно по центру
        float totalWidth = (btnWidth * 2) + gap;
        float startX = (Screen.width - totalWidth) / 2f;
        float y = Screen.height / 2 + 80;

        // Кнопка "Restart" (слева)
        if (GUI.Button(new Rect(startX, y, btnWidth, btnHeight), "Restart"))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Кнопка "В меню" (справа)
        if (GUI.Button(new Rect(startX + btnWidth + gap, y, btnWidth, btnHeight), "В меню"))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0); // Убедись, что сцена называется именно так
        }
    }
}