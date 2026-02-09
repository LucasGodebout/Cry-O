using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePause = false;
    public CanvasGroup pauseMenuUI;
    public AudioSource audioSource;

    public static PauseMenu instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PauseMenu dans la scène !");
            return;
        }
        instance = this;
    }

    void Start()
    {
        pauseMenuUI.alpha = 0;
        pauseMenuUI.interactable = false;
        pauseMenuUI.blocksRaycasts = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePause)
            {
                audioSource.PlayOneShot(audioSource.clip);
                Resume();
            }
            else
            {
                audioSource.PlayOneShot(audioSource.clip);
                Paused();
            }
        }
    }

    public void PauseButton()
    {
    if (isGamePause)
        {
            audioSource.PlayOneShot(audioSource.clip);
            Resume();
        }
        else
        {
            audioSource.PlayOneShot(audioSource.clip);
            Paused();
        }
    }

    public void ResumeButton()
    {
        audioSource.PlayOneShot(audioSource.clip);
        Resume();
    }

    public void SettingsButton()
    {
        audioSource.PlayOneShot(audioSource.clip);
        AudioPauseMenu.instance.OpenSettings();
    }

    public void MainMenuButton()
    {
        audioSource.PlayOneShot(audioSource.clip);
        pauseMenuUI.alpha = 0;
        pauseMenuUI.interactable = false;
        pauseMenuUI.blocksRaycasts = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    void Paused()
    {
        pauseMenuUI.alpha = 1;
        pauseMenuUI.interactable = true;
        pauseMenuUI.blocksRaycasts = true;
        Time.timeScale = 0;
        isGamePause = true;
    }
        void Resume()
    {
        pauseMenuUI.alpha = 0;
        pauseMenuUI.interactable = false;
        pauseMenuUI.blocksRaycasts = false;
        Time.timeScale = 1;
        isGamePause = false;
    }
}
