using UnityEngine;
using UnityEngine.UI; // Eğer eski UI Text kullanıyorsanız bunu ekleyin
using TMPro; // TextMeshPro kullanıyorsanız bunu ekleyin
using UnityEngine.Events; // Zaman dolduğunda olay tetiklemek için

public class CountdownTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startTime = 60f; // Geri sayımın başlayacağı süre (saniye cinsinden)
    private float currentTime; // Mevcut süre

    [Header("UI References")]
    // Kullandığınız Text türüne göre aşağıdaki satırlardan BİRİNİ aktif bırakın:
    // public Text countdownText; // Eski UI Text için
    public TextMeshProUGUI countdownText; // TextMeshPro için

    [Header("Timer Events")]
    public UnityEvent onTimerEnd; // Süre bittiğinde tetiklenecek olay

    private bool timerIsRunning = false;

    void Start()
    {
        // Başlangıçta süreyi ayarla
        currentTime = startTime;
        UpdateTimerDisplay(currentTime);
        // Sayacı başlatmak için StartTimer() çağrılabilir veya başka bir yerden tetiklenebilir
        StartTimer();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (currentTime > 0)
            {
                // Zamanı azalt
                currentTime -= Time.deltaTime;
                UpdateTimerDisplay(currentTime);
            }
            else
            {
                // Zaman doldu
                Debug.Log("Süre Doldu!");
                currentTime = 0;
                timerIsRunning = false;
                UpdateTimerDisplay(currentTime);
                // Süre bittiğinde tanımlanan olayları tetikle
                onTimerEnd.Invoke();
            }
        }
    }

    // Sayacı başlatan fonksiyon
    public void StartTimer()
    {
        timerIsRunning = true;
        currentTime = startTime; // İsterseniz her başladığında başlangıç süresine dönsün
    }

    // Sayacı durduran fonksiyon (isteğe bağlı)
    public void StopTimer()
    {
        timerIsRunning = false;
    }

    // Zamanı ekranda güncelleyen fonksiyon
    void UpdateTimerDisplay(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        // Zamanı dakika ve saniye olarak formatla
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Metni güncelle (Örn: 01:30)
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}