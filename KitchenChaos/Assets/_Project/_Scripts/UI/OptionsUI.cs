using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    //[SerializeField] private Button sfxButton;
    //[SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Button sfxIncreaseButton;
    [SerializeField] private Button sfxDecreaseButton;

    [SerializeField] private Button musicIncreaseButton; 
    [SerializeField] private Button musicDecreaseButton;

    private void Awake()
    {
        //sfxButton.onClick.AddListener(() =>
        //{
        //    SoundManager.Instance.ChangeVolume();
        //    UpdateVisual();
        //});
        //musicButton.onClick.AddListener(() =>
        //{
        //    MusicManager.Instance.ChangeVolume();
        //    UpdateVisual();
        //});

        //SFX
        sfxIncreaseButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.IncreaseVolume();
            UpdateVisual();
        });
        sfxDecreaseButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.DecreaseVolume();
            UpdateVisual();
        });
        //Music
        musicIncreaseButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.IncreaseVolume();
            UpdateVisual();
        });
        musicDecreaseButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.DecreaseVolume();
            UpdateVisual();
        });
    }

    private void Start()
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        sfxText.text = "SFX: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }
}