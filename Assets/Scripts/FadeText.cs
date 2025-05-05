using UnityEngine;
using System.Collections;
using TMPro; // TextMeshPro i�in
using UnityEngine.UI; // Standart UI Text i�in

public class FadeText : MonoBehaviour
{
    public float fadeInDuration = 1f; // Fade-in s�resi (saniye)
    public float fadeOutDuration = 1f; // Fade-out s�resi (saniye)
    public float displayTime = 3f; // Ekranda kalma s�resi (saniye)
    public bool isGameStarted = false; // Oyun ba�lad� m�?

    private TextMeshProUGUI tmpText; // TextMeshPro i�in
    private Text uiText; // Standart UI Text i�in

    private void Start()
    {
        // Componentleri bul
        tmpText = GetComponent<TextMeshProUGUI>();
        uiText = GetComponent<Text>();

        // Ba�lang��ta fade-in yap
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        // 1) Fade-In (Yava��a g�r�n�r ol)
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

        // 3) Fade-Out (Yava��a kaybol)
        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            SetTextAlpha(alpha);
            yield return null;
        }

        gameObject.SetActive(false);
        isGameStarted = true; // Oyun ba�lad� olarak i�aretle

        // �sterseniz burada metni tamamen kald�rabilirsiniz:
        // gameObject.SetActive(false);
    }

    // Metnin alpha de�erini ayarla
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