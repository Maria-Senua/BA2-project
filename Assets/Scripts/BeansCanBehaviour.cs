using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeansCanBehaviour : MonoBehaviour
{

    public float interactionDistance = 1.5f;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) < interactionDistance)
        {

            animator.SetTrigger("FillBeans");
        }

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Machine"))
        {
            Debug.Log("start refilleng");
            StartCoroutine(RefillBeans());
        }
    }

    private IEnumerator RefillBeans()
    {
        while (CoffeeBehaviour.instance.cupsNum < 5)
        {
            CoffeeBehaviour.instance.RefillBean();
            yield return new WaitForSeconds(0.1f);  
        }
       
    }
}
