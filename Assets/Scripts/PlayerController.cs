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
    float angularVelocity = 30f;

    [SerializeField]
    float maxVelocity = 1f;
    [SerializeField]
    float minVelocity = -0.3f;

    float velocity = 0;

    void Awake()
    {
        playerReference = this.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            PlayerStats.ChangeHealth(-10);
            Vector2 enemyVelocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            Vector2 deltaVelocity = enemyVelocity + rb.velocity;
            collision.gameObject.GetComponent<EnemyController>().ChangeHealthAmount(-10);
        }
        else if (collision.gameObject.CompareTag("Land"))
        {
            //TODO: take speed and direction into account
            PlayerStats.ChangeHealth(-10);
        }
    }

    private void FixedUpdate()
    {
        if (!Dock.docked) {
            Vector2 movementVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

            float curAngle = transform.eulerAngles.z;
            Vector2 currentMovementVec = AngleToVector(curAngle + 90);
            velocity += accelerationMultiplier * movementVec.y;
            velocity = Mathf.Clamp(velocity, minVelocity, maxVelocity);
            rb.velocity = currentMovementVec * velocity;
            rb.angularVelocity = -movementVec.x * angularVelocity * velocity;
        }
    }

    Vector2 AngleToVector(float angleDeg)
    {
        float rad = angleDeg * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
}
