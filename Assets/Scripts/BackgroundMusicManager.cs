using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    private AudioSource m_AudioSource;

    public void OnEnable()
    {
        GameObject.DontDestroyOnLoad(this.gameObject); // Maintain music even when transitioning scenes
    }

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        m_AudioSource.volume = OptionSettings.MusicVolume;
    }
}
