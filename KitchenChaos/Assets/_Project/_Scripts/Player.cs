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
        float playerRadius = 0.7f;
        float playerHeight = 2.0f;
        float moveDistance = playerMoveSpeed * Time.deltaTime;
        bool playerCanMove = !Physics.CapsuleCast(transform.position, transform.position + transform.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!playerCanMove)
        {
            //Cannot move towards moveDir

            //Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            playerCanMove = !Physics.CapsuleCast(transform.position, transform.position + transform.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (playerCanMove)
            {
                //playerCanMove only on X direction
                moveDir = moveDirX;
            }
            else
            {
                //Cannot move towards moveDir

                //Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                playerCanMove = !Physics.CapsuleCast(transform.position, transform.position + transform.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (playerCanMove)
                {
                    //playerCanMove only on Z direction
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot move in any Direction
                }
            }
        }
        if (playerCanMove) transform.position += moveDistance * moveDir;
        isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, playerRotationRate * Time.deltaTime);
        //Debug.Log(moveDir);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}