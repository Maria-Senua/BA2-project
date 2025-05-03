using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialog : MonoBehaviour
{

    public DialogRunner dialogRunner;
    public Sprite portrait;
    public DialogSequence dialogSequence;
    public DialogSequence newDialogSequence;
    int currentDialogID;

    public float interactionDistance = 3.0f;
    private bool isEngaged;

    public bool proximityEngage = false;
    private bool hasTalked = false;

    // Start is called before the first frame update
    void Start()
    {
        Disengage();
    }

    void Engage()
    {
        if (dialogSequence.lines.Length == 0) return;
        currentDialogID = 0;
        dialogRunner.Show();
        isEngaged = true;
    }

    void Disengage()
    {
        dialogRunner.Hide();
        isEngaged = false;
        hasTalked = true;
    }

    public void Interact()
    {
        if (!isEngaged)
        {
            Engage();
        } else
        {
            currentDialogID++;

            if (currentDialogID >= dialogSequence.lines.Length)
            {
                Disengage();
                return;
            }
        }

        dialogRunner.SetDialog(dialogSequence.lines[currentDialogID], portrait);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsInRange()) Interact();

        if (isEngaged)
        {
            if (!IsInRange()) Disengage();
        }
        else if (proximityEngage && IsInRange() && !hasTalked)
        {
            Interact(); 
        }
        else if (!IsInRange())
        {
            hasTalked = false; 
        }

    }

    bool IsInRange()
    {
        return Vector3.Distance(transform.position, Camera.main.transform.position) < interactionDistance;
        
    }

    public void SwapDialog()
    {
        dialogSequence = newDialogSequence;
    }
}
