using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private List<AudioSource> audioSources = new List<AudioSource>();

    void Start()
    {
        // Create and add audio sources as needed
        for (int i = 0; i < 5; i++)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            audioSources.Add(newSource);
        }
    }

    void OnDisable()
    {
        foreach (var source in audioSources)
        {
            if (source != null)
            {
                Destroy(source);
            }
        }
        audioSources.Clear();
    }
}
