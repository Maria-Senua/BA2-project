using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "NewDialog",
    menuName = "Dialog/DialogSequence")
]

public class DialogSequence : ScriptableObject
{
    public DialogLine[] lines;

    public DialogSequence next = null;
}
