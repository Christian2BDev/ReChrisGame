using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class DockedPlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public GameObject boat;
    public Animator anim;

    [SerializeField]float moveLimiter = 0.7f;
    [SerializeField] float Speed = 20.0f;
    [SerializeField] Collider2D lastCollider;
    [SerializeField]  bool canHarvest;
    [SerializeField]  TilemapCollider2D landCollider;

    float horizontal;
    float vertical;
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
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            Vector2 movementVec = new Vector2(horizontal * moveLimiter,vertical * moveLimiter) * Speed;
            rb.velocity += movementVec;
            anim.SetFloat("X", horizontal);
            anim.SetFloat("Y", vertical);
           
        }
        else
        {
            transform.localPosition = new Vector3(0,-1,0);
            //transform.position = boat.transform.position;
        }
    }


}
