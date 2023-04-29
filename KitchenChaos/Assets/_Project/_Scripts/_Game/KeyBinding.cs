using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBinding : MonoBehaviour
{

    [Header("Button Text")]
    [Space(8)]
    [SerializeField] private TextMeshProUGUI moveUPText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamePadInteractText;
    [SerializeField] private TextMeshProUGUI gamePadInteractAltText;
    [SerializeField] private TextMeshProUGUI gamePadPauseText;

    [Header("Buttons")]
    [Space(8)]
    [SerializeField] private Button moveUPButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button gamePadInteractButton;
    [SerializeField] private Button gamePadInteractAltButton;
    [SerializeField] private Button gamePadPauseButton;

    [SerializeField] private Transform pressToRebindKeyTransform;


    private void Awake()
    {
        moveUPButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Up);
        });
        moveDownButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Down);
        });
        moveLeftButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Left);
        });
        moveRightButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Right);
        });
        interactButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Interact);
        });
        interactAltButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Interact_Alt);
        });
        pauseButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Pause);
        });
        gamePadInteractButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.GamePad_Interact);
        });
        gamePadInteractAltButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.GamePad_Interact_Alt);
        });
        gamePadPauseButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.GamePad_Pause);
        });
    }
    private void Start()
    {
        UpdateVisual();
        HidePressToRebindKey();
    }

    private void UpdateVisual()
    {
        moveUPText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alt);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamePadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_Interact);
        gamePadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_Interact_Alt);
        gamePadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_Pause);
    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.ReBindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
