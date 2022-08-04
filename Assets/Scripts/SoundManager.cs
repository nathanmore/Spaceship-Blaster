using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager{
    public enum Sound{
        enemyDie,
        playerAttack,
        playerHit,
        playerPowerup,
    }

    public static void PlaySound(Sound sound){
      GameObject soundGameObject = new GameObject("Sound");
      AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
      //audioSource.PlayOneShot(GameAssets.i.playerAttack);  
    }
}
   
