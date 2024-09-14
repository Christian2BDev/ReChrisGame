using UnityEngine;

public class Dock : MonoBehaviour
{
    public static bool docked =false;
    public bool dockingPossible = false;
    public GameObject player;
    public BoxCollider2D playerCol;
    public PolygonCollider2D boatCol;

    [SerializeField] BoxCollider2D[] worldBorderColliders;
    
    Vector3 dockPosition;
    [SerializeField]
    Rigidbody2D playerRigidBody;

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
                foreach(BoxCollider2D col in worldBorderColliders)
                {
                    if(col.OverlapPoint(dockPosition))
                    {
                        return;
                    }
                }
                playerRigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
                //Disable collisions between player and land
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Land"), true);
                playerCol.enabled = true;
                player.transform.position = dockPosition;

            }
            else {
                playerRigidBody.constraints = RigidbodyConstraints2D.None;
                //Enable collisions between player and land
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Land"), false);
                player.transform.position = transform.position;
                playerCol.enabled = false;
                //boatCol.enabled = true;
            }
            docked = !docked;
            Camera.main.transform.GetComponent<SoundManager>().PlayDock();
        }
    }
}
