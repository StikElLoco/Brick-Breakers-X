using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Parents")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject chooseDifficulty;
    [SerializeField] GameObject confirmationMenu;
    [SerializeField] GameObject settings;

    [Header("TMP Text Fields")]
    [SerializeField] TMP_Text highscoreText;

    [Header("Data Manager")]
    [SerializeField] DataManager dataManager;

    [Header("Menu Booleans")]
    [SerializeField] bool isInSettings = false;
    [SerializeField] bool isInChoice = false;
    [SerializeField] bool isQuitting = false;

    private void Start()
    {
        dataManager.Load();
        highscoreText.text = "Highscore: " + dataManager.highscore;
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
                isInChoice = false;
                mainMenu.SetActive(true);
                chooseDifficulty.SetActive(false);
            }
            //If the settings window is open, close it and open the menu
            if (isInSettings)
            {
                isInSettings = false;
                mainMenu.SetActive(true);
                settings.SetActive(false);
            }
            //If the quit confirmation window is open, close it and open the menu
            if (isQuitting)
            {
                isQuitting = false;
                mainMenu.SetActive(true);
                confirmationMenu.SetActive(false);
            }
        }
    }
    //Pressing Start Game in the main menu
    public void ChooseDifficulty()
    {
        isInChoice = true;
        mainMenu.SetActive(false);
        chooseDifficulty.SetActive(true);
    }
    //Load the game scene
    public void StartGame()
    {
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
        StartGame();
    }
    public void Medium()
    {
        dataManager.difficulty = 2;
        StartGame();
    }
    public void Hard()
    {
        dataManager.difficulty = 3;
        StartGame();
    }
}
