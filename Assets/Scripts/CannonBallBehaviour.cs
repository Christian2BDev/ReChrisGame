using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour
{
    //A start point
    //B end point
    //C control point
    //Bezier curve: P(t)=(1−t)^2*A+2(1−t)tC+t^2*B

    Vector2 beginPos, endPos, controlPos;
    float travelTime;
    float elapsedTime;
    public void StartCannonBallRoute(Vector2 _beginPos, Vector2 _endPos, Vector2 _controlPos, float _travelTime)
    {
        beginPos = _beginPos;
        controlPos = _controlPos;
        endPos = _endPos;
        travelTime = _travelTime;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        //time normalized between 0 and 1
        float t = elapsedTime / travelTime;
        //Dont overshoot, more noticable when player has low FPS.
        t = Mathf.Clamp01(t);

        // Calculate the position on the quadratic Bézier curve
        Vector2 newPosition = Mathf.Pow(1 - t, 2) * beginPos +
                              2 * (1 - t) * t * controlPos +
                              Mathf.Pow(t, 2) * endPos;

        transform.position = newPosition;

        if(t >= 1)
        {
            CannonBallStopped();
        }
    }

    private void CannonBallStopped()
    {
        PolygonCollider2D boatCollider = PlayerController.playerReference.GetComponent<PolygonCollider2D>();

        if (boatCollider.OverlapPoint(transform.position))
        {
            PlayerStats.ChangeHealth(-10);
        }
        Destroy(transform.gameObject);
    }
}
