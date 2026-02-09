using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameOverManager : MonoBehaviour
{
    public CanvasGroup gameOverUI;
    public AudioSource audioSource;
    public AudioSource gameOverMusic;
    public AudioSource audioManager;
    public static GameOverManager instance;

    private float originalVolume; 

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scène !");
            return;
        }
        instance = this;
    }

    public void Start()
    {
        gameOverUI.alpha = 0;
        gameOverUI.interactable = false;
        gameOverUI.blocksRaycasts = false;
        
        Time.timeScale = 1f;
    }

    public void OnPlayerDeath()
    {
        originalVolume = audioManager.volume;

        audioManager.volume = originalVolume * 0.5f;

        if (gameOverMusic != null)
        {
            gameOverMusic.PlayOneShot(gameOverMusic.clip);
        }

        audioSource.PlayOneShot(audioSource.clip);
        
        gameOverUI.alpha = 1;
        gameOverUI.interactable = true;
        gameOverUI.blocksRaycasts = true;

        Time.timeScale = 0f;
    }

    public void RetryButton()
    {
        RestoreVolumeAndPlaySound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {
        RestoreVolumeAndPlaySound();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        RestoreVolumeAndPlaySound();
        Application.Quit();
    }

    private void RestoreVolumeAndPlaySound()
    {
        audioManager.volume = originalVolume;
        
        audioSource.PlayOneShot(audioSource.clip);
        
        Time.timeScale = 1f;
    }
}