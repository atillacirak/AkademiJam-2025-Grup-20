using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mermi : MonoBehaviour
{
    public int hiz = 5;
    public GameObject mermii;
    AudioClip ses;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject mermi = Instantiate(mermii);
            Rigidbody rb = mermi.AddComponent<Rigidbody>();
            mermi.transform.position = gameObject.transform.position;
            mermi.transform.rotation = gameObject.transform.rotation;
            mermi.tag = "mermi";
            rb.AddForce(transform.forward * hiz, ForceMode.Impulse);

            AudioSource audio = mermi.AddComponent<AudioSource>();
            audio.clip = ses;
            audio.Play();

            patlatma asd = mermi.AddComponent<patlatma>();

            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
    }
}
