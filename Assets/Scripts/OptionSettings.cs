using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OptionSettings
{
    [SerializeField]
    private static float defaultSFXVolume = 0.75f;
    [SerializeField]
    private static float defaultMusicVolume = 0.75f;

    private static float sfxVolume;
    private static float musicVolume;
    private static bool muteAudio;

    public static float SFXVolume { get { return sfxVolume; } }
    public static float MusicVolume { get { return musicVolume; } }
    public static bool IsAudioMute { get { return muteAudio; } }

    public static void SetupOptions()
    {
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
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

    public static void SetSFXVolume(float newValue)
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

    public static void SetMusicVolume(float newValue)
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

    public static void SetMuteValue(bool newValue)
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

    public static void SystemMute()
    {
        muteAudio = true;
    }
}
