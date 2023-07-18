using UnityEngine;

public class FoodSmoke : MonoBehaviour
{
    Food food;
    [SerializeField] ParticleSystem smokeEffect;

    private void Awake()
    {
        food = GetComponent<Food>();
    }

    private void StartSmokeEffect()
    {
        smokeEffect.Play();
    }

    private void StopSmokeEffect()
    {
        smokeEffect.Stop();
    }

    private void OnEnable()
    {
        food.onStartCooking += StartSmokeEffect;
        food.onStopCooking += StopSmokeEffect;
    }

    private void OnDisable()
    {
        food.onStartCooking -= StartSmokeEffect;
        food.onStopCooking -= StopSmokeEffect;
    }

}
