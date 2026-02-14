using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    public void ChangeMusicVolume(float newVolume)
    {
        if (musicSource == null) return;

        musicSource.volume = newVolume;

        if (newVolume <= 0.0001f)
        {
            if (musicSource.isPlaying)
            musicSource.Pause();
        }
        else
        {
            if (!musicSource.isPlaying)
            musicSource.Play();
        }
    }

    public void ChangeSFXVolume(float newVolume)
    {
        if (sfxSource == null) return;

        sfxSource.volume = newVolume;

        if (newVolume <= 0.0001f)
        {
            if (sfxSource.isPlaying)
            sfxSource.Pause();
        }
        else
        {
            if (!sfxSource.isPlaying)
            sfxSource.Play();
        }
    }
}
