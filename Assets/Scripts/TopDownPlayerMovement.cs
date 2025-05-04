using UnityEngine;

public class TopDownPlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform coreTransform; 

    [SerializeField] private float orbitRadius = 3.0f; 
    [SerializeField] private bool faceOutwards = true;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = mainCamera.nearClipPlane + 10;
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        Vector3 directionFromCore = (mouseWorldPos - coreTransform.position);
        directionFromCore.z = 0; 

        Vector3 targetPosition = coreTransform.position + directionFromCore.normalized * orbitRadius;
        transform.position = targetPosition;

        if (faceOutwards)
        {
            transform.right = directionFromCore.normalized; 
        }
        else
        {
            Vector3 lookDirection = (mouseWorldPos - transform.position).normalized;
            transform.right = lookDirection;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (coreTransform != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(coreTransform.position, orbitRadius);
        }
    }
}