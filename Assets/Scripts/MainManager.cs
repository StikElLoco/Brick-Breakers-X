using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [SerializeField] Rigidbody ball;

    private UiManager uiManager;
    private DataManager dataManager;

    public bool hasStarted = false;
    public bool isGameOver = false;
    public bool isPaused = false;

    public int score;
    public int highscore;
    private float timer = 0.0f;

    public float difficultyMultiplier = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UiManager>();
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();

        if (dataManager != null)
        {
            if (dataManager.difficulty == 1)
            {
                difficultyMultiplier = 0.6f;
            }
            else if (dataManager.difficulty == 3)
            {
                difficultyMultiplier = 2.0f;
            }
            else
            {
                difficultyMultiplier = 1.0f;
            }

            highscore = dataManager.highscore;
        }

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
        if (Input.GetButtonDown("Cancel"))
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

    public void CalculateScore()
    {
        Debug.Log("Score:" + score);
        Debug.Log("Timer:" + timer);

        timer /= 2;
        score -= Mathf.RoundToInt(timer);
        Debug.Log("Final Score: " + score);
        if (score <= 0)
        {
            score = 0;
        }
    }

    public void Restart()
    {
        CalculateScore();
        if (score > highscore)
        {
            dataManager.highscore = score;
            dataManager.Save();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
