using UnityEngine;

public class CamControl : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public bool isLocked = false;
    
    public Transform orientation;
    public Transform character;

    float xRotation;
    float yRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Mouse'u kilitler.
        Cursor.visible = false; // Mouse'u görünmez yapar.
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked) return;
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX; // Mouse'un X eksenindeki hareketini alýr
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY; // Mouse'un Y eksenindeki hareketini alýr

            yRotation += mouseX;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // X rotasyonunu -90 ile 90 arasýnda sýnýrlar

            //rotate camera and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); // Transform'un rotasyonunu ayarlar
            orientation.rotation = Quaternion.Euler(0, yRotation, 0); // Orientation'un rotasyonunu ayarlar

            //rotate character
            character.rotation = Quaternion.Euler(0, yRotation, 0); // Karakterin rotasyonunu ayarlar
        }

    }
}
