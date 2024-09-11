using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DockedPlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public GameObject boat;
    [SerializeField]float moveLimiter = 0.7f;
    [SerializeField] float Speed = 20.0f;
    [SerializeField] Collider2D lastCollider;
    [SerializeField]  bool canHarvest;
    [SerializeField]  TilemapCollider2D landCollider;
    // Start is called before the first frame update

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Item"))
        {
            canHarvest = true;
            lastCollider = collision;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Item"))
        {
            canHarvest = false;
            lastCollider = null;
        }

    }

    private void Update()
    {
        if (canHarvest && Input.GetKeyDown(KeyCode.Mouse0))
        {
            lastCollider.gameObject.GetComponent<ItemCollector>().Damage(1);
        }

        if (Dock.docked)
        {
            if (!landCollider.OverlapPoint(transform.position))
            {
                Debug.Log("Not on colider");
                transform.position = landCollider.ClosestPoint(transform.position);
            }
        }
    }

    void FixedUpdate()
    {
        Movement(); 
    }


    void Movement()
    {
        if (Dock.docked)
        {
            Vector2 movementVec = new Vector2(Input.GetAxis("Horizontal") * moveLimiter, Input.GetAxis("Vertical") * moveLimiter) * Speed;
            rb.velocity += movementVec;
        }
        else
        {
            transform.position = boat.transform.position;
        }
    }


}
