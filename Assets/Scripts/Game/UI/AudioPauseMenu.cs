using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class AudioPauseMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public CanvasGroup settingsUI;
    public Slider musicSlider;
    public Slider soundSlider;
    public AudioSource soundAudioSource;

    public static AudioPauseMenu instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de AudioPauseMenu dans la scène !");
            return;
        }
        instance = this;
    }

    void Start()
    {
    settingsUI.alpha = 0;
    settingsUI.interactable = false;
    settingsUI.blocksRaycasts = false;

    audioMixer.GetFloat("Music", out float musicVolume);
    musicSlider.value = musicVolume;
    audioMixer.GetFloat("Sound", out float soundVolume);
    soundSlider.value = soundVolume;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && settingsUI.alpha == 1)
        {
            soundAudioSource.PlayOneShot(soundAudioSource.clip);
            QuitSettings();
        }
    }

    public void OpenSettings()
    {
        soundAudioSource.PlayOneShot(soundAudioSource.clip);
        settingsUI.alpha = 1;
        settingsUI.interactable = true;
        settingsUI.blocksRaycasts = true;
        Time.timeScale = 0;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music",volume);
    }

        public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("Sounds",volume);
    }

    public void QuitSettings()
    {
        PauseMenu.instance.PauseButton();
        settingsUI.alpha = 0;
        settingsUI.interactable = false;
        settingsUI.blocksRaycasts = false;
    }
}
