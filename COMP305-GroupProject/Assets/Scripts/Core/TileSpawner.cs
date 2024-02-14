using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gameObjects;

    GameObject tile;
    // Start is called before the first frame update
    void Start()
    {
        tile = Instantiate(gameObjects[Random.Range(0, gameObjects.Length - 1)], transform.position, Quaternion.identity);
        tile.transform.SetParent(this.transform);
    }

    public void Reset()
    {
        Destroy(tile);
        tile = Instantiate(gameObjects[Random.Range(0, gameObjects.Length - 1)], transform.position, Quaternion.identity);
        tile.transform.SetParent(this.transform);
    }
}
