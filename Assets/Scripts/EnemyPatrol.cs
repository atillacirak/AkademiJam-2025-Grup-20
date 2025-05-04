using UnityEngine;
using System.Collections.Generic;

public class EnemyPatrol : MonoBehaviour
{
    public List<Transform> patrolPoints = new List<Transform>();

    public float moveSpeed = 3.0f;

    public float rotationSpeed = 180.0f; 

    public float stoppingDistance = 0.2f;

    private int currentPatrolIndex = 0;
    private Transform currentTarget;
    private Rigidbody2D rb; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.freezeRotation = true;

        currentPatrolIndex = 0;
        currentTarget = patrolPoints[currentPatrolIndex];

        if (currentTarget != null)
        {
            Vector2 directionToTarget = (Vector2)currentTarget.position - (Vector2)transform.position;
            if (directionToTarget != Vector2.zero)
            {
                float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f; // Adjust by -90 if sprite faces 'up' by default
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
    }

    void Update()
    {

        Vector2 currentPosition = transform.position; 
        Vector2 targetPosition = currentTarget.position; 

        Vector2 directionToTarget = targetPosition - currentPosition;
        float distanceToTarget = directionToTarget.magnitude;

        if (distanceToTarget <= stoppingDistance)
        {
            currentPatrolIndex++;
            if (currentPatrolIndex >= patrolPoints.Count)
            {
                currentPatrolIndex = 0;
            }
            currentTarget = patrolPoints[currentPatrolIndex];
            if (currentTarget == null)
            {
                Debug.LogError($"EnemyPatrol2D: Patrol point at index {currentPatrolIndex} is null! Disabling movement.", this);
                return;
            }
        }
        else 
        {
            
            if (directionToTarget != Vector2.zero)
            {
                float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f; // Adjust offset (-90f) if needed!

                float currentAngle = transform.eulerAngles.z;
                float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

                transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
            }

            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Debug.Log(gameObject.name + " hit by 2D projectile: " + other.gameObject.name);
            Destroy(other.gameObject);
            Destroy(gameObject);       
        }
    }

    void OnDrawGizmosSelected()
    {


        Gizmos.color = Color.cyan;
        
        for (int i = 0; i < patrolPoints.Count; i++)
        {
            if (patrolPoints[i] != null)
            {
                
                Vector3 pointPos = new Vector3(patrolPoints[i].position.x, patrolPoints[i].position.y, transform.position.z);
                Gizmos.DrawWireSphere(pointPos, stoppingDistance); 

                Transform nextPoint = null;
                if (i + 1 < patrolPoints.Count)
                {
                    nextPoint = patrolPoints[i + 1];
                }
                else if (patrolPoints.Count > 1)
                { 
                    nextPoint = patrolPoints[0];
                }

                if (nextPoint != null)
                {
                    Vector3 nextPointPos = new Vector3(nextPoint.position.x, nextPoint.position.y, transform.position.z);
                    Gizmos.DrawLine(pointPos, nextPointPos);
                }
            }
        }

        if (currentTarget != null)
        {
            Gizmos.color = Color.red;
            Vector3 targetPos = new Vector3(currentTarget.position.x, currentTarget.position.y, transform.position.z);
            Gizmos.DrawLine(transform.position, targetPos);
        }
    }
}