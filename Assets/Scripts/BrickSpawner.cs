using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] brickLines;
    [SerializeField] GameObject[] brickPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < brickLines.Length; i++)
        {
            List<Transform> spawnsLocations = GetChildren(brickLines[i].transform);

            foreach(Transform child in spawnsLocations)
            {
                Debug.Log(child.position);
                Instantiate(brickPrefabs[Random.Range(0, 3)], child);
            }
        }

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

}
