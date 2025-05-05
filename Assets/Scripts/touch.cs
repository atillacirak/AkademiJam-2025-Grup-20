using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRÝGGER ENTER");
        Destroy(other.gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("TRÝGGER STAY");
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("TRÝGGER EXÝT");
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLÝSÝON ENTER");
        Destroy(collision.gameObject);
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("COLLÝSÝON STAY");
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("COLLÝSÝON EXÝT");
    }
}
