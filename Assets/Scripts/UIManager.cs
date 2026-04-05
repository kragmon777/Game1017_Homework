using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject playButton, resetButton, gameOverButton;
    [SerializeField] private GameObject timerUI;

    public void Initialize()
    {
        playButton.SetActive(true);
        resetButton.SetActive(false);
        gameOverButton.SetActive(false);
        timerUI.SetActive(false);
    }

    public void OnPlayPressed()
    {
        playButton.SetActive(false);
        resetButton.SetActive(true);
        gameOverButton.SetActive(true);
        timerUI.SetActive(true);
    }

    public void OnResetPressed()
    {
        Initialize();
    }

}
