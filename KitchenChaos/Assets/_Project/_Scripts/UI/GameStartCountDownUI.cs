using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    private KitchenGameManager kitchenGameManager;
    private void Start()
    {
        kitchenGameManager = KitchenGameManager.Instance;
        kitchenGameManager.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }

    private void Update()
    {
        countDownText.text = Mathf.Ceil(kitchenGameManager.GetCountDownToStartTimer()).ToString();
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