using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueezeCondiment : MonoBehaviour
{
    [SerializeField] ParticleSystem condimentParticles;
    public void StartParticleSystem()
    {
        condimentParticles.Play();
    }

    public void StopParticleSystem()
    {
        condimentParticles.Stop();
    }

    private void AddFoodCollider(Transform foodTransform)
    {
        condimentParticles.collision.AddPlane(foodTransform);
    }

    private void OnEnable()
    {
        FoodSaucer.onFoodInitialize += AddFoodCollider;
    }

    private void OnDisable()
    {
        FoodSaucer.onFoodInitialize -= AddFoodCollider;
    }
}
