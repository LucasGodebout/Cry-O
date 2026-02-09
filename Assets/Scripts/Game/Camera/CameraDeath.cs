using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class CameraDeath : MonoBehaviour
{
    public Transform player;
    public CanvasGroup gameOverMenu;
    public float deathThreshold = 6f;

    private Camera mainCamera;
    public bool isDead = false;
    public static CameraDeath instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de CameraDeath dans la scène !");
            return;
        }
        instance = this;
    } 

    void Start()
    {
        mainCamera = Camera.main;
        if (gameOverMenu != null)
        {
            gameOverMenu.alpha = 0;
            gameOverMenu.interactable = false;
            gameOverMenu.blocksRaycasts = false;
        }
    }
    void Update()
    {
        if (isDead || player == null) return;

        Vector3 screenPos = mainCamera.WorldToViewportPoint(player.position);
        if (screenPos.y < -0.01f) 
        {
            if (player.position.y > 0) 
            {
                Health.instance.Die();
            }
            isDead = true;
        }
    }
}