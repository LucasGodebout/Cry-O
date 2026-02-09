using UnityEngine;
using TMPro; // Obligatoire pour utiliser TextMeshPro

public class ScoreManager : MonoBehaviour
{
    [Header("References")]
    public Transform player;           // Glisse ton joueur ici
    public TextMeshProUGUI scoreText;  // Glisse ton texte UI ici

    [Header("Settings")]
    public float multiplier = 1f;      // Pour augmenter artificiellement le score
    
    private float startY;
    private float maxReachedY;

    void Start()
    {
        // On définit le point de départ pour que le score commence à 0
        if (player != null)
        {
            startY = player.position.y;
            maxReachedY = startY;
        }
    }

    void Update()
    {
        // CONDITION DE MORT
        if (player == null || (CameraDeath.instance != null && CameraDeath.instance.isDead)) 
            return;

        if (player.position.y > maxReachedY)
        {
            maxReachedY = player.position.y;
        }

        int score = Mathf.FloorToInt((maxReachedY - startY) * multiplier);
        scoreText.text = score.ToString();
    }
}