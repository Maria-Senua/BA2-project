using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    public float moveDistance = 5f;  
    public float moveSpeed = 2f;     

    private Vector3 originalPosition;
    private Vector3 targetPosition;   
    private Vector3 lastPosition;
    private Vector3 movementDelta;
    public static Vector3 currentDelta { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position; 
        targetPosition = originalPosition;
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        movementDelta = transform.position - lastPosition;
        currentDelta = movementDelta;
        lastPosition = transform.position;
    }

    public void TogglePlatformMovement()
    {
        if (transform.position == originalPosition)
        {
            targetPosition = originalPosition + Vector3.up * moveDistance; 
        }
        else
        {
            targetPosition = originalPosition; 
        }
    }

    
}
