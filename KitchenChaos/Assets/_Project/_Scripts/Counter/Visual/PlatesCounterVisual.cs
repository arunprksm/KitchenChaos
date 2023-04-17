using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> platesVisualGameObjectsList;

    private void Awake()
    {
        platesVisualGameObjectsList = new List<GameObject>();
    }
    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_onPlateSpawned;
    }

    private void PlatesCounter_onPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        float plateOffSetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffSetY * platesVisualGameObjectsList.Count, 0);
        platesVisualGameObjectsList.Add(plateVisualTransform.gameObject);
    }
}
