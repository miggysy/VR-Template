using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public enum FoodType
{
    NullType,
    BBQ,
    Isaw,
    Betamax,
    Liver

}
public class FoodManager : MonoBehaviour
{
    [SerializeField] private FoodType foodType = FoodType.NullType;
    public FoodType Type { get => foodType; }
    [SerializeField] private SauceType typeSauce = SauceType.Unsauced; 
    public SauceType TypeSauce { get => typeSauce; }
    [SerializeField] private bool isCooked;
    public bool IsCooked { get => isCooked; }
    [SerializeField] private List<XRSocketInteractor> foodSockets = new List<XRSocketInteractor>();
    [SerializeField] private List<GameObject> foodObjects = new List<GameObject>();
    public void SetFoodObjects()
    {
        foodObjects.Clear();
        foreach(XRSocketInteractor socket in foodSockets)
        {
            IXRSelectInteractable obj = socket.GetOldestInteractableSelected();
            if(obj != null)
                foodObjects.Add(obj.transform.gameObject);
        }
        CheckFood();
    }

    private void CheckFood()
    {
        if(foodObjects.Count == foodSockets.Count)
        {
            if(foodObjects[0].GetComponent<Food>().Type == foodObjects[1].GetComponent<Food>().Type && foodObjects[1].GetComponent<Food>().Type == foodObjects[2].GetComponent<Food>().Type)
            {
                foodType = foodObjects[0].GetComponent<Food>().Type;
            }
            else foodType = FoodType.NullType;

            if(foodObjects[0].GetComponent<Food>().FoodState == FoodState.Cooked && foodObjects[1].GetComponent<Food>().FoodState == FoodState.Cooked && foodObjects[2].GetComponent<Food>().FoodState == FoodState.Cooked)
            {
                isCooked = true;
            }
            else isCooked = false;

            if(foodObjects[0].GetComponent<Food>().TypeSauce == foodObjects[1].GetComponent<Food>().TypeSauce && foodObjects[1].GetComponent<Food>().TypeSauce == foodObjects[2].GetComponent<Food>().TypeSauce)
            {
                typeSauce = foodObjects[0].GetComponent<Food>().TypeSauce;
            }
            else typeSauce = SauceType.Unsauced;
        }
    }

    public void DestroyFood()
    {
        int count = foodObjects.Count;
        for(int i = 0; i < count; i++)
        {
            Destroy(foodObjects[0]);
        }
    }
}
