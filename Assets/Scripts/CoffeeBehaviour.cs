using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using TMPro;
using UnityEngine;

public class CoffeeBehaviour : MonoBehaviour
{

    public GameObject coffeeLiquid;
    public GameObject drinkMeLabel;
    [SerializeField] public bool isEmpty = true;
    public float interactionDistance = 1.5f;
    private bool isDrinking = false;

    private Vector3 originalPosition;
    public float moveSpeed = 1.5f;
    public GameObject bocca;

    public void OnMouseDown()
    {
        if (!isEmpty && Vector3.Distance(transform.position, Camera.main.transform.position) < interactionDistance)
        {
            
            isDrinking = true;
        }

    }

    private void DrinkCoffee()
    {
        drinkMeLabel.SetActive(false);
        transform.position = Vector3.MoveTowards(transform.position, bocca.transform.position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.AngleAxis(-54f, Vector3.right);
        StartCoroutine(EndOfDrinking());
        StartCoroutine(PutCupBack());
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    private IEnumerator EndOfDrinking()
    {
        yield return new WaitForSeconds(1.5f);
        coffeeLiquid.SetActive(false);
        
    }

    private IEnumerator PutCupBack()
    {
        yield return new WaitForSeconds(2f);
        transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime); ;
        transform.rotation = Quaternion.AngleAxis(-82f, Vector3.right);
        isDrinking = false;
        isEmpty = true;
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
