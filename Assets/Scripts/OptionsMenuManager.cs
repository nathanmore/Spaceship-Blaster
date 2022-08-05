using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenuManager : MenuButtons
{
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;
    [SerializeField]
    private Toggle muteToggle;

    public void Start()
    {
        musicSlider.value = OptionSettings.MusicVolume;
        sfxSlider.value = OptionSettings.SFXVolume;
        muteToggle.isOn = OptionSettings.IsAudioMute;
    }

    public void Update()
    {
        if (muteToggle.isOn == true)
        {
            musicSlider.interactable = false;
            sfxSlider.interactable = false;
        }
        else
        {
            musicSlider.interactable = true;
            sfxSlider.interactable = true;
        }
    }

    public void AudioMute(bool newValue)
    {
        OptionSettings.SetMuteValue(newValue);
    }

    public void SetMusicVolume(float newValue)
    {
        OptionSettings.SetMusicVolume(newValue);
    }

    public void SetSFXVolume(float newValue)
    {
        OptionSettings.SetSFXVolume(newValue);
    }
}
