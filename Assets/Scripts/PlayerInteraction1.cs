using UnityEngine;
using System.Collections;

public class PlayerInteraction1 : MonoBehaviour
{
    [Header("References")]
    public Transform sitPoint;
    public GameObject computerScreen;
    public Transform screenLookTarget;
    public Transform cameraHolder;
    public Transform PlayerF;
    public KeyCode interactKey = KeyCode.E;
    public Transform nextPoint;
    public Animator playerAnimator;

    [Header("Settings")]
    public float cameraMoveDuration = 0.8f;
    public float cameraDistance = 1.2f;
    public float interactionDistance = 2f;

    private CamControl camControl;
    private Rigidbody rb;
    private bool canSit = false;
    private bool isUsingComputer = false;
    private Vector3 originalHolderPosition;
    private Quaternion originalHolderRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camControl = Object.FindFirstObjectByType<CamControl>();

        if (cameraHolder != null)
        {
            originalHolderPosition = cameraHolder.localPosition;
            originalHolderRotation = cameraHolder.localRotation;
        }

        if (computerScreen != null)
            computerScreen.SetActive(false);
    }

    void Update()
    {
        if (canSit && Input.GetKeyDown(interactKey))
        {
            if (!isUsingComputer)
            {
                rb.detectCollisions = false;
                SitDown();
                if (camControl != null)
                    camControl.isLocked = true;
                StartCoroutine(MoveCameraToScreen());
                playerAnimator.SetBool("isSitting", true); // Animator'da "isSitting" parametresini true yap


            }
            else
            {
                CloseComputer();
            }
        }

        if (isUsingComputer && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseComputer();
        }
    }

    void SitDown()
    {
        if (sitPoint == null) return;

        rb.detectCollisions = false;
        transform.position = sitPoint.position;
        transform.rotation = sitPoint.rotation;

        if (rb != null)
        {
            rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    IEnumerator MoveCameraToScreen()

    {
        // 1. REFERANS KONTROLLERI
        if (cameraHolder == null || screenLookTarget == null || Camera.main == null)
        {
            Debug.LogError("Eksik referans!", this);
            yield break;
        }

        // 2. KARAKTER KILITLEME
        transform.rotation = Quaternion.Euler(
            0f,
            Quaternion.LookRotation(screenLookTarget.forward).eulerAngles.y,
            0f
        );

        // 3. SABIT YUKSEKLIK
        float fixedHeight = cameraHolder.position.y;

        // 4. HEDEF POZISYON (Y'yi koruyarak)
        Vector3 targetPosition = new Vector3(
            screenLookTarget.position.x - (screenLookTarget.forward.x * cameraDistance),
            fixedHeight,
            screenLookTarget.position.z - (screenLookTarget.forward.z * cameraDistance)
        );

        // 5. KAMERA ROTASYONU
        Camera.main.transform.rotation = Quaternion.LookRotation(
            screenLookTarget.position - targetPosition
        );

        // 6. YAKINLASMA ANIMASYONU
        Vector3 startPos = cameraHolder.position;
        float elapsed = 0f;

        while (elapsed < cameraMoveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / cameraMoveDuration);

            Vector3 newPos = Vector3.Lerp(startPos, targetPosition, t);
            newPos.y = fixedHeight;
            cameraHolder.position = newPos;

            yield return null;
        }
        OpenComputer();
    }

    void OpenComputer()
    {
        if (computerScreen != null)
        {
            computerScreen.SetActive(true);
            isUsingComputer = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void CloseComputer()
    {
        if (computerScreen != null)
        {
            computerScreen.SetActive(false);
            isUsingComputer = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (camControl != null)
                camControl.isLocked = false;

            ResetCamera();
        }
    }

    void ResetCamera()
    {
        if (cameraHolder == null) return;

        StopAllCoroutines();
        StartCoroutine(ResetCameraRoutine());
    }

    IEnumerator ResetCameraRoutine()
    {
        Vector3 startPos = cameraHolder.localPosition;
        Quaternion startRot = cameraHolder.localRotation;

        float elapsedTime = 0f;

        while (elapsedTime < cameraMoveDuration)
        {
            float t = Mathf.SmoothStep(0, 1, elapsedTime / cameraMoveDuration);

            cameraHolder.localPosition = Vector3.Lerp(startPos, originalHolderPosition, t);
            cameraHolder.localRotation = Quaternion.Slerp(startRot, originalHolderRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraHolder.localPosition = originalHolderPosition;
        cameraHolder.localRotation = originalHolderRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ComputerArea"))
            canSit = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ComputerArea"))
        {
            canSit = false;

            if (isUsingComputer)
                CloseComputer();
        }
    }
}