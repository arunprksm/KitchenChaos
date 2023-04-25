using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenGameObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenGameObjectSO_GameObject> kitchenGameObjectSOGameObjectList;
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (KitchenGameObjectSO_GameObject kitchenGameObjectSOGameObject in kitchenGameObjectSOGameObjectList)
        {
            kitchenGameObjectSOGameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenGameObjectSO_GameObject kitchenGameObjectSOGameObject in kitchenGameObjectSOGameObjectList)
        {
            if(kitchenGameObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenGameObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}
