using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using TMPro;
using UnityEngine;

public class CoffeeBehaviour : MonoBehaviour
{

    public GameObject coffeeLiquid;
    public GameObject drinkMeLabel;
    private bool isEmpty = true;
    public float interactionDistance = 1.5f;
    private bool isDrinking = false;

    private Vector3 originalPosition;
    public float moveSpeed = 1.5f;
    public GameObject bocca;

    public void OnMouseDown()
    {
        if (!isEmpty && Vector3.Distance(transform.position, Camera.main.transform.position) < interactionDistance)
        {
            
            isDrinking = true; //rotation x -54
        }

    }

    private void DrinkCoffee()
    {
        transform.position = Vector3.MoveTowards(transform.position, bocca.transform.position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.AngleAxis(-54f, Vector3.right);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrinking) DrinkCoffee();
    }

    public void FinishPuoringCoffee()
    {
        Invoke("PourCoffee", 1.5f);
    }

    private void PourCoffee()
    {
        coffeeLiquid.SetActive(true);
        drinkMeLabel.SetActive(true);
        isEmpty = false;
    }

}
