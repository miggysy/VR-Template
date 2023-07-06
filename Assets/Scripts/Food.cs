using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Food : MonoBehaviour
{
    [SerializeField] private float currentFoodTimer;
    private bool cooked;
    private bool burned;
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

        currentFoodTimer += Time.deltaTime;

        if(!cooked)
        {
            if(currentFoodTimer >= cookTime)
            {
                cooked = true;
                currentFoodTimer = 0;
                onCooked?.Invoke();
             }
        }
        else if(!burned)
        {
            if(currentFoodTimer >= burnTime)
            {
                burned = true;
                onBurned?.Invoke();
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
