using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static GameObject playerReference;

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        playerReference = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
 
       //transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0) * speed * Time.deltaTime);
    }

     void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
        
    }
}
