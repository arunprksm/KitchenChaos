using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    private KitchenGameManager kitchenGameManager;
    private void Start()
    {
        kitchenGameManager = KitchenGameManager.Instance;
        kitchenGameManager.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (kitchenGameManager.IsGameOver())
        {
            Show();
            UpdateRecipesDeliveredText();
        }
        else
        {
            Hide();
        }
    }
    private void UpdateRecipesDeliveredText()
    {
        DeliveryManager deliveryManager = DeliveryManager.Instance;
        recipesDeliveredText.text = deliveryManager.GetSuccessfulRecipesAmount().ToString();
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
