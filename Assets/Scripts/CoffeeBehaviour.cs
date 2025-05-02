using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeBehaviour : MonoBehaviour
{
    public enum CoffeeState
    {
        Idle,
        Drinking,
        ReturningCup,
        CowApproaching,
        CowReturning,
        PouringMilk,
        PouringCoffee
    }

    private CoffeeState currentState = CoffeeState.Idle;
    private bool actionStarted = false;

    public static CoffeeBehaviour instance;

    public GameObject coffeeLiquid;
    public GameObject drinkMeLabel;
    [SerializeField] public bool isEmpty = true;
    public float interactionDistance = 1.5f;

    private Vector3 originalPosition;
    public float moveSpeed = 1.5f;
    public GameObject bocca;
    public GameObject beansIndicator;
    public int cupsNum = 5;
    public Sprite usedBean;
    public Sprite filledBean;

    public GameObject dialogWindow;
    public GameObject steam;
    public GameObject cow;
    public GameObject milk;
    public Transform cowPos;
    private Vector3 cowOriginalPosition;
    private AudioSource audioSource;


    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        cowOriginalPosition = cow.transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    public void OnMouseDown()
    {
        if (!isEmpty && Vector3.Distance(transform.position, Camera.main.transform.position) < interactionDistance)
        {

            currentState = CoffeeState.Drinking;
            audioSource.Play();
        }

    }

    private void DrinkCoffee()
    {
        drinkMeLabel.SetActive(false);
        transform.position = Vector3.MoveTowards(transform.position, bocca.transform.position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.AngleAxis(-54f, Vector3.right);

        if (!actionStarted)
        {
            actionStarted = true;
            StartCoroutine(EndOfDrinking());
            StartCoroutine(StartReturningCup());
        }
    }

    private IEnumerator EndOfDrinking()
    {
        yield return new WaitForSeconds(1.5f);
        coffeeLiquid.SetActive(false);
        
    }

    private IEnumerator StartReturningCup()
    {
        yield return new WaitForSeconds(2f);
        currentState = CoffeeState.ReturningCup;
        actionStarted = false;
    }

    private void ReturnCup()
    {
        transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.AngleAxis(-82f, Vector3.right);

        if (Vector3.Distance(transform.position, originalPosition) < 0.01f)
        {
            currentState = CoffeeState.Idle;
            isEmpty = true;
            actionStarted = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleState();
        ShowBeansIndicator();
    }
    private void HandleState()
    {
        switch (currentState)
        {
            case CoffeeState.Drinking:
                DrinkCoffee();
                break;

            case CoffeeState.ReturningCup:
                ReturnCup();
                break;

            case CoffeeState.CowApproaching:
                MoveCowTo(cowPos.position, () =>
                {
                    currentState = CoffeeState.PouringMilk;
                    actionStarted = false;
                    Invoke("PourMilk", 0.5f);
                });
                break;

            case CoffeeState.CowReturning:
                MoveCowTo(cowOriginalPosition, () =>
                {
                    cow.SetActive(false);
                    currentState = CoffeeState.Idle;
                    actionStarted = false;
                });
                break;

            case CoffeeState.PouringCoffee:
                if (!actionStarted)
                {
                    actionStarted = true;
                    steam.SetActive(true);
                    Invoke("PourCoffee", 1.5f);
                }
                break;

            default:
                break;
        }
    }

    private void ShowBeansIndicator()
    {
        bool isNear = Vector3.Distance(transform.position, Camera.main.transform.position) < interactionDistance;
        beansIndicator.SetActive(isNear);
    }

    private void MoveCowTo(Vector3 target, System.Action onArrive)
    {
        cow.transform.position = Vector3.MoveTowards(cow.transform.position, target, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(cow.transform.position, target) < 0.01f)
        {
            onArrive?.Invoke();
        }
    }

    public void FinishPuoringCoffee()
    {
        Invoke("StartPouringCoffee", 2f);
    }

    private void StartPouringCoffee()
    {
        if (dialogWindow.activeInHierarchy) dialogWindow.SetActive(false);

        if (cow.activeInHierarchy)
        {
            currentState = CoffeeState.CowReturning;
            milk.SetActive(false);
        }
        else
        {
            currentState = CoffeeState.PouringCoffee;
        }

        actionStarted = false;
    }

    private void PourCoffee()
    {
        coffeeLiquid.SetActive(true);
        drinkMeLabel.SetActive(true);
        isEmpty = false;
        cupsNum--;
        UpdateBeansIndicator();
        currentState = CoffeeState.Idle;
        actionStarted = false;
    }

    private void PourMilk()
    {
        milk.SetActive(true);
        Invoke("StartPouringCoffee", 3f);
    }

    private void StartCappuccinoSequence()
    {
        currentState = CoffeeState.CowApproaching;
        actionStarted = false;
        dialogWindow.SetActive(false);
        cow.SetActive(true);
    }


    public void FinishCappuccino()
    {
        Invoke("StartCappuccinoSequence", 2f);
        Invoke("StartPouringCoffee", 8f);
    }



    private void UpdateBeansIndicator()
    {
        int index = 5 - cupsNum - 1; 

        if (index >= 0 && index < beansIndicator.transform.childCount)
        {
            Transform child = beansIndicator.transform.GetChild(index);
            Image img = child.GetComponent<Image>();
            if (img != null)
            {
                img.sprite = usedBean;
            }
        }
    }

    public void RefillBean()
    {
        if (cupsNum < 5)
        {
            int index = 5 - cupsNum - 1; 
            if (index >= 0 && index < beansIndicator.transform.childCount)
            {
                Transform child = beansIndicator.transform.GetChild(index);
                Image img = child.GetComponent<Image>();
                if (img != null)
                {
                    img.sprite = filledBean;
                }
            }
            cupsNum++;
        }
    }

}
