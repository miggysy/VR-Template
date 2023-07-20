using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueingSystem : MonoBehaviour
{
    [SerializeField] private int numCustomers;
    [SerializeField] private List<CustomerOrder> customers = new List<CustomerOrder>();
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Vector3 queueOffset;
    [SerializeField][Range(0f, 1f)] private float foodOrderChance;
    public float FoodOrderChance { get => foodOrderChance; }
    [SerializeField][Range(0f, 1f)] private float drinkOrderChance;
    public float DrinkOrderChance { get => drinkOrderChance; }

    private OrderValidator orderValidator;
    
    private void Awake()
    {
        orderValidator = FindObjectOfType<OrderValidator>();
    }

    private void Start()
    {
        for(int i = 0; i < numCustomers; i++)
        {
            SpawnCustomer();
        }
    }

    private void SpawnCustomer()
    {
        Vector3 newSpawnPos = spawnPosition.position;

        if(customers.Count > 0)
        {
            newSpawnPos = customers[customers.Count-1].gameObject.transform.position + queueOffset;
        }

        GameObject instance = Instantiate(customerPrefab, newSpawnPos, customerPrefab.transform.rotation, transform);

        customers.Add(instance.GetComponent<CustomerOrder>());
        
        if(customers.Count == 1) SetCurrentCustomer();
    }

    private void SetCurrentCustomer()
    {
        orderValidator.CurrentCustomer = customers[0];
        //start timer

    }
}
