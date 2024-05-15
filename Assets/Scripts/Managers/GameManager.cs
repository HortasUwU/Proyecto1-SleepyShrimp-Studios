using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum GameState
    {
        Playing,
        Paused,
        GameOver,
        Interactuating
    }

    public GameState currentState;
    public List<RoomFSM> allRooms;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currentState = GameState.Playing;
        UIManager.instance.pauseMenu.SetActive(false);
    }

    void Update()
    {
        switch (currentState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    PauseGame();
                }
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None; 
                Cursor.visible = true;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ResumeGame();
                }
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.Interactuating:
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }

    public void EndGame()
    {
        currentState = GameState.GameOver;
        UIManager.instance.DeadMenu();
    }

    public void Interaction()
    {
        currentState = GameState.Interactuating;
    }

    public void PauseGame()
    {
        currentState = GameState.Paused;
        UIManager.instance.PauseMenu();
    }

    public void ResumeGame()
    {
        currentState = GameState.Playing;
        UIManager.instance.Resume();

    }

    public void Exit()
    {
        PlayerPrefs.DeleteKey("RoomState");
        Application.Quit();
    }

    public void Retry()
    {
        UIManager.instance.deadMenu.SetActive(false);
        currentState = GameState.Playing;
        //LoadGameState();
        PlayerManager.instance.spawnear();

    }

    public void SaveGameState()
    {
        // Guarda el estado de todas las habitaciones
        foreach (RoomFSM room in allRooms)
        {
            PlayerPrefs.SetInt(room.gameObject.name + "_RoomState", (int)room.currentState);
        }
    }

    public void LoadGameState()
    {
        // Carga el estado de todas las habitaciones
        foreach (RoomFSM room in allRooms)
        {
            if (PlayerPrefs.HasKey(room.gameObject.name + "_RoomState"))
            {
                int savedState = PlayerPrefs.GetInt(room.gameObject.name + "_RoomState");
                room.currentState = (RoomFSM.RoomState)savedState;
            }
        }
    }
}
