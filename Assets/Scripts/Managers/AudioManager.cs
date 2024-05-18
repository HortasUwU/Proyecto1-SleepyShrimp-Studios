using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource SFX;
    [SerializeField] public AudioSource Musica;
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClip[] backgroundMusicClips;
    private int currentTrackIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // Cargar volúmenes guardados
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            Musica.volume = PlayerPrefs.GetFloat("MusicVolume");
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SFX.volume = PlayerPrefs.GetFloat("SFXVolume");
        }

        // Reproducir la primera canción
        PlayBackgroundMusic(currentTrackIndex);
    }

    public void PlaySound(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }

    public void PlayBackgroundMusic(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < backgroundMusicClips.Length)
        {
            currentTrackIndex = trackIndex;
            Musica.clip = backgroundMusicClips[trackIndex];
            Musica.loop = true;
            Musica.Play();
        }
        else
        {
            Debug.LogWarning("Track index out of range");
        }
    }

    public void StopBackgroundMusic()
    {
        Musica.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        Musica.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        SFX.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void NextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % backgroundMusicClips.Length;
        PlayBackgroundMusic(currentTrackIndex);
    }

    public void PreviousTrack()
    {
        currentTrackIndex = (currentTrackIndex - 1 + backgroundMusicClips.Length) % backgroundMusicClips.Length;
        PlayBackgroundMusic(currentTrackIndex);
    }
}
