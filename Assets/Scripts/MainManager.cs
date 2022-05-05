using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainManager : MonoBehaviour
{
    [SerializeField] Rigidbody ball;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] AudioClip[] audioClips; //0-jump, 1-hit, 2-Explosion, 3-Select

    private AudioSource audioSource;
    private UiManager uiManager;
    private DataManager dataManager;

    public bool hasStarted = false;
    public bool isGameOver = false;
    public bool isPaused = false;

    public int score, maxScore;
    public int highscore;
    private float timer = 0.0f;

    public float difficultyMultiplier = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UiManager>();
        audioSource = GetComponent<AudioSource>();

        if (dataManager != null)
        {
            dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
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

                Debug.Log("Maxscore: " + maxScore);

                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                ball.AddForce(forceDir * 5.0f, ForceMode.VelocityChange);

                PlayAudioClip(0);
            }
        }
        else if (isGameOver)
        {
            if (Input.GetButtonDown("Start"))
            {
                PlayAudioClip(3);
                Restart();
            }
        }

        //Call Pause menu
        if (Input.GetButtonDown("Cancel"))
        {
            if (!isPaused && !isGameOver)
            {
                PlayAudioClip(3);
                uiManager.Pause();
            }
            else if (isPaused)
            {
                PlayAudioClip(3);
                uiManager.UnPause();
            }
        }
    }

    private void LateUpdate()
    {
        //Check if all blocks destroyed, maxScore is sent by each BrickController on Start(), this way it will change/work for additional levels
        if (score >= maxScore)
        {
            uiManager.GameOver();
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
        scoreText.text = "Score: " + score;
    }

    public void Restart()
    {
        if (score > highscore)
        {
            dataManager.highscore = score;
            dataManager.Save();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayAudioClip(int selection)
    {
        audioSource.PlayOneShot(audioClips[selection], 1.0f);
    }
}
