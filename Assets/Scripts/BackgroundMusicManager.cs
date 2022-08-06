using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager instance;

    [SerializeField]
    private float volumeOffset = 0.8f;
    private AudioSource m_AudioSource;

    public void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject); // Maintain music even when transitioning scenes
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OptionSettings.IsAudioMute == true)
        {
            m_AudioSource.volume = 0f;
        }
        else
        {
            m_AudioSource.volume = OptionSettings.MusicVolume * volumeOffset;
        }
    }
}
