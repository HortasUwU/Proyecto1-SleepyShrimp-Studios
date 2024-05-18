using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject pauseMenu;
    public GameObject deadMenu;
    public GameObject health;
    public GameObject settingsMenu;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

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

    public void Start()
    {
        // Inicializar los sliders con los valores actuales del AudioManager
        musicSlider.value = AudioManager.Instance.Musica.volume;
        sfxSlider.value = AudioManager.Instance.SFX.volume;

        // Añadir listeners a los sliders para manejar los cambios
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);

        health.SetActive(true);
        ToggleMenuDeOpciones();
    }

    public void DeadMenu()
    {
        bool isDeadMenuActive = !deadMenu.activeSelf; // Alternar el estado de activación del menú de muerte
        deadMenu.SetActive(isDeadMenuActive); // Establecer la visibilidad del menú de muerte

        health.SetActive(isDeadMenuActive);
    }

    public void ToggleMenuDeOpciones()
    {
        bool isActive = settingsMenu.activeSelf;
        settingsMenu.SetActive(!isActive);
    }

    public void SaveOptions()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.Save();
    }

    private void SetMusicVolume(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);
    }

    private void SetSFXVolume(float volume)
    {
        AudioManager.Instance.SetSFXVolume(volume);
    }
}
