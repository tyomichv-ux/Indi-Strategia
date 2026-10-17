using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    private float elapsedTime = 0f;
    private bool isRunning = false;

    public System.Action<float> OnTimeChanged;
    public System.Action<float> OnTimerStopped;

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
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }

    public void StopTimer()
    {
        if (!isRunning) return;
        isRunning = false;
        OnTimerStopped?.Invoke(elapsedTime);
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        isRunning = false;
        OnTimeChanged?.Invoke(elapsedTime);
    }

    void Update()
    {
        if (!isRunning) return;
        elapsedTime += Time.deltaTime;
        OnTimeChanged?.Invoke(elapsedTime);
    }

    public float GetTime()
    {
        return elapsedTime;
    }

    public static string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}