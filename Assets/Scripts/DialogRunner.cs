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

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
