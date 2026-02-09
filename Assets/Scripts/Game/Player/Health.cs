using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;
public class Health : MonoBehaviour
{
    public int maxHealth = 4;
    private int currentHealth;
    public bool isInvicible = false;
    public float invicibilityFlashDelay = 0.2f;
    public float invicibilityTimeAfterHit = 2f;
    public Image[] HealthBars;
    public Sprite[] PlayerSprites = new Sprite[4];
    public SpriteRenderer graphics;
    public Sprite CryoDeadSprite;
    public AudioSource hitSound;
    public AudioClip hitClip;
    public static Health instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Health dans la scène !");
            return;
        }
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBars();
    }
    public void TakeDamage(int damage)
    {
        if (!isInvicible)
        {
            hitSound.PlayOneShot(hitClip);
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            if (currentHealth <= 0)
            {

                UpdateHealthBars();
                Die();
                return;
            }
            isInvicible = true;
            StartCoroutine(InvicibilityFlash());
            UpdateHealthBars();
            UpdatePlayerSprite();
            StartCoroutine(HandleInvicibilityDelay());
        }
    }

    void UpdateHealthBars()
    {
        for (int i = 0; i < HealthBars.Length; i++)
        {
            HealthBars[i].enabled = i < currentHealth;
        }
    }
    void UpdatePlayerSprite()
    {
        int spriteIndex = Mathf.Clamp(maxHealth - currentHealth, 0, PlayerSprites.Length - 1);
        GetComponent<SpriteRenderer>().sprite = PlayerSprites[spriteIndex];
    }

public void Die()
{
    Debug.Log("Character has died.");
    
    if (CameraDeath.instance != null) CameraDeath.instance.isDead = true;
    PlayerMovement.instance.enabled = false; 
    PlayerMovement.instance.rb.linearVelocity = Vector2.zero;
    PlayerMovement.instance.rb.angularVelocity = 0f;
    PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Static;
    if(PlayerMovement.instance.playerCollider != null)
        PlayerMovement.instance.playerCollider.enabled = false;
    GetComponent<SpriteRenderer>().sprite = PlayerSprites[PlayerSprites.Length - 1];
    if (HealthBars.Length > 0) HealthBars[0].enabled = false;
    if (RisingLava.instance != null) RisingLava.instance.enabled = false;
    GameOverManager.instance.OnPlayerDeath();
}
    public IEnumerator InvicibilityFlash()
    {
        while (isInvicible)
        {
            PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(invicibilityTimeAfterHit);
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        isInvicible = false;
    }
}