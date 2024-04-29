using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Instancia estática para acceder al GameManager desde cualquier parte del código

    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    public GameState currentState; // Estado actual del juego
    public GameObject pauseMenu; // Referencia al menú de pausa

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
        // Desactivar el menú de pausa al inicio
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
                // Lógica del juego mientras está en juego
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    PauseGame();
                }
                break;
            case GameState.Paused:
                // Lógica del juego cuando está pausado
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ResumeGame();
                }
                break;
            case GameState.GameOver:
                // Lógica del juego cuando termina el juego
                break;
        }
    }

    // Método para pausar el juego
    public void PauseGame()
    {
        Time.timeScale = 0f; // Pausar el tiempo en el juego
        currentState = GameState.Paused; // Cambiar el estado del juego a pausado
        Cursor.lockState = CursorLockMode.None; // Desbloquear el cursor del ratón
        Cursor.visible = true; // Hacer visible el cursor del ratón
        // Activar el menú de pausa si está asignado
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
    }

    // Método para reanudar el juego
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Reanudar el tiempo en el juego
        currentState = GameState.Playing; // Cambiar el estado del juego a jugando
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor del ratón
        Cursor.visible = false; // Ocultar el cursor del ratón
        // Desactivar el menú de pausa si está asignado
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    // Método para terminar el juego
    public void EndGame()
    {
        currentState = GameState.GameOver; // Cambiar el estado del juego a finalizado
    }
}
