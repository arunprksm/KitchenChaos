using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [Header("Player Control")]
    [Space(8)]

    [SerializeField] private float playerMoveSpeed = 7.0f;
    [SerializeField] private float playerRotationRate = 10.0f;
    [SerializeField] private GameInput gameInput = null;

    private bool isWalking = false;
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, transform.position.y, inputVector.y);
        isWalking = moveDir != Vector3.zero;
        transform.position += playerMoveSpeed * Time.deltaTime * moveDir;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, playerRotationRate * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}