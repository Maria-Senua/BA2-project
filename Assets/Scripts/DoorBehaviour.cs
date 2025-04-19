using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public enum DoorState
    {
        Locked,
        Unlocked,
        Open
    }

    private DoorState currentState = DoorState.Locked;

    public enum DoorType
    {
        Locked,         
        Automatic       
    }

    public DoorType doorType = DoorType.Automatic;


    private Animator doorAnimator;


    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleDoor()
    {
        doorAnimator.SetTrigger("ToggleDoor");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (doorType == DoorType.Automatic || currentState == DoorState.Unlocked)
            {
                OpenDoor();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           if (doorType == DoorType.Automatic) doorAnimator.Play("doorClose");
        }
    }

    

    public void OpenDoor()
    {
        doorAnimator.Play("doorOpen");
        currentState = DoorState.Open;
    }

    public void UnlockDoor()
    {
        currentState = DoorState.Unlocked;
    }

    public void LockDoor()
    {
        currentState = DoorState.Locked;
    }
}
