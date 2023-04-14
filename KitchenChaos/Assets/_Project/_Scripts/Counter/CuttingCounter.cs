using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject()) //There is No Kitchen Object on the CCounter
        {
            if (player.HasKitchenObject()) //Player carrying something
            {
                player.GetKitchenObject().SetKitchenObjectParent(this); //place Kitchen Object on the CCounter from Player
            }
            else
            {
                //Player not carrying anything
            }
        }
        else //There is Kitchen Object on the CCounter
        {
            if (player.HasKitchenObject()) //Player carrying something
            {

            }
            else
            {
                //Player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject()) //There is No Kitchen Object on the CCounter
        {
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }
}
