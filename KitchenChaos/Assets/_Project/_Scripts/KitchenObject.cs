using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private ClearCounter clearCounter;
    public KitchenObjectSO GetkitchenObject()
    {
        return kitchenObjectSO;
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
    public void SetClearCounter(ClearCounter _clearCounter)
    {
        if (clearCounter != null)
        {
            clearCounter.ClearKitchenObject();
        }
        clearCounter = _clearCounter;
        if (_clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter already has a Kitchen Object");
        }
        _clearCounter.SetKitchenObject(this);
        transform.parent = _clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
}