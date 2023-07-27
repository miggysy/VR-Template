using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladle : MonoBehaviour
{
    [SerializeField] private bool hasLiquid;
    [SerializeField] private GameObject liquid;
    [SerializeField] private ParticleSystem pourParticles;
    [SerializeField] private float maxFillAmount;
    [SerializeField] private float fillAmount;

    [Header("Pouring")]
    [SerializeField] private bool isPouring;
    public bool IsPouring { get => isPouring; }
    [SerializeField] private float anglePourThreshold;
    [SerializeField] private float pourSpeed;
    private Material drinkMaterial;
    private ParticleSystem.EmissionModule em;

    private void Start()
    {
        em = pourParticles.emission;
    }

    
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Drink"))
        {
            fillAmount = maxFillAmount;
            hasLiquid = true;
            drinkMaterial = collider.GetComponent<MeshRenderer>().material;
            pourParticles.GetComponent<ParticleSystemRenderer>().material = drinkMaterial;
            liquid.GetComponent<MeshRenderer>().material = drinkMaterial;
            liquid.SetActive(true);
        }
    }

    private void Update()
    {
        if(!hasLiquid)
        {
            isPouring = false;
            em.enabled = false;
            return;
        }

        if((transform.eulerAngles.x > anglePourThreshold && transform.eulerAngles.x < 360 - anglePourThreshold) || (transform.eulerAngles.z > anglePourThreshold && transform.eulerAngles.z < 360 - anglePourThreshold))
        {
            Pour();
        }
        else
        {
            isPouring = false;
            em.enabled = false;
        }
    }

    private void Pour()
    {
        isPouring = true;
        fillAmount -= Time.deltaTime * pourSpeed;
        em.enabled = true;

        if(fillAmount <= 0)
        {
            em.enabled = false;
            fillAmount = 0;
            liquid.SetActive(false);
            hasLiquid = false;
        }
    }
}
