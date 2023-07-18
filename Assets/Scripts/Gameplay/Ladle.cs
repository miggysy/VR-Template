using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladle : MonoBehaviour
{
    [SerializeField] private bool isFull;
    [SerializeField] private GameObject liquid;
    [Header("Pouring")]
    [SerializeField] private bool isPouring;
    [SerializeField] private float anglePourThreshold;
    private Material drinkMaterial;

    
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Drink"))
        {
            isFull = true;
            drinkMaterial = collider.GetComponent<MeshRenderer>().material;
            liquid.GetComponent<MeshRenderer>().material = drinkMaterial;
            liquid.SetActive(true);
        }
    }

    private void Update()
    {
        if(isPouring || !isFull) return;
        if((transform.eulerAngles.x > anglePourThreshold && transform.eulerAngles.x < 360 - anglePourThreshold) || (transform.eulerAngles.z > anglePourThreshold && transform.eulerAngles.z < 360 - anglePourThreshold))
        {
            Debug.Log("Pouring. " + transform.eulerAngles);
            Pour();
        }
    }

    private void Pour()
    {
        isPouring = true;
        liquid.SetActive(false);
        isFull = false;
        isPouring = false;
    }
}
