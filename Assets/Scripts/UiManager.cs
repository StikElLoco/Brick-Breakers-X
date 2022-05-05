using UnityEngine;
using UnityEngine.SceneManagement;


public class UiManager : MonoBehaviour
{
    private MainManager mainManager;

    [SerializeField] GameObject pauseMenu, confirmationMenu, gameOverMenu;

    private void Start()
    {
        mainManager = GetComponent<MainManager>();
    }

    //Functions mostly called by buttons, they show/hide the correct menu

    //Uses time Scale to effectively pause the game
    public void Pause()
    {
        mainManager.isPaused = true;
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);
        confirmationMenu.SetActive(false);
    }

    public void UnPause()
    {
        mainManager.isPaused = false;
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        confirmationMenu.SetActive(false);
    }

    public void MenuConfirmation()
    {
        if (!mainManager.isGameOver)
        {
            pauseMenu.SetActive(false);
            confirmationMenu.SetActive(true);
        }
        else if (mainManager.isGameOver)
        {
            gameOverMenu.SetActive(false);
            confirmationMenu.SetActive(true);
        }
    }

    public void ConfirmationYes()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ConfirmationNo()
    {
        if (!mainManager.isGameOver)
        {
            Pause();
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        mainManager.CalculateScore();
        confirmationMenu.SetActive(false);
        gameOverMenu.SetActive(true);
    }
}
