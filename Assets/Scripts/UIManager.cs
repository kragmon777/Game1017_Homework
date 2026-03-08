using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject playButton, resetButton, gameOverButton;
    [SerializeField] private TMP_Text timerText;

    private float timer = 0f;
    private void Start()
    {
        Initialize();
    }
    
    private void Update()
    {   // Timer for running
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.CurrentGameState == GameState.InGame)
        {
            timer += Time.deltaTime;
            UpdateTimerText();
        }
    }

    public void Initialize()
    {
        playButton.SetActive(true);
        resetButton.SetActive(false);
        gameOverButton.SetActive(false);
    }

    public void OnPlayPressed()
    {
        playButton.SetActive(false);
        resetButton.SetActive(true);
        gameOverButton.SetActive(true);
        ResetTimer();
    }

    public void OnResetPressed()
    {
        Initialize();
    }

    public void ResetTimer()
    {
        timer = 0f;
        UpdateTimerText();
    }

    // updating the text
    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60f); // counting minutes
        int seconds = Mathf.FloorToInt(timer % 60f); // counting seconds

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
