using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonInteract : MonoBehaviour
{
    [Header("Camera Switching")]
    public Camera normalCamera;        // MainCamera
    public Camera screenCamera;        // Ekran i�i kamera
    public float camBlendTime = 0.8f;  // crossfade s�resi (�ki cam varsa, Lerp ile de yap�labilir)
    public GameObject computerScreen;
    public Transform sonPozisyon;
    public Transform PlayerUst;
    public Transform PlayerALt;

    [Header("Animation")]   
    public Animator playerAnimator;
    public string disappearTrigger = "ekranaCekil";

    [Header("UI")]
    public Button interactButton;      // Buton referans�

    void Start()
    {
        // Ba�lang��ta butona listener ekle
        if (interactButton != null)
            interactButton.onClick.AddListener(OnInteractButtonPressed);

        // Ekran kameras� kapal� olsun
        if (screenCamera != null)
            screenCamera.enabled = false;
    }

    public void OnInteractButtonPressed()
    {
        // Start the whole sequence
        computerScreen.SetActive(false);
        StartCoroutine(DoScreenSuckSequence());
    }

    IEnumerator DoScreenSuckSequence()
    {

        PlayerALt.rotation = sonPozisyon.rotation;
        // 1) Kamera de�i�tirme
        if (normalCamera != null && screenCamera != null)
        {
            // Basit crossfade: �nce yeni kameray� a�, sonra eskiyi kapat
            screenCamera.enabled = true;
            float elapsed = 0f;
            // (�stersen post-processing blend veya Cinemachine blend kullanabilirsin)
            while (elapsed < camBlendTime)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }
            normalCamera.enabled = false;
        }

        // 2) Karakter animasyonunu tetikle
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger(disappearTrigger);
        }

        // 3) Animasyonun biti�ini bekle (opsiyonel)
        // float animLength = playerAnimator.GetCurrentAnimatorStateInfo(0).length;
        // yield return new WaitForSeconds(animLength);

        // 4) �leride ba�ka bir i�in varsa buraya ekle
    }
}
