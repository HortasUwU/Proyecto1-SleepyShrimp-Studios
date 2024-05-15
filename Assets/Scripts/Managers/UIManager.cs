using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject pauseMenu;
    public GameObject deadMenu;
    public GameObject health;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        health.SetActive(true);

    }

    public void DeadMenu()
{
    bool isDeadMenuActive = !deadMenu.activeSelf; // Alternar el estado de activaci�n del men� de muerte
    deadMenu.SetActive(isDeadMenuActive); // Establecer la visibilidad del men� de muerte

    health.SetActive(isDeadMenuActive);
}
}
