using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    [SerializeField] private MainManager mainManager;
    [SerializeField] private UiManager uiManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            mainManager.isGameOver = true;
            uiManager.GameOver();
        }
    }
}
