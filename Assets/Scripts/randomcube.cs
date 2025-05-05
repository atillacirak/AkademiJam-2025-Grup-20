using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class randomcube : MonoBehaviour
{
    /* Start is called before the first frame update
    public GameObject cubee;
    void Cubeat()
    {
        float x = Random.Range(-30f, 30f);
        float y = Random.Range(0f, 20f);
        float z = Random.Range(-30f, 30f);

        Vector3 randomPosition = new Vector3(x, y, z);

        Color randomColor = new Color(Random.value, Random.value, Random.value);

        GameObject cube = Instantiate(cubee, randomPosition, Quaternion.identity);

        cube.GetComponent<Renderer>().material.color = randomColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cubeat();
        }
    }  */

    public int sayi;
    public GameObject Dusman;
    void Start()
    {
        for (int i = 0; i < sayi; i++)
        {
            float x = Random.Range(-30f, 30f);
            float y = Random.Range(0f, 20f);
            float z = Random.Range(-30f, 30f);



            GameObject cube = Instantiate(Dusman);
            cube.name = "Küp" + (i + 1);

            patlatma asd = cube.AddComponent<patlatma>();
            Vector3 vek = new Vector3(x, y, z);
            cube.transform.position = vek;
            cube.tag = "hedef";

        }
    }
}
