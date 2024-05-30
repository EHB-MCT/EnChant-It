using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private List<AudioSource> _audioSources = new List<AudioSource>();

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            _audioSources.Add(newSource);
        }
    }

    void OnDisable()
    {
        foreach (var source in _audioSources)
        {
            if (source != null)
            {
                Destroy(source);
            }
        }
        _audioSources.Clear();
    }
}
