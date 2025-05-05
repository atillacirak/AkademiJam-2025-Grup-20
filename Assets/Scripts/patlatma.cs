using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class patlatma : MonoBehaviour
{
private void OnTriggerEnter(Collider other)
{
if (other.gameObject.tag == "hedef")
{
Destroy(other.gameObject);
Destroy(gameObject);
            GameObject go = GameObject.Find("Canvas");
        score asd = go.GetComponent<score>();
        asd.Skor();
    }
}
}