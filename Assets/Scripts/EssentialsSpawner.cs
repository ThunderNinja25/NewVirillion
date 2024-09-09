using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsSpawner : MonoBehaviour
{
    [SerializeField] GameObject essentials;
    [SerializeField] Vector3 spawnPoint;

    private void Awake()
    {
        var existingObjects = FindObjectsOfType<Essentials>();
        if(existingObjects.Length == 0)
        {
            Instantiate(essentials, spawnPoint, Quaternion.identity);
        }
    }
}
