using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody ball;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] AudioClip[] audioClips; //0-jump, 1-hit, 2-Explosion, 3-Select

    [HideInInspector] public bool hasStarted = false;
    [HideInInspector] public bool isGameOver = false;
    [HideInInspector] public bool isPaused = false;

    [HideInInspector] public int score, maxScore;
    [HideInInspector] public int highscore;
    [HideInInspector] public float difficultyMultiplier = 1.0f;

    private AudioSource audioSource;
    private UiManager uiManager;
    private DataManager dataManager;

    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Object references
        uiManager = GetComponent<UiManager>();
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        audioSource = GetComponent<AudioSource>();

        //Check if DataManager exists, should always exist when loaded from the main menu
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
            audioSource.mute = dataManager.isMuted;
        }
        //Set time scale to 1, when using the Restart button as GameOver sets it to 0
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
                //generate random direction for the ball to start moving
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();
                //Add force to the ball
                ball.AddForce(forceDir * 5.0f, ForceMode.VelocityChange);
                //Play the jumping sound
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

    //Score is calculated by halving the timer then subtracting that from the score, so high score is determined by how fast you can break all the bricks, highscore is more likely the higher the difficulty because the ball moves faster
    public void CalculateScore()
    {
        timer /= 2;
        score -= Mathf.RoundToInt(timer);
        if (score <= 0)
        {
            score = 0;
        }
        scoreText.text = "Score: " + score;
        if (score > highscore)
        {
            dataManager.highscore = score;
            dataManager.Save();
        }
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

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAudioClip(int selection)
    {
        audioSource.PlayOneShot(audioClips[selection], 1.0f);
    }
}
