using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueingSystem : MonoBehaviour
{
    [SerializeField] private int numCustomers;
    [SerializeField] private List<CustomerOrder> customers = new List<CustomerOrder>();
    [SerializeField] private List<GameObject> customerPrefabs = new List<GameObject>();
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

        int randomIndex = Random.Range(0, customerPrefabs.Count);
        GameObject instance = Instantiate(customerPrefabs[randomIndex], newSpawnPos, customerPrefabs[randomIndex].transform.rotation, transform);

        customers.Add(instance.GetComponent<CustomerOrder>());
        
        if(customers.Count == 1) SetCurrentCustomer();
    }

    private void SetCurrentCustomer()
    {
        orderValidator.CurrentCustomer = customers[0];
        //start timer

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            RemoveCurrentCustomer();
        }
    }

    private void RemoveCurrentCustomer()
    {
        Destroy(customers[0].gameObject);
        customers.RemoveAt(0);
        if(customers.Count > 0) 
        {
            SetCurrentCustomer();
            //shift all the customers
            foreach(CustomerOrder customer in customers)
            {
                customer.gameObject.transform.position -= queueOffset;
            }
        }
        SpawnCustomer();
    }

    private void OnEnable()
    {
        GameManager.onSubmittedOrder += RemoveCurrentCustomer;
        GameManager.onCustomerLeft += RemoveCurrentCustomer;
    }

    private void OnDisable()
    {
        GameManager.onSubmittedOrder -= RemoveCurrentCustomer;
        GameManager.onCustomerLeft -= RemoveCurrentCustomer;
    }
}
