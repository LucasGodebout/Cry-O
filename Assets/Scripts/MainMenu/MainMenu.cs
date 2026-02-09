using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public string game;
    public AudioSource audioSource;

    public void StartGame()
    {
        audioSource.PlayOneShot(audioSource.clip);
        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        audioSource.PlayOneShot(audioSource.clip);
        Application.Quit();
    }
}
