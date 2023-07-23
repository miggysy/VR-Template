using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float maxTime = 30f;
    [SerializeField] private float minTime = 15f;
    [SerializeField] private float angerThreshold = 10f;
    [SerializeField] private Material angryMaterial;
    private bool countDown;
    private bool isAngry;
    private GameManager gameManager;

    [SerializeField] private float currentTime;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if(gameManager.IsPaused || !countDown) return;
        currentTime -= Time.deltaTime;

        if(currentTime <= angerThreshold && !isAngry)
        {
            isAngry = true;

            //Set angry effect
            transform.GetChild(0).GetComponent<MeshRenderer>().material = angryMaterial;
        }

        if (currentTime <= 0f)
        {
            // Timer has reached zero, perform your desired action here
            Debug.Log("Time's up!");
            countDown = false;
            GameManager.onCustomerLeft?.Invoke();
        }
    }

    public void ResetTimer()
    {
        currentTime = Random.Range(minTime, maxTime);
        countDown = true;
    }
}