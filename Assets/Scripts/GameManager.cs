using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Instancia est�tica para acceder al GameManager desde cualquier parte del c�digo

    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    public GameState currentState; // Estado actual del juego
    public GameObject pauseMenu; // Referencia al men� de pausa

    void Awake()
    {
        // Verificar si ya hay una instancia del GameManager
        if (instance == null)
        {
            // Si no hay, establecer esta instancia como la instancia activa
            instance = this;
        }
        else if (instance != this)
        {
            // Si ya hay una instancia y no es esta, destruir este objeto
            Destroy(gameObject);
        }

        // Mantener este objeto vivo entre las escenas
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Iniciar el juego
        currentState = GameState.Playing;
        // Desactivar el men� de pausa al inicio
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    void Update()
    {
        // Controlar el estado del juego
        switch (currentState)
        {
            case GameState.Playing:
                // L�gica del juego mientras est� en juego
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    PauseGame();
                }
                break;
            case GameState.Paused:
                // L�gica del juego cuando est� pausado
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ResumeGame();
                }
                break;
            case GameState.GameOver:
                // L�gica del juego cuando termina el juego
                break;
        }
    }

    // M�todo para pausar el juego
    public void PauseGame()
    {
        Time.timeScale = 0f; // Pausar el tiempo en el juego
        currentState = GameState.Paused; // Cambiar el estado del juego a pausado
        Cursor.lockState = CursorLockMode.None; // Desbloquear el cursor del rat�n
        Cursor.visible = true; // Hacer visible el cursor del rat�n
        // Activar el men� de pausa si est� asignado
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
    }

    // M�todo para reanudar el juego
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Reanudar el tiempo en el juego
        currentState = GameState.Playing; // Cambiar el estado del juego a jugando
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor del rat�n
        Cursor.visible = false; // Ocultar el cursor del rat�n
        // Desactivar el men� de pausa si est� asignado
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    // M�todo para terminar el juego
    public void EndGame()
    {
        currentState = GameState.GameOver; // Cambiar el estado del juego a finalizado
    }
}
