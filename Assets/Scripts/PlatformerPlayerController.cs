using UnityEngine;

public class PlatformerPlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public GameObject projectilePrefabA; 
    public GameObject projectilePrefabB; 
    public Transform firePoint;

    private Rigidbody2D rb;
     private Animator animator; 

    private float horizontalInput;
    private bool isGrounded;
    private bool facingRight = true;
    private bool useProjectileA = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            Jump();
        }

        if (Input.GetButtonDown("Fire1")) 
        {
            Shoot();
        }

        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        animator.SetBool("IsJumping", !isGrounded);

    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    void Shoot()
    {
        GameObject prefabToShoot = useProjectileA ? projectilePrefabA : projectilePrefabB;

        Instantiate(prefabToShoot, firePoint.position, firePoint.rotation);
        useProjectileA = !useProjectileA;       
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}