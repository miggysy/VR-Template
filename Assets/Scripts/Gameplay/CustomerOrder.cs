using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerOrder : MonoBehaviour
{
    [SerializeField] List<FoodType> foodOrders = new List<FoodType>();
    [SerializeField] List<SauceType> foodSauce = new List<SauceType>();
    [SerializeField] List<DrinkType> drinkOrders = new List<DrinkType>();
    [Header("Customer Order UI")]
    [SerializeField] GameObject orderUI;
    [SerializeField] TextMeshProUGUI orderText;
    List<FoodManager> completedFoodOrders = new List<FoodManager>();
    List<Cup> completedDrinkOrders = new List<Cup>();
    private QueueingSystem queueingSystem;

    private void Awake()
    {
        queueingSystem = FindObjectOfType<QueueingSystem>();
    }

    private void OnEnable()
    {
        //set instance's order
        if(Random.Range(0f, 1f) <= queueingSystem.FoodOrderChance)
        {
            //set food
            foodOrders.Add(GenerateRandomFoodOrder());
            foodSauce.Add(GenerateRandomSauce());
        }
        if(Random.Range(0f, 1f) <= queueingSystem.DrinkOrderChance || foodOrders.Count == 0)
        {
            //set drink
            drinkOrders.Add(GenerateRandomDrinkOrder());
        } 

        if(foodOrders.Count > 0) orderText.text += GetFoodString(foodOrders[0]);
        if(foodSauce.Count > 0) orderText.text += " " + GetSauceString(foodSauce[0]);
        if(drinkOrders.Count > 0) orderText.text += (orderText.text.Length > 0 ? " and " : "") + GetDrinkString(drinkOrders[0]);

        orderText.text += ".";

        orderUI.SetActive(false);
    }

    public void EnableUI()
    {
        orderUI.SetActive(true);
    }

    public void CheckFoodOrder(FoodManager foodOrder)
    {
        if(foodOrder.IsCooked && foodOrders.Contains(foodOrder.Type) && foodSauce[foodOrders.IndexOf(foodOrder.Type)] == foodOrder.TypeSauce)
        {
            foodOrders.Remove(foodOrder.Type);
            foodSauce.Remove(foodOrder.TypeSauce);
            completedFoodOrders.Add(foodOrder);
            CheckIfOrderIsComplete();
        }
        else
        {
            //Say that the food order is incorrect
            
        }
    }

    public void CheckDrinkOrder(Cup drinkOrder)
    {
        if(drinkOrders.Contains(drinkOrder.Type))
        {
            drinkOrders.Remove(drinkOrder.Type);
            completedDrinkOrders.Add(drinkOrder);
            CheckIfOrderIsComplete();
        }
        else
        {
            //Say that the drink order is incorrect
        }
    }

    private void CheckIfOrderIsComplete()
    {
        if(foodOrders.Count == 0 && foodSauce.Count == 0 && drinkOrders.Count == 0)
        {
            int count = completedFoodOrders.Count;
            //Destroy the game objects on the table
            for(int i = 0; i < count; i++)
            {
                completedFoodOrders[0].DestroyFood();
                Destroy(completedFoodOrders[0].gameObject);
            }
            count = completedDrinkOrders.Count;
            for(int i = 0; i < completedDrinkOrders.Count; i++)
            {
                Destroy(completedDrinkOrders[0].gameObject);
            }

            //Tell the customer that their order is complete
            GameManager.onSubmittedOrder?.Invoke();
        }
    }

    private FoodType GenerateRandomFoodOrder()
    {
        switch(Random.Range(0,4))
        {
            case 0: return FoodType.BBQ;
            case 1: return FoodType.Betamax;
            case 2: return FoodType.Isaw;
            case 3: return FoodType.Liver;
            default: return FoodType.NullType;
        }
    }

    private SauceType GenerateRandomSauce()
    {
        switch(Random.Range(0,3))
        {
            case 0: return SauceType.SweetAndSpicy;
            case 1: return SauceType.Vinegar;
            default: return SauceType.Unsauced;
        }
    }

    private DrinkType GenerateRandomDrinkOrder()
    {
        switch(Random.Range(0,2))
        {
            case 0: return DrinkType.BukoJuice;
            case 1: return DrinkType.Gulaman;
            default: return DrinkType.NullType;
        }
    }

    private string GetFoodString(FoodType type)
    {
        switch(type)
        {
            case FoodType.BBQ: return "BBQ";
            case FoodType.Isaw: return "Isaw";
            case FoodType.Betamax: return "Betamax";
            case FoodType.Liver: return "Liver";
            default: return "";
        }
    }

    private string GetSauceString(SauceType type)
    {
        switch(type)
        {
            case SauceType.SweetAndSpicy: return "with Sweet and Spicy sauce";
            case SauceType.Vinegar: return "with Vinegar";
            case SauceType.Unsauced: return "no sauce";
            default: return "";
        }
    }

    private string GetDrinkString(DrinkType type)
    {
        switch(type)
        {
            case DrinkType.Gulaman: return "Gulaman";
            case DrinkType.BukoJuice: return "Buko Juice";
            default: return "";
        }
    }
}
