using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class typewriter : MonoBehaviour
{
    private TMP_Text t;
    private string text;
    public float delay = 360;
    public int target = 0;
    public float value = 0;
    public int i = 0;
    public int loreNr = 0;
    private Coroutine c;
    
    List<string> t1_list = new List<string>();



    void Start()
    {
        t = GetComponent<TMP_Text>();
        text = t.text;
        target = t.text.Length;
        t1_list = t.text.Select(c => c.ToString()).ToList();
        t.text = "";
       c = StartCoroutine(Typer());

       
    }

   

    IEnumerator Typer()
    {

        while (i < target -1)
        {
            yield return new WaitForSeconds(delay);
            
           
            t.text = t.text + t1_list[i];
            i++;
            
          
            
        }
        LoreController.loreState = loreNr;


    }


   public void StopAndComplete() {
        StopCoroutine(c);
        t.text = text;
    }
}
