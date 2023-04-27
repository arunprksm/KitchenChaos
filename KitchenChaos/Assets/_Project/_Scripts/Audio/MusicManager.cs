using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{


    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;
    private float volume = 0.3f;
    private float minVolume = 0f;
    private float maxVolume = 1f;


    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    //public void ChangeVolume()
    //{
    //    volume += 0.1f;
    //    if (volume > maxVolume)
    //    {
    //        volume = 0f;
    //    }
    //    audioSource.volume = volume;
    //}

    public void IncreaseVolume()
    {
        volume += 0.1f;

        if (volume > maxVolume)
        {
            volume = 1f;
        }
        audioSource.volume = volume;

    }
    public void DecreaseVolume()
    {
        volume -= 0.1f;

        if (volume < minVolume)
        {
            volume = 0f;
        }
        audioSource.volume = volume;

    }
    public float GetVolume()
    {
        return volume;
    }
}
