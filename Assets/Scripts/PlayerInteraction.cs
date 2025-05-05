using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform sitPoint;
    public KeyCode interactKey = KeyCode.E;
    public float interactDistance = 2f;
    private Rigidbody rb;

    private bool canSit = false;
    private Transform player;
    void Start()
    {
        player = this.transform;
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (canSit && Input.GetKeyDown(interactKey))
        {
            SitDown();
        }
    }

    void SitDown()
    {
        player.position = sitPoint.position;
        player.rotation = sitPoint.rotation;
        rb.freezeRotation = true; // Donduruldu
        rb.constraints = RigidbodyConstraints.FreezeAll; // T�m hareketleri donduruldu
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Debug.Log("Oturuldu.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ComputerArea")) // Koltu�un �n�ndeki trigger alan
            canSit = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ComputerArea"))
            canSit = false;
    }
}
