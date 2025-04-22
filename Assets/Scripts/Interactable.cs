using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteraction;
    public float interactionDistance = 3.0f;

    public void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) < interactionDistance)
        {
            onInteraction.Invoke();
        }
        
    }
}
