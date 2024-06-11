using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusicManager : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip BackgroundMusic;
    public AudioClip CombatMusic;
    public GameManager GameManager;

    private bool _isCombatMusicPlaying = false;

    void Start()
    {
        AudioSource.clip = BackgroundMusic;
        AudioSource.loop = true; 
        AudioSource.Play();
    }

    void Update()
    {
        if (GameManager.StartWave && !_isCombatMusicPlaying)
        {
            AudioSource.clip = CombatMusic;
            AudioSource.loop = true; 
            AudioSource.Play();
            _isCombatMusicPlaying = true;
        }
    }
}
