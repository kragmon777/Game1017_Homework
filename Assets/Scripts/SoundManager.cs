using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        // Checking if we have instance before
        if (Instance !=null && Instance !=this)
        {
            Destroy(gameObject); // if yes -  destroy this
            return;
        }

        Instance = this;  // if no attach this one
        DontDestroyOnLoad(gameObject);
    }
    // Method for setting volume for music source
    public void SetMusicVolume(float value)
{
    musicSource.volume = value;

    // Checking if SoundManager got a value from slider and playing Music Source
    if (value > 0f)
    {   
        if (!musicSource.isPlaying)
            musicSource.Play();
    }
    else
    {
        if (musicSource.isPlaying)
            musicSource.Stop();
    }
}


    // Method for setting volume for SFX source
    public void SetSfxVolume(float value)
    {
        sfxSource.volume = value;

    // Checking if SoundManager got a value from slider and playing SFX source
        if (value > 0f)
    {
        if (!sfxSource.isPlaying)
            sfxSource.Play();
    }
    else
    {
        if (sfxSource.isPlaying)
            sfxSource.Stop();
    }
    }
}
