using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
  
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Dock.docked && gameObject.name.Equals("UpgradeCanvas"))
        {

            gameObject.transform.GetChild(0).gameObject.SetActive(!gameObject.transform.GetChild(0).gameObject.activeSelf);
        } 
    }
}
