using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager{

    public enum Sound{
        enemyDie,
        playerAttack,
        playerHit,
        playerPowerup,
        playerHit2,
        hit,
        shoot
    }

    public static void PlaySound(Sound sound, float volumeOffset = 1.0f){
      GameObject soundGameObject = new GameObject("Sound");
      AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
      audioSource.PlayOneShot(GetAudioClip(sound), (OptionSettings.SFXVolume * volumeOffset));  
    }

    private static AudioClip GetAudioClip (Sound sound){
      foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray){
        if (soundAudioClip.sound == sound){
          return soundAudioClip.audioClip;
        }
      }
     Debug.LogError("Sound " + sound + " not found!");
     return null;
        
      }
}
   
