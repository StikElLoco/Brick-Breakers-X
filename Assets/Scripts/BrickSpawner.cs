using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] brickLines;
    [SerializeField] GameObject[] brickPrefabs;
    [SerializeField] int spawnPattern = 0;

    [SerializeField] List<List<Transform>> spawnLocations = new List<List<Transform>>();

    private DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        spawnPattern = dataManager.pattern;

        for (int i = 0; i < brickLines.Length; i++)
        {
            spawnLocations.Add(GetChildren(brickLines[i].transform));
            SpawnBricks(spawnPattern, i, spawnLocations[i]);
        }
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

    void SpawnBricks(int pattern, int line, List<Transform> locations)
    {
        /*
        //Failsafe, not needed?
        if (pattern > 4 || pattern < 0)
        {
            pattern = 0;
        }
        */

        //Random Pattern
        if (pattern == 0)
        {
            foreach (Transform child in locations)
            {
                Instantiate(brickPrefabs[Random.Range(0, 3)], child);
            }
        }

        //All Green
        if (pattern == 1)
        {
            foreach (Transform child in locations)
            {
                Instantiate(brickPrefabs[0], child);
            }
        }

        //All Red
        if (pattern == 2)
        {
            foreach (Transform child in locations)
            {
                Instantiate(brickPrefabs[1], child);
            }
        }

        //All Blue
        if (pattern == 3)
        {
            foreach (Transform child in locations)
            {
                Instantiate(brickPrefabs[2], child);
            }
        }

        //4 Green, 2 Red, 2 Blue
        if (pattern == 4)
        {
            foreach (Transform child in locations)
            {
                if (line == 0 || line == 1)
                {
                    Instantiate(brickPrefabs[2], child);
                }
                if (line == 2 || line == 3)
                {
                    Instantiate(brickPrefabs[1], child);
                }
                if (line == 4 || line == 5 || line == 6 || line == 7)
                {
                    Instantiate(brickPrefabs[0], child);
                }
            }
        }

        //Alternating Blue/Green
        if (pattern == 5)
        {
            foreach (Transform child in locations)
            {
                if (line == 0 || line == 3 || line == 6)
                {
                    Instantiate(brickPrefabs[2], child);
                }
                else if (line == 1 || line == 2 || line == 4 || line == 5)
                {
                    Instantiate(brickPrefabs[0], child);
                }
                else
                {
                    Instantiate(brickPrefabs[1], child);
                }
            }
        }

    }
}

