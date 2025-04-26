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
    public float maxDistance;
    public GameObject splash;
    public int groupID = 0;

    private bool isPouring = false;    


    private static List<ButtonBehaviour> allButtons = new List<ButtonBehaviour>();
    private static Dictionary<int, bool> groupStates = new Dictionary<int, bool>();

    public UnityEvent onButtonPress;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        buttonAnimator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        splash.SetActive(false);
        allButtons.Add(this);

        if (!groupStates.ContainsKey(groupID))
        {
            groupStates[groupID] = false; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (Vector3.Distance(gameObject.transform.position, Camera.main.transform.position) < maxDistance)
        {
            if (groupID == 2 && isPouring) return;
            Switch();
                
        }
    }

    private void Switch()
    {
        if (groupID == 2)
        {
            if (!CoffeeBehaviour.instance.isEmpty || CoffeeBehaviour.instance.cupsNum == 0)
                return;
        }

        groupStates[groupID] = !groupStates[groupID];
        audioSource.Play();
        foreach (ButtonBehaviour button in allButtons)
        {
            if (button.groupID == this.groupID)
            {
                button.ChangeButtonMaterial();
                button.PressButtonAnim();
            }

            if (button.groupID == 2)
            {
                button.isPouring = groupStates[groupID];
                Invoke("Return", 8f);
            } 
        }
            
        onButtonPress.Invoke();
    }

  

    private void Return()
    {
        if (groupID == 2)
        {
            groupStates[groupID] = false;
            isPouring = false;
            ChangeButtonMaterial();
        }
    }

    private void PressButtonAnim()
    {
        buttonAnimator.Play("GoDown");
        buttonAnimator.SetTrigger("Pressed");
    }

    private void ChangeButtonMaterial()
    {
        bool state = groupStates[groupID];

        meshRenderer.material = state ? mActive : mInactive;
        if (groupID == 0) splash.SetActive(state);
        //if (groupID == 2)
        //{
        //    bool showSplash = state && CoffeeBehaviour.instance.isEmpty && CoffeeBehaviour.instance.cupsNum > 0;
        //    splash.SetActive(showSplash);
        //}
        //else
        //{
        //    splash.SetActive(state);
        //}
    }
  
}
