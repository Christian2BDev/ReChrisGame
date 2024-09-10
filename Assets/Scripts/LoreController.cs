using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreController : MonoBehaviour
{
    /*
        0 = typing page 1
        1 = finsihed page 1
        2 = typing page 2
        3 finished page 2
     */

    public static int loreState = 0;
    private GameObject c1;
    private GameObject c2;


    void Start()
    {
        c1 = transform.GetChild(0).gameObject;
        c2 = transform.GetChild(1).gameObject;
    }
    void Update()
    {
        Debug.Log(loreState);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            switch (loreState) {
                case 0:c1.GetComponent<typewriter>().StopAndComplete(); break;
                case 1:
                    c1.SetActive(false);
                    c2.SetActive(true);
                    break;
                case 2: c2.GetComponent<typewriter>().StopAndComplete(); break;
                case 3:gameObject.SetActive(false); break;

            }
            loreState++;


        }
    }
}
