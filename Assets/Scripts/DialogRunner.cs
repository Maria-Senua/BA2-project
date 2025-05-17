using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogRunner : MonoBehaviour
{

    public GameObject dialogWindow;
    public TMP_Text dialogText;
    public Image imageContainer;
    public Button btnChoice0;
    public Button btnChoice1;
    private NPCDialog choiceListener;
    [HideInInspector] public bool withChoices;

    public void Show()
    {
        dialogWindow.SetActive(true);
    }

    public void Hide()
    {
        dialogWindow.SetActive(false);
    }

    public void SetDialog(string text, Sprite portrait)
    {
        dialogText.text = text;
        imageContainer.sprite = portrait;
    }

    public void OnChoiceButton(int btnID)
    {
        if (choiceListener != null)
        {
            choiceListener.OnChoiceButton(btnID);
        }
    }

    public void ShowChoices(DialogChoice[] choices, NPCDialog listener)
    {
        Debug.Log("choices " + choices.Length);
        btnChoice0.gameObject.SetActive(true);
        btnChoice0.GetComponentInChildren<TMP_Text>().text = choices[0].buttonText;

        btnChoice1.gameObject.SetActive(true);
        btnChoice1.GetComponentInChildren<TMP_Text>().text = choices[1].buttonText;

        choiceListener = listener;
        withChoices = true;
    }

    public void HideChoices()
    {
        Debug.Log("hiding choices");
        btnChoice0.gameObject.SetActive(false);
        btnChoice1.gameObject.SetActive(false);

        choiceListener = null;
        withChoices = false;
    }
}
