using UnityEngine;


public class PlayerShooting : MonoBehaviour
{

    [SerializeField] private Transform firePoint;     
    [SerializeField] private LayerMask enemyLayerMask; 

    [SerializeField] private float fireRate = 0.25f; 
    [SerializeField] private float aimLineMaxLength = 50f; 

    [SerializeField] private Color aimLineColor = Color.red; 

    private float nextFireTime = 0f;
    private Camera mainCamera;
    private LineRenderer aimLineRenderer;

    void Start()
    {
        mainCamera = Camera.main;
        aimLineRenderer = GetComponent<LineRenderer>();

        SetupAimLine();
    }

    void SetupAimLine()
    {
        aimLineRenderer.positionCount = 2;
        aimLineRenderer.startColor = aimLineColor;
        aimLineRenderer.endColor = aimLineColor;
        aimLineRenderer.useWorldSpace = true;
    }

    void Update()
    {
        UpdateAimVisualization();

        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            ShootRaycast();
            nextFireTime = Time.time + fireRate;
        }
    }

    void UpdateAimVisualization()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = mainCamera.nearClipPlane + 10; 
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0;

        Vector2 aimDirection = (mouseWorldPos - firePoint.position).normalized;

        aimLineRenderer.SetPosition(0, firePoint.position);
        aimLineRenderer.SetPosition(1, firePoint.position + (Vector3)aimDirection * aimLineMaxLength); 
    }


    void ShootRaycast()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = mainCamera.nearClipPlane + 10;
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0;

        Vector2 shootDirection = (mouseWorldPos - firePoint.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, shootDirection, aimLineMaxLength, enemyLayerMask);

        if (hit.collider != null)
        {
             Debug.Log("Hit: " + hit.collider.name); 

            if (GameManager.Instance != null)
            {
                GameManager.Instance.IncrementScore();
            }

            Destroy(hit.collider.gameObject);
        }
    }
}