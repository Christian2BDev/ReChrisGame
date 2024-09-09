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

    [SerializeField]
    float smoothTime = 0.2f;
    // Start is called before the first frame update
    void Awake()
    {
        playerReference = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity != Vector2.zero)
        {
            // Get the angle in degrees based on the velocity direction
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

            // Subtract 90 degrees to make the top of the sprite face the direction of movement
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }

    void FixedUpdate()
    {
        // Get target velocity based on player input
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * speed;

        // Smoothly interpolate between current velocity and target velocity
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, smoothTime);
    }
}
