using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum FoodState 
{
    Uncooked,
    Cooked,
    Burned,
}

public class Food : MonoBehaviour
{
    [SerializeField] private FoodType type;
    public FoodType Type { get => type; }
    [SerializeField] private SauceType typeSauce;
    public SauceType TypeSauce { get => typeSauce; set => typeSauce = value; }
    [SerializeField] private float currentFoodTimer;
    [SerializeField] private FoodState foodState = FoodState.Uncooked;
    public FoodState FoodState { get => foodState; set => foodState = value; }
    //Number of seconds from raw to cooked
    [SerializeField] private float cookTime;

    //Number of seconds from cooked to burnt
    [SerializeField] private float burnTime;
    
    //Event for when the food starts cooking
    public event Action onStartCooking;

    //Event for when the food stops cooking
    public event Action onStopCooking;

    public event Action onCooked;
    public event Action onBurned;
    public delegate void OnSauced();
    public OnSauced onSauced;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer.Equals(LayerMask.NameToLayer("Grill")))
        {
            onStartCooking?.Invoke();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.layer.Equals(LayerMask.NameToLayer("Grill")))
        {
            onStopCooking?.Invoke();
        }
    }

    private void StartCooking()
    {
        StartCoroutine(CookCoroutine());
    }

    private void StopCooking()
    {
        currentFoodTimer = 0;   //reset the food cook timer
        StopAllCoroutines();
    }

    private IEnumerator CookCoroutine()
    {
        yield return new WaitForEndOfFrame();

        if(!gameManager.IsPaused) 
        {
            currentFoodTimer += Time.deltaTime;

            if(foodState == FoodState.Uncooked)
            {
                if(currentFoodTimer >= cookTime)
                {
                    foodState = FoodState.Cooked;
                    currentFoodTimer = 0;
                    onCooked?.Invoke();
                }
            }
            else if(foodState == FoodState.Cooked)
            {
                if(currentFoodTimer >= burnTime)
                {
                    foodState = FoodState.Burned;
                    onBurned?.Invoke();
                }
            }
        }

        StartCoroutine(CookCoroutine());
    }

    private void OnEnable()
    {
        onStartCooking += StartCooking;
        onStopCooking += StopCooking;
    }

    private void OnDisable()
    {
        onStartCooking -= StartCooking;
        onStopCooking -= StopCooking;
    }

}
