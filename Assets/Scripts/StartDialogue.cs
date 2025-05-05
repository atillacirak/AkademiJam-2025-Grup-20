using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartupDialog : MonoBehaviour
{
    public GameObject dialogPanel; // Inspector'dan atanacak
    public TextMeshProUGUI dialogText; // Inspector'dan atanacak
    public float displayTime = 3f; // Otomatik ge�i� s�resi (iste�e ba�l�)
    public bool waitForClick = true; // T�klamayla ge�ilsin mi?
    public bool isGameStarted = false; // Oyun ba�lad� m�?

    void Start()
    {


        if (!waitForClick)
        {
            // Belirli bir s�re sonra otomatik kapat
            Invoke("StartGame", displayTime);
        }
    }

    void Update()
    {
        // E�er t�klamayla ge�i� aktifse ve herhangi bir t�klama alg�land�ysa
        if (waitForClick && Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        // Diyalog panelini kapat
        dialogPanel.SetActive(false);

        // Burada oyunun ger�ek ba�lang�� fonksiyonlar�n� �a��rabilirsin
        isGameStarted = true;
        Debug.Log("Oyun ba�lad�!");
    }
}