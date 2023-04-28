using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";


    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;
    private float volume = 0.3f;
    private float minVolume = 0f;
    private float maxVolume = 1f;


    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 0.3f);
        audioSource.volume = volume;
    }

    public void IncreaseVolume()
    {
        volume += 0.1f;

        if (volume > maxVolume)
        {
            volume = 1f;
        }
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);

    }
    public void DecreaseVolume()
    {
        volume -= 0.1f;

        if (volume < minVolume)
        {
            volume = 0f;
        }
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return volume;
    }
}
