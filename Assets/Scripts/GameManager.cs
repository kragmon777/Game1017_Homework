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

    public Leaderboard Leaderboard => FindFirstObjectByType<Leaderboard>();
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
    private SegmentSpawner segmentSpawner;
    public SegmentSpawner SegmentSpawner
    {
        get
        {
            if (segmentSpawner == null)
            {
                segmentSpawner = FindFirstObjectByType<SegmentSpawner>();
            }
            return segmentSpawner;
        }
        private set
        {
            segmentSpawner = value;
        }
    }

    private PlayerController player;
    public PlayerController Player
    {
        get
        {
            if (player == null)
            {
                player = FindFirstObjectByType<PlayerController>();
            }
            return player;
        }
        private set
        {
            player = value;
        }
    }

    private UIManager uiManager;
    public UIManager UIManager
    {
        get
        {
            if (uiManager == null)
            {
                uiManager = FindFirstObjectByType<UIManager>();
            }
            return uiManager;
        }
        private set
        {
            uiManager = value;
        }
    }
    private BackgroundManager backgroundManager;
    public BackgroundManager BackgroundManager
    {
        get
        {
            if (backgroundManager == null)
            {
                backgroundManager = FindFirstObjectByType<BackgroundManager>();
            }
            return backgroundManager;
        }
        private set
        {
            backgroundManager = value;
        }
    }

    private Timer timer;
    public Timer Timer
    {
        get
        {
            if (timer == null)
            {
                timer = FindFirstObjectByType<Timer>();
            }
            return timer;
        }
        private set
        {
            timer = value;
        }
    }

    private SaveSystem saveSystem;
    public SaveSystem SaveSystem
    {
        get
        {
            if (saveSystem == null)
            {
                saveSystem = FindFirstObjectByType<SaveSystem>();
            }
            return saveSystem;
        }
        private set
        {
            saveSystem = value;
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene == SceneManager.GetSceneByBuildIndex(1))
        {
            GameSceneStart();
        }

        if (scene == SceneManager.GetSceneByBuildIndex(2))
        {
            GameOverSceneStart();
        }
    }

    private void Start()
    {
        CurrentGameState = GameState.InMenu;
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
        CurrentGameState = GameState.InMenu;
    }

    public void StartGame()
    {
        SetGameState(GameState.InGame);
        SegmentSpawner.Initialize();
        Player.Initialize();
        UIManager.OnPlayPressed();
        BackgroundManager.Initialize();
        Timer.Initialize();
    }

    public void GameOver()
    {
        SetGameState(GameState.GameOver);

        SaveSystem.SaveTimer(Timer.GetCurrentBestTime());
        SceneManager.LoadScene("GameOverScene");
    }

    private void GameSceneStart()
    {
        UIManager.Initialize();
    }

    public void GameOverSceneStart()
    {
        Leaderboard.Initialize(SaveSystem.GetScores());
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        Player.Reset();
        SegmentSpawner.Reset();
        SetGameState(GameState.InMenu);
        UIManager.OnResetPressed();
        BackgroundManager.Reset();
        Timer.Reset();
    }

    void SetGameState(GameState state)
    {
        CurrentGameState = state;
    }
}
