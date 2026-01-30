using UnityEngine;

public class MusicSliderScript : MonoBehaviour
{   
    // set a new value of volume by slider and send this to SoundManager
    public void VolumeUpdate(float value)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetMusicVolume(value);
        }
    }
}
