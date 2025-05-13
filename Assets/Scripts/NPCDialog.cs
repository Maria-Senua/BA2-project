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
    public Sprite portraitCalm, portraitSad, portraitAngry;
    public DialogSequence npcDialog;
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

        DialogLine currentLine = npcDialog.lines[currentDialogID];
        dialogRunner.SetDialog(currentLine.text, GetPortraitByEmotion(currentLine.emotion));
        if (currentLine.choices != null)
        {
            dialogRunner.ShowChoices(currentLine.choices);
        } else
        {
            dialogRunner.HideChoices();
        }
    }

    Sprite GetPortraitByEmotion(DialogEmotionalState emotionalState)
    {
        switch (emotionalState)
        {
            case DialogEmotionalState.SAD:
                return portraitSad;
            case DialogEmotionalState.ANGRY:
                return portraitAngry;
            default:
                return portraitCalm;
        }
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
        npcDialog = newDialogSequence;
        portrait = newPortrait;
    }
}
