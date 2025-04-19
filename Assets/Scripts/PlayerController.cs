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

    private void Awake()
    {
        characterController = gameObject.GetComponent<CharacterController>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Vector3.zero;

        if (characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalMovement = Mathf.Sqrt(jumpHeight * 2f * gravity);
            }
            else if (verticalMovement <= 0f) 
            {
                verticalMovement = -1f;
            }
        }
        else
        {
            verticalMovement -= gravity * Time.deltaTime;
           
        }


        if (Input.GetKey(KeyCode.W))
        {
            moveDirection = gameObject.transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection = -gameObject.transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        Vector3 finalMove = moveDirection.normalized * speed * Time.deltaTime;

        finalMove.y = verticalMovement * Time.deltaTime;

        if (characterController.isGrounded)
        {
            finalMove += PlatformBehaviour.currentDelta;
        }

        characterController.Move(finalMove);

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
