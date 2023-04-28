using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBinding : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveUPText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;

    [SerializeField] private Button moveUPButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;


    private void Awake()
    {
        moveUPButton.onClick.AddListener(() =>
        {
            GameInput.Instance.ReBindBinding(GameInput.Binding.Move_Up);
        });
        moveDownButton.onClick.AddListener(() =>
        {

        });
        moveLeftButton.onClick.AddListener(() =>
        {

        });
        moveRightButton.onClick.AddListener(() =>
        {

        });
        interactButton.onClick.AddListener(() =>
        {

        });
        interactAltButton.onClick.AddListener(() =>
        {

        });
        pauseButton.onClick.AddListener(() =>
        {

        });
    }
    private void Start()
    {
        UpdateVisual();
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
    }
}
