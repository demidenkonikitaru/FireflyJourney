using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{

    /* This script provides behaviour for UI.*/

    public Color blueMountains;

    [Header("UI Screens.")]

    [Header("Main menu.")]
    public GameObject mainMenuScreen;

    public Button start;
    public Button backgroundsBtn;
    public Button settingsBtn;
    public Button aboutBtn;
    public Button exitBtn;

    [Header("Pause menu.")]
    public GameObject pauseMenuScreen;

    public Button resumeBtn;
    public Button restartBtn;
    public Button mainMenuBtn;
    public Button exitBtn2;

    [Header("Gameplay screen.")]
    public GameObject gameplayScreen;

    public Button pauseBtn;

    [Header("Backgrounds screen.")]
    public GameObject backgroundsScreen;

    [Header("Settings screen.")]
    public GameObject settingsScreen;

    [Header("About screen.")]
    public GameObject aboutScreen;

    [Header("Game over screen.")]
    public GameObject gameOverScreen;

    public enum UIModes
    {
        Start = 0,
        MainMenu = 1,
        Pause = 2,
        Resume = 3,
        Backgrounds = 4,
        Settings = 5,
        About = 6,
        GameOver = 7,
        DisableSecondary = 8,
        Exit = 9

    }

	void Start () {
        //assigning main menu screen buttons

        start.onClick.AddListener(() => SwitchUIMode(0));
        backgroundsBtn.onClick.AddListener(() => SwitchUIMode(4));
        settingsBtn.onClick.AddListener(() => SwitchUIMode(5));
        aboutBtn.onClick.AddListener(() => SwitchUIMode(6));


        //assigning pause screen menu buttons

        resumeBtn.onClick.AddListener(() => SwitchUIMode(3));
        mainMenuBtn.onClick.AddListener(() => SwitchUIMode(1));

        //assigning gameplay screen buttons

        pauseBtn.onClick.AddListener(() => SwitchUIMode(2));

        //assigning backgrounds screen buttons

        //assigning settings screen buttons

        //assigning about screen buttons

        //assigning game over screen buttons


	}
	
	void Update () {
		
	}

    public void SwitchUIMode(int uiMode)
    {
        UIModes selectedMode = (UIModes)uiMode;

        switch (selectedMode)
        {
            case UIModes.Start:

                GameManager.Instance.StartGame();
                LevelGenerator.Instance.GenerateLevel();
                LevelGenerator.Instance.GenerateBottomPlatforms();



                gameplayScreen.SetActive(true);

                pauseMenuScreen.SetActive(false);
                mainMenuScreen.SetActive(false);

                break;

            case UIModes.MainMenu:

                GameManager.Instance.FinishGame();
                GameManager.Instance.ResumeGame();

                mainMenuScreen.SetActive(true);

                gameplayScreen.SetActive(false);
                pauseMenuScreen.SetActive(false);

                break;

            case UIModes.Pause:

                GameManager.Instance.PauseGame();

                pauseMenuScreen.SetActive(true);

                break;

            case UIModes.Resume:

                GameManager.Instance.ResumeGame();

                pauseMenuScreen.SetActive(false);

                break;

            case UIModes.Backgrounds:

                backgroundsScreen.SetActive(true);

                break;

            case UIModes.Settings:

                settingsScreen.SetActive(true);

                break;

            case UIModes.About:

                aboutScreen.SetActive(true);

                break;

            case UIModes.GameOver:

                gameOverScreen.SetActive(true);

                break;

            case UIModes.DisableSecondary:

                backgroundsScreen.SetActive(false);
                settingsScreen.SetActive(false);
                gameOverScreen.SetActive(false);
                aboutScreen.SetActive(false);

                break;

            case UIModes.Exit:

                Application.Quit();

                break;
        }
    }

    
}
