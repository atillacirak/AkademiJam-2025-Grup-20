using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class score : MonoBehaviour
{
    public TMP_Text skorsayaci;
    int skor = 0;

    public void Skor()
    {
        skor++;
        skorsayaci.text = skor.ToString(); 
    }
}
