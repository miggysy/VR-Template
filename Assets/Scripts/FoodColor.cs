using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodColor : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Food food;
    [SerializeField] Material rawMaterial;
    [SerializeField] Material cookedMaterial;
    [SerializeField] Material burnedMaterial;
    Material currentMaterial;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        food = GetComponent<Food>();
        ResetColor();
    }

    private void SetCookedColor()
    {
        currentMaterial = cookedMaterial;
        meshRenderer.material = cookedMaterial;
    }

    private void SetBurnedColor()
    {
        currentMaterial = burnedMaterial;
        meshRenderer.material = burnedMaterial;
    }

    private void ResetColor()
    {
        currentMaterial = rawMaterial;
        meshRenderer.material = rawMaterial;
    }

    public void ReturnToCurrentMaterial()
    {
        meshRenderer.material = currentMaterial;
    }

    private void OnEnable()
    {
        food.onCooked += SetCookedColor;
        food.onBurned += SetBurnedColor;
    }

    private void OnDisable()
    {
        food.onCooked -= SetCookedColor;
        food.onBurned -= SetBurnedColor;
    }
}
