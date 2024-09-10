using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Rendering.DebugUI;

public class typewriter : MonoBehaviour
{
    public TMP_Text t1;
    public float delay = 360;
    public int target = 0;
    public float value = 0;
    public int i = 0;
    List<string> t1_list = new List<string>();



    void Start()
    {
        target = t1.text.Length;
        t1_list = t1.text.Split(',').ToList();
        t1.text = "";
        StartCoroutine(Typer());

       
    }


    IEnumerator Typer()
    {

        while (true)
        {
            yield return new WaitForSeconds(delay);
            if (i < target -1) {
           
            t1.text = t1.text + t1_list[i];
            i++;
                Debug.Log(i);
            }
            
        }



    }
}
