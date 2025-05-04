using UnityEngine;

public class PlatformerProjectile : MonoBehaviour
{

    public float speed = 10f;
    public float rotationSpeed = 360f; 
    public float lifetime = 2f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (rotationSpeed != 0) 
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }
}