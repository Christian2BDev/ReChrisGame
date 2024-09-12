using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;

public class Dock : MonoBehaviour
{
    public static bool docked =false;
    public bool dockingPossible = false;
    public GameObject player;
    public BoxCollider2D playerCol;
    public PolygonCollider2D boatCol;
    
    Vector3 dockPosition;

    public void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.name.Equals("IslandLayer") && !docked)
        {
            dockPosition = collision.ClosestPoint(player.transform.position); 
            dockingPossible = true;
        }
        if (collision.gameObject == player && docked) dockingPossible = true;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals("IslandLayer") && !docked) dockingPossible = false;
        if (collision.gameObject == player && docked) dockingPossible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && dockingPossible)
        {
            if (!docked)
            {
                //Disable collisions between player and land
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Land"), true);
                playerCol.enabled = true;
                //boatCol.enabled = false;
                player.transform.position = dockPosition;
               
            }
            else {
                //Enable collisions between player and land
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Land"), false);
                player.transform.position = transform.position;
                playerCol.enabled = false;
                //boatCol.enabled = true;
            }
            docked = !docked;
        }
    }

}
