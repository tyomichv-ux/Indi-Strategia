using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float checkInterval = 0.5f;   // как часто проверять (сек)
    bool playerHadCastle = false; // у игрока хоть раз был замок?
    bool enemyHadCastle = false;// у врага хоть раз был замок?
    string resultText = ""; // "YOU WIN" / "YOU LOSE"
    bool gameOver = false;

    float checkTimer = 0.0f;

    void Start()
    {
        Debug.Log("GameManager STARTED");   // <-- ВРЕМЕННО
    }

    void Update()
    {
        if (gameOver) return;
        checkTimer += Time.deltaTime;
        if (checkTimer < checkInterval) return;
        checkTimer = 0.0f;

        int playerCastles = CountPlayerCastles();
        int enemyCastles = CountEnemyCastles();

        Debug.Log("P=" + playerCastles + " E=" + enemyCastles +
              " hadP=" + playerHadCastle + " hadE=" + enemyHadCastle);

        // Запоминаем, что замки вообще были
        if (playerCastles > 0) playerHadCastle = true;
        if (enemyCastles > 0) enemyHadCastle = true;

        // Проверяем условия конца игры
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
        Time.timeScale = 0f;// замораживаем игру
    }

    int CountPlayerCastles()
    {
        int count = 0;
        foreach (var c in FindObjectsOfType<CastlePlayer>())
        {
            if (c.gameObject.tag == "Player") count++;
        }
        return count;
    }

    int CountEnemyCastles()
    {
        return FindObjectsOfType<CastleNpc>().Length;
    }

    void OnGUI()
    {
        if (!gameOver) return;

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 60;
        style.alignment = TextAnchor.MiddleCenter;
        
        style.normal.textColor = (resultText == "YOU WIN") ? Color.green : Color.red;
        GUI.Label(new Rect(0, Screen.height / 2 - 60, Screen.width, 120), resultText, style);

        // Кнопка "Restart"
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 80, 200, 50), "Restart"))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }



}
