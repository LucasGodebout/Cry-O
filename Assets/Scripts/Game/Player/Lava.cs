using UnityEngine;
using UnityEngine.UI; // Nécessaire pour RawImage

public class RisingLava : MonoBehaviour
{
    [Header("Movement Settings")]
    public float baseSpeed = 1f;         
    public float speedAdd = 0.2f;    
    public float heightStep = 100f;     
    public Transform player;

    [Header("Horizontal Oscillation")]
    public float horizontalSpeed = 2f;    // Vitesse du mouvement gauche-droite
    public float horizontalRange = 50f;   // Distance parcourue à gauche et à droite
    private float startX;                 // Position X de départ

    [Header("Interaction Settings")]
    public float bounceForce = 15f;      
    public int damage = 1;              

    private float currentSpeed;
    private float nextThreshold;   
    public RawImage lavaImage;

    public static RisingLava instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de RisingLava dans la scène !");
            return;
        }
        instance = this;
    }  

    void Start()
    {
        currentSpeed = baseSpeed;
        nextThreshold = player.position.y + heightStep; 
        
        // On mémorise le X de départ pour l'oscillation
        if (lavaImage != null)
        {
            startX = lavaImage.rectTransform.anchoredPosition.x;
        }
    }

    void Update()
    {
        // 1. Accélération de la lave
        if (player.position.y >= nextThreshold)
        {
            currentSpeed += speedAdd;
            nextThreshold += heightStep;
        }

        // 2. Mouvement Vertical (Montée)
        transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);

        // 3. Mouvement Horizontal (Oscillation)
        if (lavaImage != null)
        {
            // PingPong renvoie une valeur qui va de 0 à horizontalRange * 2
            float xOffset = Mathf.PingPong(Time.time * horizontalSpeed, horizontalRange * 2) - horizontalRange;
            
            // On applique la nouvelle position X par rapport au point de départ
            Vector2 pos = lavaImage.rectTransform.anchoredPosition;
            pos.x = startX + xOffset;
            lavaImage.rectTransform.anchoredPosition = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (CameraDeath.instance != null && CameraDeath.instance.isDead) 
                return;

            Health health = collision.GetComponent<Health>();
            if (health != null) health.TakeDamage(damage);

            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);
            }
        }
    }
}