using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider soundBar;
    public Slider musicBar;
    public Slider sfxBar;
    
    public void OnEnable()
    {
        soundBar.value = AudioManager.Instance.MasterVolume;
        musicBar.value = AudioManager.Instance.MusicVolume;
        sfxBar.value = AudioManager.Instance.SfxVolume;
    }

    public void Update()
    {
        AudioManager.Instance.MasterVolume = soundBar.value;       
        AudioManager.Instance.MusicVolume = musicBar.value;       
        AudioManager.Instance.SfxVolume = sfxBar.value;       
    }
}
