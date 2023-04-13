using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [Header("Player Control")]
    [Space(8)]

    [SerializeField] private float playerMoveSpeed = 7.0f;
    [SerializeField] private float playerRotationRate = 10.0f;

    [Space(8)]
    [SerializeField] private GameInput gameInput = null;
    Vector3 lastInteractDir;
    [SerializeField] private LayerMask countersLayerMask;

    private ClearCounter selectedCounter;

    private bool isWalking = false;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Duplication of Singleton class " + Instance + " is not Allowed");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new(inputVector.x, transform.position.y, inputVector.y);
        float interactionDistance = 2.0f;

        if (moveDir != Vector3.zero) lastInteractDir = moveDir;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactionDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //Has Clear Counter
                //clearCounter.Interact();
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);

        }
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new(inputVector.x, transform.position.y, inputVector.y);
        float playerRadius = 0.7f;
        float playerHeight = 2.0f;
        float moveDistance = playerMoveSpeed * Time.deltaTime;
        bool playerCanMove = !Physics.CapsuleCast(transform.position, transform.position + transform.up * playerHeight, playerRadius, moveDir, moveDistance);
        transform.forward = Vector3.Slerp(transform.forward, moveDir, playerRotationRate * Time.deltaTime);

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
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void SetSelectedCounter(ClearCounter _selectedCounter)
    {
        selectedCounter = _selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}