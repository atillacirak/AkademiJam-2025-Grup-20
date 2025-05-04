using UnityEngine;
using System.Collections;
using TMPro; // TextMeshPro için
using UnityEngine.UI; // Standart UI Text için

public class FadeText : MonoBehaviour
{
    public float fadeInDuration = 1f; // Fade-in süresi (saniye)
    public float fadeOutDuration = 1f; // Fade-out süresi (saniye)
    public float displayTime = 3f; // Ekranda kalma süresi (saniye)
    public bool isGameStarted = false; // Oyun baþladý mý?

    private TextMeshProUGUI tmpText; // TextMeshPro için
    private Text uiText; // Standart UI Text için

    private void Start()
    {
        // Componentleri bul
        tmpText = GetComponent<TextMeshProUGUI>();
        uiText = GetComponent<Text>();

        // Baþlangýçta fade-in yap
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        // 1) Fade-In (Yavaþça görünür ol)
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            SetTextAlpha(alpha);
            yield return null;
        }

        // 2) 3 Saniye Bekle
        yield return new WaitForSeconds(displayTime);

        // 3) Fade-Out (Yavaþça kaybol)
        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            SetTextAlpha(alpha);
            yield return null;
        }

        gameObject.SetActive(false);
        isGameStarted = true; // Oyun baþladý olarak iþaretle

        // Ýsterseniz burada metni tamamen kaldýrabilirsiniz:
        // gameObject.SetActive(false);
    }

    // Metnin alpha deðerini ayarla
    private void SetTextAlpha(float alpha)
    {
        if (tmpText != null)
        {
            Color newColor = tmpText.color;
            newColor.a = alpha;
            tmpText.color = newColor;
        }
        else if (uiText != null)
        {
            Color newColor = uiText.color;
            newColor.a = alpha;
            uiText.color = newColor;
        }


    }


}