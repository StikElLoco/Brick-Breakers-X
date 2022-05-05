using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] private int pointValue;
    [SerializeField] private int brickHp;

    [SerializeField] MainManager mainManager;

    private void Awake()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    private void Start()
    {
        mainManager.maxScore += pointValue;
    }

    private void OnCollisionEnter(Collision other)
    {
        brickHp -= 1;

        if (brickHp <= 0)
        {
            mainManager.PlayAudioClip(2);
            mainManager.score += pointValue;
            Destroy(gameObject, 0.1f);
        }
    }
}
