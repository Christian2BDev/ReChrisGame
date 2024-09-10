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

        if (Mathf.Abs(anglediff) < 150)
        {
            velocity += (1 - (Mathf.Abs(anglediff) / 180)) * accelerationMultiplier;
        }
        else {
            velocity -= (1 - (Mathf.Abs(anglediff) / 180)) * decelerationMultiplier;
        }
        velocity = Mathf.Clamp(velocity, minVelocity, maxVelocity);
        //Debug.Log(anglediff);

        // Smoothly interpolate between current velocity and target velocity
        rb.velocity = AngleToVector(curAngle + 90).normalized * velocity;
    }

    Vector2 AngleToVector(float angleDeg) {
        float rad = angleDeg * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Enemy"))
        {
            PlayerStats.ChangeHealth(-10);
            Vector2 enemyVelocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            Vector2 deltaVelocity = enemyVelocity + rb.velocity;
            collision.gameObject.GetComponent<EnemyController>().ChangeHealthAmount(-10);
        }
        else if (collision.gameObject.tag.Equals("Land"))
        {
            //TODO: take speed and direction into account
            PlayerStats.ChangeHealth(-10);
        }
    }
}
