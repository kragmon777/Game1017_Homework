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

    public SegmentSpawner segmentSpawner;
    public PlayerController player;
    private bool hasInitialized = false;

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
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CurrentGameState = GameState.InMenu;
    }
    
    public void PlayGame()
    {
        SetGameState(GameState.InGame);
       
        segmentSpawner.Initialize();
        player.Initialize();
    }

    public void GameOver()
    {
        SetGameState(GameState.GameOver);
        SceneManager.LoadScene("GameOverScene");
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        player.Reset();
        segmentSpawner.Reset();
        SetGameState(GameState.InMenu);
    }

    void SetGameState(GameState state)
    {
        CurrentGameState = state;
    }
}
