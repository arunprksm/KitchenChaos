using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectsSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;

    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondClearCounter);
            }
        }
    }
    public void Interact()
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            Debug.Log(kitchenObject.GetClearCounter());
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject _kitchenObject) { kitchenObject = _kitchenObject; }

    public KitchenObject GetKitchenObject() { return kitchenObject; }

    public void ClearKitchenObject() { kitchenObject = null; }

    public bool HasKitchenObject() { return kitchenObject != null; }
}