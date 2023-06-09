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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) //player is Holding a plate
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetkitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else //Player is not carrying Plate but something else
                {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) //Counter is Holding a Plate
                    {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetkitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
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