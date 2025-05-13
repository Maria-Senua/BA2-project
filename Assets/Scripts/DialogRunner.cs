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

    }

    public void ShowChoices(DialogChoice[] choices)
    {
        btnChoice0.gameObject.SetActive(true);
        btnChoice0.GetComponentInChildren<TMP_Text>().text = choices[0].buttonText;

        btnChoice1.gameObject.SetActive(true);
        btnChoice1.GetComponentInChildren<TMP_Text>().text = choices[1].buttonText;
    }

    public void HideChoices()
    {
        btnChoice0.gameObject.SetActive(false);
        btnChoice1.gameObject.SetActive(false);
    }
}
