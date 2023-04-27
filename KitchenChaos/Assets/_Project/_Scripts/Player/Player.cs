using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [Header("Player Control")]
    [Space(8)]

    [SerializeField] private float playerMoveSpeed = 7.0f;
    [SerializeField] private float playerRotationRate = 10.0f;

    [Space(8)]
    [SerializeField] private GameInput gameInput = null;
    Vector3 lastInteractDir;
    [SerializeField] private LayerMask countersLayerMask;

    [SerializeField] private Transform kitchenObjectHoldPoint;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;



    private bool isWalking = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Duplication of Singleton class " + Instance + " is not Allowed");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
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
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //Has Clear Counter
                //clearCounter.Interact();
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
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

        if (!playerCanMove)
        {
            //Cannot move towards moveDir
            //Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            playerCanMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + (transform.up * playerHeight), playerRadius, moveDirX, moveDistance);
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
                playerCanMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + (transform.up * playerHeight), playerRadius, moveDirZ, moveDistance);
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
        if (moveDir.magnitude > 0.0f)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, playerRotationRate * Time.deltaTime);
        }

        //transform.forward = Vector3.Slerp(transform.forward, moveDir, playerRotationRate * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void SetSelectedCounter(BaseCounter _selectedCounter)
    {
        selectedCounter = _selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform() { return kitchenObjectHoldPoint; }

    public void SetKitchenObject(KitchenObject _kitchenObject)
    {
        kitchenObject = _kitchenObject;
        if (kitchenObject != null) OnPickedSomething?.Invoke(this, EventArgs.Empty);
    }

    public KitchenObject GetKitchenObject() { return kitchenObject; }

    public void ClearKitchenObject() { kitchenObject = null; }

    public bool HasKitchenObject() { return kitchenObject != null; }
}