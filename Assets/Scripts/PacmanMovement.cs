using UnityEngine;

public class PacmanMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        if (moveInput.sqrMagnitude > 0.01f) 
        {
            UpdateSpriteOrientation(moveX, moveY);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }

    void UpdateSpriteOrientation(float moveX, float moveY)
    {
        if (Mathf.Abs(moveY) > Mathf.Abs(moveX))
        {
            spriteRenderer.flipX = false; 

            if (moveY > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else 
            {
                transform.rotation = Quaternion.Euler(0, 0, -90); 
            }
        }
        else if (Mathf.Abs(moveX) > 0) 
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

            if (moveX > 0)
            {
                spriteRenderer.flipX = false; 
            }
            else 
            {
                spriteRenderer.flipX = true; 
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bugs"))
        {
            Destroy(other.gameObject);
            PacmanGameManager.Instance.BugsEaten(); 
        }
    }
}