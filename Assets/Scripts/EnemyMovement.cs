using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float minSpeed = 2f;
    [SerializeField] private float maxSpeed = 5f;

    private float currentSpeed;
    private Transform coreTransform;

    void Start()
    {
        GameObject coreObject = GameObject.FindGameObjectWithTag("Core");
        coreTransform = coreObject.transform;

        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, coreTransform.position, currentSpeed * Time.deltaTime);

         Vector3 directionToCore = (coreTransform.position - transform.position).normalized;
         transform.right = directionToCore; 
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Core"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.EnemyReachedCore(this); 
            }
            Destroy(gameObject);
        }
    }
}