using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gameObjects;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(gameObjects[Random.Range(0, gameObjects.Length - 1)], transform.position, Quaternion.identity);
    }
}
