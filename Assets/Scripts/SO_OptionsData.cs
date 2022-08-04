using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Options-Data", menuName = "ScriptableObjects/SO_OptionsData", order = 1)]
public class SO_OptionsData : ScriptableObject
{
    [SerializeField]
    private float defaultSFXVolume;
    [SerializeField]
    private float defaultMusicVolume;

    private float sfxVolume;
    private float musicVolume;
    private bool muteAudio;

    public float SFXVolume { get { return sfxVolume; } }
    public float MusicVolume { get { return musicVolume; } }
    public bool IsAudioMute { get { return muteAudio; } }

    public void Awake()
    {
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("sfxvolume");
        }
        else
        {
            sfxVolume = defaultSFXVolume;
        }

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            musicVolume = defaultMusicVolume;
        }

        if (PlayerPrefs.HasKey("MuteAudio"))
        {
            int val = PlayerPrefs.GetInt("MuteAudio");
            if (val == 0)
            {
                muteAudio = true;
            }
            else
            {
                muteAudio = false;
            }
        }
        else
        {
            muteAudio = false;
        }
    }

    public void SetSFXVolume(float newValue)
    {
        if (newValue < 0f)
        {
            sfxVolume = 0f;
        }
        else if (newValue > 1f)
        {
            sfxVolume = 1f;
        }
        else
        {
            sfxVolume = newValue;
        }

        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
    }

    public void SetMusicVolume(float newValue)
    {
        if (newValue < 0f)
        {
            musicVolume = 0f;
        }
        else if (newValue > 1f)
        {
            musicVolume = 1f;
        }
        else
        {
            musicVolume = newValue;
        }

        PlayerPrefs.SetFloat("musicVolume", musicVolume);
    }

    public void SetMuteValue(bool newValue)
    {
        muteAudio = newValue;

        if (newValue == true)
        {
            PlayerPrefs.SetInt("MuteAudio", 0);
        }
        else
        {
            PlayerPrefs.SetInt("MuteAudio", 1);
        }
    }
}
