using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static GameObject playerReference;

    private static float originalAccelerationMultiplier = 0.005f;
    private static float accelerationMultiplier = 0.005f;

    private static float originalDecelerationMultiplier = 0.08f;
    private static float decelerationMultiplier = 0.08f;

    [SerializeField]
    Rigidbody2D rb;

    static float originAlangularVelocity = 30f;
    static float angularVelocity = 30f;

    static float originalVelocity = 0.5f;
    static float maxVelocity = 0.5f;
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
            collision.gameObject.GetComponent<EnemyController>().ChangeHealthAmount(-10);
        }
        else if (collision.gameObject.CompareTag("Land"))
        {
            PlayerStats.ChangeHealth(-10);
        }
        Camera.main.transform.GetComponent<SoundManager>().PlayCrash();
    }

    private void FixedUpdate()
    {
        if (Dock.docked) {
            velocity = 0;
            rb.velocity = new Vector2(0, 0);
            rb.angularVelocity = 0;
            return;
        }
        
        Vector2 movementVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        float curAngle = transform.eulerAngles.z;
        Vector2 currentMovementVec = AngleToVector(curAngle + 90);
        float multiplier = currentMovementVec.y < 0 ? accelerationMultiplier : decelerationMultiplier;
        velocity += multiplier * movementVec.y;
        velocity = Mathf.Clamp(velocity, minVelocity, maxVelocity);
        if(movementVec.y == 0)
        {
            rb.velocity = rb.velocity / 2 * Time.fixedDeltaTime;
        }
        rb.velocity = currentMovementVec * velocity;
        rb.angularVelocity = -movementVec.x * angularVelocity * velocity;
    }

    Vector2 AngleToVector(float angleDeg)
    {
        float rad = angleDeg * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    public static void UpdateMaxVelocity() {
        maxVelocity = originalVelocity + (Upgrades.SpeedUpgrade - 1) * 0.2f;
        accelerationMultiplier = originalAccelerationMultiplier + (Upgrades.SpeedUpgrade - 1) * 0.002f;
        decelerationMultiplier = originalDecelerationMultiplier + (Upgrades.SpeedUpgrade - 1) * 0.002f;
    }

    public static void UpdateAngularVelocity()
    {
        angularVelocity = originAlangularVelocity + (Upgrades.RotationUpgrade-1) * 2;
    }
}
