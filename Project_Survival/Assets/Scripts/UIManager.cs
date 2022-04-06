using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Game state enums > MainMenu, playing(active), paused, gameOver
    //why cant i use serializefield
    public enum GameState { MainMenu, Playing, Paused, GameOver };
    public GameState currentState;
    public GameObject mainMenuPanel, pauseMenuPanel, gameOverPanel, inGameUI, discoveryPanel, popMessagePanel;

    public TextMeshProUGUI healthText, awardText, awardValveText, popMessageText;
    public TextMeshProUGUI XpText, playerLevelText;

    PlayerManager _playerManager;

    bool timing;
    float timer = 1f;

    //Game current state variable

    //what will happen when on awake when the script is launched > Load Mainmenu else its Active

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            SetGameState(GameState.MainMenu);
        }
        else
        {
            SetGameState(GameState.Playing);
        }
    }

    private void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
    }

    void Update()
    {
        PlayerInput();
        UpdatePlayerHpUI();
        UpdatePlayerXpText();
        if (timing)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                discoveryPanel.SetActive(false);
                timer = 1f;
                timing = false;
            }
        }
        
    }

    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("Working");
            if (currentState == GameState.Playing)
            {
                SetGameState(GameState.Paused);
            }
            else if (currentState == GameState.Paused)
            {
                SetGameState(GameState.Playing);
            }
        }
    }
    // CheckGameState(GameState newGameState) >
    public void SetGameState(GameState newGameState)
    {
        currentState = newGameState;
        switch (currentState)
        {
            case GameState.MainMenu:
                MainMenu();
                break;
            case GameState.Playing:
                GameActive();
                //Manager.gamePaused = false;
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                GamePaused();
                //Manager.gamePaused = true;
                Time.timeScale = 0f;
                break;
            case GameState.GameOver:
                GameOver();
                //Manager.gamePaused = true;
                Time.timeScale = 0f;
                break;
        }
    }


    public void MainMenu()
    {
        //in game Ui
        inGameUI.SetActive(false);
        mainMenuPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);

    }

    public void GameActive()
    {
        inGameUI.SetActive(true);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void GamePaused()
    {
        inGameUI.SetActive(false);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        inGameUI.SetActive(false);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("FirstLevel");
        SetGameState(GameState.Playing);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        SetGameState(GameState.MainMenu);
    }


    public void PauseGame()
    {
        SetGameState(GameState.Paused);
    }

    public void ResumeGame()
    {
        SetGameState(GameState.Playing);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdatePlayerXpText()
    {
        XpText.text = "EXP : " + _playerManager.CurrentXp.ToString();
        playerLevelText.text = _playerManager.playerLevel.ToString();
    }

    public void AwardXpMessage(string award, int awardValve)
    {
        awardText.text = award;
        awardValveText.text = awardValve.ToString() + "Xp";
        timing = true;
        discoveryPanel.SetActive(true);
    }

    public void PopMessage(string message)
    {
        popMessageText.text = message;
    }

    
    public void UpdatePlayerHpUI()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        healthText.text = "Player Health: " + _playerManager.PlayerHp.ToString();

    }
}
