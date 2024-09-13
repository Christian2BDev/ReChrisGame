using UnityEngine;
using UnityEngine.Tilemaps;

public class DockedPlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public GameObject boat;
    public Animator anim;
    public Camera cam;

    [SerializeField] float moveLimiter = 0.7f;
    [SerializeField] float Speed = 20.0f;
    [SerializeField] bool canHarvest;
    [SerializeField] TilemapCollider2D landCollider;
    [SerializeField] float HarvestRange = 2f;
    public LayerMask ask;

    float horizontal;
    float vertical;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 99999999,ask);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Item") && Vector2.Distance(hit.collider.gameObject.transform.position, transform.position) <= HarvestRange)
            {
                hit.collider.gameObject.GetComponent<ItemCollector>().Damage(1);
            }

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
            
        }
    }


}
