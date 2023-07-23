using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FellToFloor : MonoBehaviour
{
    [SerializeField] private float groundLevel = -1.1176f;

    [Header("Respawn")]
    [SerializeField] private bool respawn;
    [SerializeField] private Transform spawnPointTransform;
    [SerializeField] private Vector3 spawnPointCoordinates;
    [SerializeField] private Vector3 spawnRotation;

    private void Update()
    {
        if(transform.position.y <= groundLevel)
        {
            if(respawn)
            {
                transform.position = (spawnPointTransform != null) ? spawnPointTransform.position : spawnPointCoordinates;
                transform.rotation = Quaternion.Euler(spawnRotation);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
