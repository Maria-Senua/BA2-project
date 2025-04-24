using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeBehaviour : MonoBehaviour
{

    public GameObject coffeeLiquid;
    public GameObject drinkMeLabel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishPuoringCoffee()
    {
        Invoke("PourCoffee", 1.5f);
    }

    private void PourCoffee()
    {
        coffeeLiquid.SetActive(true);
        drinkMeLabel.SetActive(true);
    }

}
