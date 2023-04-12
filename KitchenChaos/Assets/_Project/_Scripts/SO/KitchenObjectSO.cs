using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
