using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float timer = 0;
    private bool isRunning = false;

    public void Initialize()
    {
        timer = 0;
        isRunning = true;
    }

    public void Reset()
    {
        isRunning = false;
        timer = 0;
    }

    public float GetCurrentBestTime()
    {
        return timer;
    }

    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;

            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);

            timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }
    }
}
