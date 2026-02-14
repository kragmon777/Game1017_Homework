using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    private void OnEnable() 
    {
        GetComponent<Button>().onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(RestartGame);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
