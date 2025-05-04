using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartupDialog : MonoBehaviour
{
    public GameObject dialogPanel; // Inspector'dan atanacak
    public TextMeshProUGUI dialogText; // Inspector'dan atanacak
    public float displayTime = 3f; // Otomatik geçiþ süresi (isteðe baðlý)
    public bool waitForClick = true; // Týklamayla geçilsin mi?
    public bool isGameStarted = false; // Oyun baþladý mý?

    void Start()
    {


        if (!waitForClick)
        {
            // Belirli bir süre sonra otomatik kapat
            Invoke("StartGame", displayTime);
        }
    }

    void Update()
    {
        // Eðer týklamayla geçiþ aktifse ve herhangi bir týklama algýlandýysa
        if (waitForClick && Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        // Diyalog panelini kapat
        dialogPanel.SetActive(false);

        // Burada oyunun gerçek baþlangýç fonksiyonlarýný çaðýrabilirsin
        isGameStarted = true;
        Debug.Log("Oyun baþladý!");
    }
}