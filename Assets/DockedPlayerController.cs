using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockedPlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public GameObject boat;
    [SerializeField]float moveLimiter = 0.7f;
    [SerializeField] float Speed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Dock.docked)
        {
            Vector2 movementVec = new Vector2(Input.GetAxis("Horizontal") * moveLimiter, Input.GetAxis("Vertical") * moveLimiter) * Speed;
            rb.velocity += movementVec;
        }
        else {
            transform.position = boat.transform.position;
        }
       
    }
}
