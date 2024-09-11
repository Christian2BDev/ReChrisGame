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
    public TilemapCollider2D seaCol;
    public TilemapCollider2D islandCol;
    public PolygonCollider2D boatCol;
    Vector3 dockPosition;

    public void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.name.Equals("IslandLayer") && !docked)
        {
            dockPosition = collision.ClosestPoint(player.transform.position); 
            dockingPossible = true;
        }
        if (collision.name.Equals("playerBack") && docked) dockingPossible = true;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals("IslandLayer") && !docked) dockingPossible = false;
        if (collision.name.Equals("playerBack") && docked) dockingPossible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && dockingPossible)
        {
            if (!docked)
            {
                playerCol.enabled = true;
                seaCol.enabled = true;
                islandCol.enabled = false;
                boatCol.enabled = false;
                player.transform.position = dockPosition;
            }
            else {
                player.transform.position = transform.position;
                playerCol.enabled = false;
                seaCol.enabled = false;
                islandCol.enabled = true;
                boatCol.enabled = true;
            }
            docked = !docked;
            Debug.Log("Dock");

        }
    }

}
