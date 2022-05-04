using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [SerializeField] Rigidbody ball;

    private UiManager uiManager;

    public bool hasStarted = false;
    public bool isGameOver = false;
    public bool isPaused = false;

    public int score;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UiManager>();
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //if the game has started and it's not paused add to the timer
        if (hasStarted && !isPaused)
        {
            timer += Time.deltaTime;
        }

        //If the game hasn't started look for the Start input and add force to the ball when it's pressed
        if (!hasStarted)
        {
            if (Input.GetButtonDown("Start"))
            {
                hasStarted = true;

                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                ball.AddForce(forceDir * 5.0f, ForceMode.VelocityChange);
            }
        }
        else if (isGameOver)
        {
            if (Input.GetButtonDown("Start"))
            {
                Restart();
            }
        }

        //Call Pause menu
        if (Input.GetButtonDown("Pause"))
        {
            if (!isPaused && !isGameOver)
            {
                uiManager.Pause();
            }
            else if (isPaused)
            {
                uiManager.UnPause();
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
