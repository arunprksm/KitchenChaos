using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectSO GetkitchenObject()
    {
        return kitchenObjectSO;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }
    public void SetKitchenObjectParent(IKitchenObjectParent _kitchenObjectParent)
    {
        if (kitchenObjectParent != null)
        {
            kitchenObjectParent.ClearKitchenObject();
        }
        kitchenObjectParent = _kitchenObjectParent;
        if (_kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError(_kitchenObjectParent + " already has a Kitchen Object");
        }
        _kitchenObjectParent.SetKitchenObject(this);
        transform.parent = _kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
}