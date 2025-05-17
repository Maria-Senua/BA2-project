using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogLine
{
    public string text;
    public DialogEmotionalState emotion;

    public DialogChoice[] choices = null;
}

[System.Serializable]
public class DialogChoice
{
    public string buttonText;
    public DialogSequence next;
}

public enum DialogEmotionalState
{
    ANGRY,
    FURY,
    MISTAKE,
    MOCK,
    NORMAL,
    PINK,
    RIGHT,
    SAD,
    SHOCK
}