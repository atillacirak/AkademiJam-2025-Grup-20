using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonInteract : MonoBehaviour
{
    [Header("Camera Switching")]
    public Camera normalCamera;        // MainCamera
    public Camera screenCamera;        // Ekran içi kamera
    public float camBlendTime = 0.8f;  // crossfade süresi (Ýki cam varsa, Lerp ile de yapýlabilir)
    public GameObject computerScreen;
    public Transform sonPozisyon;
    public Transform PlayerUst;
    public Transform PlayerALt;

    [Header("Animation")]   
    public Animator playerAnimator;
    public string disappearTrigger = "ekranaCekil";

    [Header("UI")]
    public Button interactButton;      // Buton referansý

    void Start()
    {
        // Baþlangýçta butona listener ekle
        if (interactButton != null)
            interactButton.onClick.AddListener(OnInteractButtonPressed);

        // Ekran kamerasý kapalý olsun
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
        // 1) Kamera deðiþtirme
        if (normalCamera != null && screenCamera != null)
        {
            // Basit crossfade: önce yeni kamerayý aç, sonra eskiyi kapat
            screenCamera.enabled = true;
            float elapsed = 0f;
            // (Ýstersen post-processing blend veya Cinemachine blend kullanabilirsin)
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

        // 3) Animasyonun bitiþini bekle (opsiyonel)
        // float animLength = playerAnimator.GetCurrentAnimatorStateInfo(0).length;
        // yield return new WaitForSeconds(animLength);

        // 4) Ýleride baþka bir iþin varsa buraya ekle
    }
}
