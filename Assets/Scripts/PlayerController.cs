using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private enum PlayerState
    {
        IDLE,
        WALK,
        JUMP
    }

    private PlayerState currentState = PlayerState.IDLE;

    private CharacterController characterController;
    private Vector3 moveDirection;
    public float speed;
    public float rotationSpeed;
    public float gravity = 9.81f;
    private float verticalMovement;
    public float jumpHeight;

    public GameObject keyIcon;
    public UnityEvent onKeyPickup;

    private Transform currentPlatform = null;
    private Transform previousPlatform = null;


    private void Awake()
    {
        characterController = gameObject.GetComponent<CharacterController>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Jump()
    {
        verticalMovement = Mathf.Sqrt(jumpHeight * 2f * gravity);
        currentState = PlayerState.JUMP;
    }

    private void SetMovementDirection(float movementFactor = 1.0f)
    {

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection = gameObject.transform.forward * movementFactor;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection = -gameObject.transform.forward * movementFactor;
        }
    }

    private void StateUpdate()
    {
        switch (currentState)
        {
            case PlayerState.IDLE:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();   
                } else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    currentState = PlayerState.WALK;
                }
                break;

            case PlayerState.WALK:

                SetMovementDirection();

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                } else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                {
                    currentState = PlayerState.IDLE;
                }
                break;

            case PlayerState.JUMP:
                if (characterController.isGrounded)
                {
                    currentState = PlayerState.IDLE;
                }
                SetMovementDirection(0.25f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if (characterController.isGrounded)
        {
           
             verticalMovement = -1f;
            
        }
        else
        {
            verticalMovement -= gravity * Time.deltaTime;
           
        }

        moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        StateUpdate();

        Vector3 finalMove = moveDirection.normalized * speed * Time.deltaTime;

        finalMove.y = verticalMovement * Time.deltaTime;

        if (characterController.isGrounded)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f)) 
            {
                PlatformBehaviour platform = hit.collider.GetComponent<PlatformBehaviour>();
                if (platform != null)
                {
                    currentPlatform = platform.transform;
                }
                else
                {
                    currentPlatform = null;
                }
            }
        }
        else
        {
            currentPlatform = null;
        }

       

        characterController.Move(finalMove);

        if (currentPlatform != previousPlatform)
        {
            if (currentPlatform != null)
            {
                transform.SetParent(currentPlatform); 
            }
            else
            {
                transform.SetParent(null); 
            }

            previousPlatform = currentPlatform;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            other.gameObject.SetActive(false);
            keyIcon.SetActive(true);
            onKeyPickup.Invoke();
        }
    }
}
