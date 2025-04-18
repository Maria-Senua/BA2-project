using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonBehaviour : MonoBehaviour
{
    public Material mActive;
    public Material mInactive;
    private MeshRenderer meshRenderer;
    public Animator doorAnimator;
    private Animator buttonAnimator;
    private bool switchState;
    public float maxDistance;
    public GameObject splash;

    public UnityEvent onButtonPress;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        buttonAnimator = gameObject.GetComponent<Animator>();
        splash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (Vector3.Distance(gameObject.transform.position, Camera.main.transform.position) < maxDistance)
        {
            Switch();
        }
    }

    private void Switch()
    {
        switchState = !switchState;
        //doorAnimator.SetTrigger("ToggleDoor");
        
        buttonAnimator.Play("GoDown");
        if (switchState == true)
        {
            meshRenderer.material = mActive;
            splash.SetActive(true);
            
        }
        else
        {
            meshRenderer.material = mInactive;
            splash.SetActive(false);
        }
        onButtonPress.Invoke();
        Invoke("PutButtonBack", 1f);
    }

    private void PutButtonBack()
    {
        buttonAnimator.SetTrigger("Pressed");
    }
}
