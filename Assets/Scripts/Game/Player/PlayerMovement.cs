using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 800f; 
    public float jumpForce = 1200f; // On passe sur des valeurs réelles de pixels
    public float gravityScale = 5f; // Pour tomber plus vite et éviter l'effet "lune"

    [Header("State")]
    public bool isGrounded;
    private float horizontalInput;
    private bool jumpRequest;

    [Header("References")]
    public Rigidbody2D rb;
    public Transform transform;
    public Collider2D playerCollider;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;

    [Header("Audio")]
    public AudioSource jumpSound;
    public AudioClip jumpClip;

    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null) return;
        instance = this;
        transform = GetComponent<Transform>();
        
        rb.gravityScale = gravityScale;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
    }


    void Update()
    {
        horizontalInput = moveAction.action.ReadValue<Vector2>().x;

        if (jumpAction.action.triggered && isGrounded)
        {
            jumpSound.PlayOneShot(jumpClip);
            jumpRequest = true;
        }
    }

    void FixedUpdate()
    {
        float targetCenterX = horizontalInput * moveSpeed;
        rb.linearVelocity = new Vector2(targetCenterX, rb.linearVelocity.y);

        if (jumpRequest)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
            jumpRequest = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}