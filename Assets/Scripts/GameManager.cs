using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    InMenu,
    InGame,
    GameOver
}

    public class GameManager : MonoBehaviour
    {
    public static GameManager Instance;
    public GameState CurrentGameState{get; private set;}
    
    //[SerializeField] private SoundManager soundManager;
    private SoundManager soundManager;
    public SoundManager SoundManager
    {
        get
        {
            if ( soundManager == null )
            {
                soundManager = FindFirstObjectByType<SoundManager>();
            }
            return soundManager;
        }
        private set
        {
            soundManager = value;
        }
    }


    private void Awake()
    {
        if (Instance !=null)
        {
            Destroy(gameObject);
            return; 
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CurrentGameState = GameState.InMenu;
    }
    
    public void PlayGame()
    {
        SetGameState(GameState.InGame);
    }

    public void GameOver()
    {
        SetGameState(GameState.GameOver);
        SceneManager.LoadScene("GameOverScene");
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        FindFirstObjectByType<PlayerController>().ResetPlayer();
        SetGameState(GameState.InMenu);
    }

    void SetGameState(GameState state)
    {
        CurrentGameState = state;
    }
}
