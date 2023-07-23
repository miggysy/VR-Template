using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class ObjectPile : MonoBehaviour
{
    [SerializeField] private GameObject pileObject;
    [SerializeField] private Transform spawnedObjectParent;
    private GameObject instance;
    private XRSimpleInteractable interactable;
    private IXRHoverInteractor controller;
    private bool followController;

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.hoverEntered.AddListener(SpawnInstance);
    }

    private void Update()
    {
        if(followController && instance != null && controller != null)
        {
            instance.transform.position = controller.transform.position;
        }
    }

    public void SpawnInstance(HoverEnterEventArgs args)
    {
        followController = true;
        instance = Instantiate(pileObject, transform.position, pileObject.transform.rotation, spawnedObjectParent); 
        controller = args.interactorObject;
        instance.GetComponent<XRGrabInteractable>().selectEntered.AddListener(DetachInstance);
    }

    public void DestroyInstance()
    {
        if(instance == null) return;
        followController = false;
        Destroy(instance);
    }

    public void DetachInstance(SelectEnterEventArgs args)
    {
        followController = false;
        instance = null;
    }
}
