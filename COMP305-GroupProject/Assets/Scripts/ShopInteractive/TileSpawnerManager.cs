using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileSpawnerManager : MonoBehaviour
{
    List<TileSpawner> spawners = new List<TileSpawner>();

    // Start is called before the first frame update
    void Start()
    {
        spawners = transform.GetComponentsInChildren<TileSpawner>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        spawners.ForEach(t => t.Reset());
    }
}
