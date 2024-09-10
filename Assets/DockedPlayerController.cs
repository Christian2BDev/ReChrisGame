using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockedPlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public GameObject boat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Dock.docked)
        {
            Vector2 movementVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime;
            rb.velocity += movementVec;
        }
        else {
            transform.position = boat.transform.position;
        }
       
    }
}
