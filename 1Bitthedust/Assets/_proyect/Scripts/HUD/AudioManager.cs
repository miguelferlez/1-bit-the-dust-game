using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixerGroup master;
    public AudioMixerGroup music;
    public AudioMixerGroup sfx;

    public float minVol = -80;
    public float maxVol = 0;

    public float MasterVolume
    {
        get
        {
            float volume;
            master.audioMixer.GetFloat("volume", out volume);
            return (volume - minVol) / (maxVol - minVol);
        }

        set
        {
            float volume = minVol + value * (maxVol - minVol);
            master.audioMixer.SetFloat("volume", volume);
        }
    }
    public float MusicVolume
    {
        get
        {
            float volume;
            music.audioMixer.GetFloat("volumeMusic", out volume);
            return (volume - minVol) / (maxVol - minVol);
        }

        set
        {
            float volume = minVol + value * (maxVol - minVol);
            music.audioMixer.SetFloat("volumeMusic", volume);
        }
    }
    public float SfxVolume
    {
        get
        {
            float volume;
            sfx.audioMixer.GetFloat("volumeSFX", out volume);
            return (volume - minVol) / (maxVol - minVol);
        }

        set
        {
            float volume = minVol + value * (maxVol - minVol);
            sfx.audioMixer.SetFloat("volumeSFX", volume);
        }
    }

    public void Awake()
    {
        Instance = this;
    }
}
