﻿using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour
{
    //A start point
    //B end point
    //C control point
    //Bezier curve: P(t)=(1−t)^2*A+2(1−t)tC+t^2*B
    public static int orignalCannonBallDmg = 10;
    public static int CannonBallDmg = 10;


    Vector2 beginPos, endPos, controlPos;
    float travelTime;
    float elapsedTime;

    bool shotFromPlayer;
    public void StartCannonBallRoute(Vector2 _beginPos, Vector2 _endPos, Vector2 _controlPos, float _travelTime, bool playerShot = false)
    {
        beginPos = _beginPos;
        controlPos = _controlPos;
        endPos = _endPos;
        travelTime = _travelTime;
        shotFromPlayer = playerShot;
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
        SoundManager sounds;
        sounds = Camera.main.transform.GetComponent<SoundManager>();
        Collider2D endCollider;
        if(shotFromPlayer)
        {
            endCollider = Physics2D.OverlapPoint(transform.position);
            if(endCollider == null || !endCollider.gameObject.CompareTag("Enemy"))
            {
                if(endCollider != null && endCollider.CompareTag("Land"))
                {
                    sounds.PlayMissedBall(false);
                }
                else
                {
                    sounds.PlayMissedBall(true);
                }
                DestroyCannonBall();
                return;
            }
            endCollider.transform.GetComponent<EnemyController>().ChangeHealthAmount(-CannonBallDmg);
            sounds.PlayRandomExplosion();
        }
        else
        {
            endCollider = PlayerController.playerReference.GetComponent<PolygonCollider2D>();
            if (endCollider.OverlapPoint(transform.position))
            {
                PlayerStats.ChangeHealth(-10);
                sounds.PlayRandomExplosion();
            }
            else if(Physics2D.OverlapPoint(transform.position).CompareTag("Land"))
            {
                sounds.PlayMissedBall(false);
            }
            else
            {
                sounds.PlayMissedBall(true);
            }
        }

        DestroyCannonBall();
    }

    void DestroyCannonBall()
    {
        Destroy(transform.gameObject);
    }

    public static void UpdateCannonBallDmg()
    {
        CannonBallDmg = orignalCannonBallDmg + (Upgrades.DmgUpgrade-1)* 2;
    }
}
