using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DrinkType
{
    NullType,
    Gulaman,
    BukoJuice
}

public class Cup : MonoBehaviour
{
    [SerializeField] private DrinkType type;
    public DrinkType Type { get => type; }
    [SerializeField] private GameObject liquid;
    [SerializeField] private float fillSpeed;
    private float fullAmount;
    private float fillAmount;
    private bool isFull;
    public bool IsFull { get => isFull; }
    private Material material;

    void Start()
    {
        fullAmount = liquid.transform.localScale.z;
        fillAmount = 0;
        SetLiquidLevel();
    }

    private void OnParticleCollision(GameObject collider)
    {
        if(collider.layer == LayerMask.NameToLayer("Drink"))
        {
            material = collider.GetComponent<ParticleSystemRenderer>().material;
            SetDrinkType(material.name.Split(' ')[0]);
            StartCoroutine(FillCup());
        }
    }

    private IEnumerator FillCup()
    {
        fillAmount += (Time.deltaTime * fillSpeed) / fullAmount;

        if(fillAmount >= fullAmount) 
        {
            fillAmount = fullAmount;
            isFull = true;
            yield return new WaitForEndOfFrame();
            StopAllCoroutines();
        }
        else
        {
            SetLiquidLevel();
            yield return new WaitForEndOfFrame();
            StartCoroutine(FillCup());
        }
    }

    private void SetLiquidLevel()
    {
        if(material != null) liquid.transform.GetChild(0).GetComponent<MeshRenderer>().material = material;
        liquid.transform.localScale = new Vector3(liquid.transform.localScale.x, liquid.transform.localScale.y, fillAmount);
    }

    private void SetDrinkType(string materialName)
    {
        switch(materialName)
        {
            case "BukoJuice": 
                type = DrinkType.BukoJuice;
                break;
            case "Gulaman":
                type = DrinkType.Gulaman;
                break;
            default:
                type = DrinkType.NullType;
                break;
        }
    }
}
