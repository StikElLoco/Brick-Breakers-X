using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Parents")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject chooseDifficulty;
    [SerializeField] GameObject choosePattern;
    [SerializeField] GameObject confirmationMenu;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject easterEggMenu;

    [Header("TMP Text Fields")]
    [SerializeField] TMP_Text highscoreText;

    [Header("Data Manager")]
    [SerializeField] DataManager dataManager;

    [Header("Menu Booleans")]
    [SerializeField] bool isInSettings = false;
    [SerializeField] bool isInChoice = false;
    [SerializeField] bool isQuitting = false;

    [Header("Other")]
    public bool isMuted;
    public Toggle muteButton;

    private AudioSource audioSource;

    private int i = 0;
    

    private void Start()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();

        //Load player data
        dataManager.Load();

        audioSource = GetComponent<AudioSource>();
        //Set highscore
        highscoreText.text = "Highscore: " + dataManager.highscore;

        isMuted = dataManager.isMuted;
        audioSource.mute = isMuted;
        //Life saver, sets the checkmark of the mute toggle without triggering it's actions
        muteButton.SetIsOnWithoutNotify(isMuted);
    }

    private void Update()
    {
        //When ESC is pressed
        if (Input.GetButtonDown("Cancel"))
        {
            //If in the main menu and the other windows are closed
            if (!isQuitting && !isInChoice && !isInSettings)
            {
                QuitConfirmation();
            }
            //If the difficulty choice window is open, close it and open the menu
            if (isInChoice)
            {
                Menu();
            }
            //If the settings window is open, close it and open the menu
            if (isInSettings)
            {
                Menu();
            }
            //If the quit confirmation window is open, close it and open the menu
            if (isQuitting)
            {
                Menu();
            }
        }
        //When Space is pressed
        if (Input.GetButtonDown("Start"))
        {
            i++;
            if (i == 5)
            {
                easterEggMenu.SetActive(true);
            }
        }
    }

    //Opening the main menu
    public void Menu()
    {
        isInChoice = false;
        isInSettings = false;
        isQuitting = false;

        mainMenu.SetActive(true);

        chooseDifficulty.SetActive(false);
        choosePattern.SetActive(false);
        settings.SetActive(false);
        confirmationMenu.SetActive(false);
    }

    //Pressing Start Game in the main menu
    public void ChooseDifficulty()
    {
        isInChoice = true;
        mainMenu.SetActive(false);
        chooseDifficulty.SetActive(true);
    }
    //Pressing on difficulty setting
    public void ChoosePattern()
    {
        isInChoice = true;
        chooseDifficulty.SetActive(false);
        choosePattern.SetActive(true);
    }

    //Load the game scene
    public void StartGame()
    {
        dataManager.Save();
        SceneManager.LoadScene("MainScene");
    }
    //Open Settings window
    public void Settings()
    {
        isInSettings = true;
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }
    //Save the data and then exit the application
    public void Quit()
    {
        dataManager.Save();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); 
#endif
    }
    //Pressing Quit in the main menu
    public void QuitConfirmation()
    {
        isQuitting = true;
        mainMenu.SetActive(false);
        confirmationMenu.SetActive(true);
    }
    //Choosing Yes in the confirmation window
    public void ConfirmationYes()
    {
        Quit();
    }
    //Choosing No in the confirmation window
    public void ConfirmationNo()
    {
        mainMenu.SetActive(true);
        confirmationMenu.SetActive(false);
    }

    //Choose Difficulty
    public void Easy()
    {
        dataManager.difficulty = 1;
        ChoosePattern();
    }
    public void Medium()
    {
        dataManager.difficulty = 2;
        ChoosePattern();
    }
    public void Hard()
    {
        dataManager.difficulty = 3;
        ChoosePattern();
    }

    //Pattern input
    public void Pattern(int pattern)
    {
        dataManager.pattern = pattern;
        StartGame();
    }

    //Play the button sellect sound, used for buttons but can be called from code as well. In this case using AudioSource.Play on the buttons would work as well
    public void PlayAudioClip()
    {
        audioSource.Play();
    }
    //Mute toggle
    public void Mute()
    {
        isMuted = !isMuted;
        dataManager.isMuted = isMuted;
        audioSource.mute = isMuted;
    }
}
