using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] brickLines;
    [SerializeField] GameObject[] brickPrefabs;
    [SerializeField] int spawnPattern = 0;

    public List<Transform> spawnLocations;
    //What the fuck https://forum.unity.com/threads/list-or-array-of-lists.513443/
    public List<List<int>> numbers = new List<List<int>>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < brickLines.Length; i++)
        {
            spawnLocations = GetChildren(brickLines[i].transform);
        }
        //TO DO: Make list of lists or array of lists?
        SpawnBricks(spawnPattern, spawnLocations);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<Transform> GetChildren(Transform parent)
    {
        List<Transform> children = new List<Transform>();

        foreach(Transform child in parent)
        {
            children.Add(child);
        }

        return children;
    }

    void SpawnBricks(int pattern, List<Transform> locations)
    {
        if (pattern == 0)
        {
            foreach (Transform child in locations)
            {
                Instantiate(brickPrefabs[Random.Range(0, 3)], child);
            }
        }

        if (pattern == 1)
        {
            foreach (Transform child in locations)
            {
                Instantiate(brickPrefabs[0], child);
            }
        }

    }

}

