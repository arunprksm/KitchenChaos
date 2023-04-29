using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{

    private const string NUMBER_POPUP = "NumberPopUp";
    [SerializeField] private TextMeshProUGUI countDownText;
    private Animator animator;
    private int previousCountDownNumber;
    private KitchenGameManager kitchenGameManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        kitchenGameManager = KitchenGameManager.Instance;
        kitchenGameManager.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }

    private void Update()
    {
        int countDownNumber = Mathf.CeilToInt(kitchenGameManager.GetCountDownToStartTimer());
        countDownText.text = countDownNumber.ToString();

        if(previousCountDownNumber != countDownNumber)
        {
            previousCountDownNumber = countDownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager soundManager = SoundManager.Instance;
            soundManager.PlayCountDownSound();
        }
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (kitchenGameManager.IsCountDownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}