using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] protected KitchenObjectSO kitchenObjectsSO;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject()) //There is No Kitchen Object on the Counter
        {
            if (player.HasKitchenObject()) //Player carrying something
            {
                player.GetKitchenObject().SetKitchenObjectParent(this); //place Kitchen Object on the Counter from Player
            }
            else
            {
                //Player not carrying anything
            }
        }
        else //There is Kitchen Object on the Counter
        {
            if (player.HasKitchenObject()) //Player carrying something
            {
                if (player.GetKitchenObject() is PlateKitchenObject) //player is Holding a plate
                {
                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetkitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                //Player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}