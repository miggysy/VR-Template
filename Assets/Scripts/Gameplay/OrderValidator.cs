using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderValidator : MonoBehaviour
{
    //Reference to NPC
    [SerializeField] private CustomerOrder currentCustomer;
    public CustomerOrder CurrentCustomer { get => currentCustomer; set => currentCustomer = value; }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.layer == LayerMask.NameToLayer("Stick"))
        {
            //Validate food
            ValidateFood(obj.GetComponent<FoodManager>());
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Cup"))
        {
            //Validate drink
            ValidateDrink(obj.GetComponent<Cup>());
        }
    }

    private void ValidateFood(FoodManager food)
    {
        currentCustomer.CheckFoodOrder(food);
    }

    private void ValidateDrink(Cup cup)
    {
        currentCustomer.CheckDrinkOrder(cup);
    }

}
