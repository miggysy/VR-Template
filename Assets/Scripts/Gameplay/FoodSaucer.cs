using UnityEngine;
using System;

public enum SauceType
{
    SweetAndSpicy,
    Vinegar,
    Unsauced
}

public class FoodSaucer : MonoBehaviour
{
    private Food food;
    [SerializeField] private ParticleSystem sauceEffect;
    public static event Action<Transform> onFoodInitialize;
    private bool isSauced;
    void Awake()
    {
        food = GetComponent<Food>();
    }
    void Start()
    {
        onFoodInitialize?.Invoke(GetComponent<Transform>());
    }

    public void SauceFood()
    {
        if(isSauced) return;
        food.onSauced?.Invoke();
    }

    private void PlaySauceEffect()
    {
        sauceEffect.gameObject.SetActive(true);
        sauceEffect.Play();
    }

    private void StopSauceEffect()
    {
        isSauced = false;
        sauceEffect.Stop();
        sauceEffect.gameObject.SetActive(false);
    }

    private void OnParticleCollision(GameObject collider)
    {
        if(collider.layer == LayerMask.NameToLayer("Sauce"))
        {
            sauceEffect.gameObject.GetComponent<ParticleSystemRenderer>().material = collider.GetComponent<ParticleSystemRenderer>().material;
            food.TypeSauce = GetSauceTypeFromMaterial(sauceEffect.gameObject.GetComponent<ParticleSystemRenderer>().material.name.Split(' ')[0]);
            SauceFood();
            isSauced = true;
        }
    }

    private void OnEnable()
    {
        food.onSauced += PlaySauceEffect;
        food.onCooked += StopSauceEffect;
        food.onBurned += StopSauceEffect;
    }

    private void OnDisable()
    {
        food.onSauced -= PlaySauceEffect;
        food.onCooked -= StopSauceEffect;
        food.onBurned -= StopSauceEffect;
    }

    private SauceType GetSauceTypeFromMaterial(string materialName)
    {
        switch(materialName)
        {
            case "SweetAndSpicy": return SauceType.SweetAndSpicy;
            case "Vinegar": return SauceType.Vinegar;
            default: return SauceType.Unsauced;
        }
    }
}
