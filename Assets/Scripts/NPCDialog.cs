using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialog : MonoBehaviour
{

    public DialogRunner dialogRunner;
    public Sprite portrait;
    public Sprite newPortrait;
    public Sprite portraitAngry, portraitFury, portraitMistake, portraitMock, portraitNormal, portraitPink, portraitRight, portraitSad, portraitShock;
    public DialogSequence npcDialog;
    public DialogSequence interruptDialog;
    private DialogSequence previousDialog;
    public DialogSequence newDialogSequence;
    int currentDialogID;

    public float interactionDistance = 3.0f;
    public float interruptednDistance = 7.0f;
    private bool isEngaged;

    public bool proximityEngage = false;
    private bool hasTalked = false;
    private bool wasInterrupted = false;

    // Start is called before the first frame update
    void Start()
    {
        Disengage();
    }

    void Engage()
    {
        if (npcDialog.lines.Length == 0) return;
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

            if (currentDialogID >= npcDialog.lines.Length)
            {
                Disengage();
                if (npcDialog.next != null) npcDialog = npcDialog.next;

                return;
            }
        }

        ShowCurrentDialogLine();
    }

    void ShowCurrentDialogLine()
    {
        if (wasInterrupted)
        {
            npcDialog = previousDialog;
            wasInterrupted = false;
        }
       

        if (npcDialog.lines.Length == 0 || currentDialogID >= npcDialog.lines.Length) return;

        DialogLine currentLine = npcDialog.lines[currentDialogID];
        dialogRunner.SetDialog(currentLine.text, GetPortraitByEmotion(currentLine.emotion));

        if (currentLine.choices.Length != 0)
        {
            dialogRunner.ShowChoices(currentLine.choices, this);
        }
        else
        {
            dialogRunner.HideChoices();
        }
    }


    Sprite GetPortraitByEmotion(DialogEmotionalState emotionalState)
    {
        switch (emotionalState)
        {
            case DialogEmotionalState.ANGRY:
                return portraitAngry;
            case DialogEmotionalState.FURY:
                return portraitFury;
            case DialogEmotionalState.MISTAKE:
                return portraitMistake;
            case DialogEmotionalState.MOCK:
                return portraitMock;
            case DialogEmotionalState.PINK:
                return portraitPink;
            case DialogEmotionalState.RIGHT:
                return portraitRight;
            case DialogEmotionalState.SAD:
                return portraitSad;
            case DialogEmotionalState.SHOCK:
                return portraitShock;
            default:
                return portraitNormal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsInRange()) Interact();

        if (isEngaged)
        {
            if (!IsInRange())
            {
                if (npcDialog.next == null && !IsInterrupted())
                {
                    Disengage();
                }
                else if (npcDialog.next != null && IsInterrupted())
                {
                    
                    previousDialog = npcDialog;
                    npcDialog = interruptDialog;
                    currentDialogID = 0;
                    dialogRunner.Show(); 
                    ShowCurrentDialogLine();
                    wasInterrupted = true;
                }
            }
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

    bool IsInterrupted()
    {
        return !IsInRange() && Vector3.Distance(transform.position, Camera.main.transform.position) < interruptednDistance;

    }

    public void SwapDialog()
    {
        npcDialog = newDialogSequence;
        portrait = newPortrait;
    }

    public void OnChoiceButton(int btnID)
    {
        npcDialog = npcDialog.lines[currentDialogID].choices[btnID].next;
        isEngaged = false;
        Interact();
    }
}
