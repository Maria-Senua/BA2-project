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
    private AudioSource audioSource;
    private static bool switchState = false;
    public float maxDistance;
    public GameObject splash;
    public int groupID = 0;

    private static List<ButtonBehaviour> allButtons = new List<ButtonBehaviour>();

    public UnityEvent onButtonPress;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        buttonAnimator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        splash.SetActive(false);
        allButtons.Add(this);
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
        audioSource.Play();
        foreach (ButtonBehaviour button in allButtons)
        {
            if (button.groupID == this.groupID)
            {
                button.ChangeButtonMaterial();
                button.PressButtonAnim();
            }
                
           
        }
            
        onButtonPress.Invoke();
    }

    private void PressButtonAnim()
    {
        buttonAnimator.Play("GoDown");
        buttonAnimator.SetTrigger("Pressed");
    }

    private void ChangeButtonMaterial()
    {
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
    }
  
}
