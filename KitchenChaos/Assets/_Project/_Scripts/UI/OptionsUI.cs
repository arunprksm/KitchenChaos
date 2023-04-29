using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Button sfxIncreaseButton;
    [SerializeField] private Button sfxDecreaseButton;

    [SerializeField] private Button musicIncreaseButton; 
    [SerializeField] private Button musicDecreaseButton;


    private Action onCloseButtonAction;
    private void Awake()
    {
        Instance = this;
        closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });

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
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        Hide();
        UpdateVisual();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        sfxText.text = "SFX: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }

    public void Show(Action _onCloseButtonAction)
    {
        onCloseButtonAction = _onCloseButtonAction;
        gameObject.SetActive(true);
        sfxDecreaseButton.Select();
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}