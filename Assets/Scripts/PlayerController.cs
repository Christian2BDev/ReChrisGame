using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static GameObject playerReference;

    [SerializeField]
    private float accelerationMultiplier = 0.01f;

    [SerializeField]
    private float decelerationMultiplier = 0.3f;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    float smoothTime = 0.2f;

    [SerializeField]
    float maxAngularVelocity = 45f;

    [SerializeField]
    float maxVelocity = 1f;
    [SerializeField]
    float minVelocity = -0.3f;

    float velocity = 0;
    // Start is called before the first frame update
    void Awake()
    {
        playerReference = this.gameObject;
    }

    void FixedUpdate()
    {
        // Get target velocity based on player input
        Vector2 movementVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (movementVec == Vector2.zero) {
            rb.angularVelocity = 0;
            return;
        }
        float targetAngle = Mathf.Atan2(movementVec.y, movementVec.x) * Mathf.Rad2Deg;
        targetAngle -= 90;

        float curAngle = transform.eulerAngles.z;

        float anglediff = Mathf.DeltaAngle(curAngle, targetAngle);

        float desiredAngularVelocity = Mathf.Clamp(anglediff, -maxAngularVelocity, maxAngularVelocity);

        rb.angularVelocity = desiredAngularVelocity;

        //Vector2 curDirection = AngleToVector(curAngle + 90);
        //Vector2 deltaDir = movementVec - curDirection;
        //float change = Mathf.Abs(deltaDir.x) + Mathf.Abs(deltaDir.y);
        //velocity += 1 / change * accelerationMultiplier;
        //Debug.Log(deltaDir);

        if (Mathf.Abs(anglediff) < 150)
        {
            velocity += (1 - (Mathf.Abs(anglediff) / 180)) * accelerationMultiplier;
        }
        else {
            velocity -= (1 - (Mathf.Abs(anglediff) / 180)) * decelerationMultiplier;
        }
        velocity = Mathf.Clamp(velocity, minVelocity, maxVelocity);
        Debug.Log(anglediff);

        // Smoothly interpolate between current velocity and target velocity
        rb.velocity = AngleToVector(curAngle + 90).normalized * velocity;
    }

    Vector2 AngleToVector(float angleDeg) {
        float rad = angleDeg * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
}
