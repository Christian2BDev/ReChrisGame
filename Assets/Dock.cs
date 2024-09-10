using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dock : MonoBehaviour
{
    public static bool docked =false;
    public bool dockingPossible = false;
    public GameObject boat;
    Vector3 dockPosition;

    public void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.name.Equals("IslandLayer") && !docked)
        {
            dockPosition = collision.ClosestPoint(boat.transform.position); 
            dockingPossible = true;
        }
        if (collision.name.Equals("player") && docked) dockingPossible = true;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals("IslandLayer") && !docked) dockingPossible = false;
        if (collision.name.Equals("player") && docked) dockingPossible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && dockingPossible)
        {
            if (!docked)
            {
                transform.position = dockPosition;
            }
            else {
                transform.position = boat.transform.position;
            }
            docked = !docked;
            Debug.Log("Dock");

        }
    }

}
