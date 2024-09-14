using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    List<Transform> cannonPointsLeft;

    [SerializeField]
    List<Transform> cannonPointsRight;

    [SerializeField]
    PolygonCollider2D shipCollider;

    int lastCannonUsedRight = 0;
    int lastCannonUsedLeft = 1;

    [SerializeField]
    GameObject cannonBall;

    [SerializeField]
    float maxShootDistance = 3;

    [SerializeField]
    Rigidbody2D rb;

    float timeToShoot;
    float passedTime;

    private void Start()
    {
        cannonPointsLeft = GetChildrenWithName(transform, "CannonPointLeft");
        cannonPointsRight = GetChildrenWithName(transform, "CannonPointRight");
    }
    private void Update()
    {
        passedTime += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space) && !Dock.docked)
        {
            Vector3 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Mouse is over the player, dont shoot a cannonball.
            if (shipCollider.OverlapPoint(endPos))
            {
                return;
            }
            Vector2 endPosDirection = endPos - transform.position;
            float endPosAngle = Mathf.Atan2(endPosDirection.y, endPosDirection.x) * Mathf.Rad2Deg;
            float boatAngle = transform.eulerAngles.z;
            if(boatAngle > 180)
            {
                boatAngle = -(360 - boatAngle);
            }
            float boatEndPosAngleDelta = endPosAngle - boatAngle;
            Vector2 useCannonPoint;
            if (boatEndPosAngleDelta > 90 || boatEndPosAngleDelta < -120)
            {
                lastCannonUsedLeft++;
                if (lastCannonUsedLeft > cannonPointsLeft.Count - 1)
                {
                    lastCannonUsedLeft = 0;
                }
                useCannonPoint = cannonPointsLeft[lastCannonUsedLeft].position;
            }
            else if (boatEndPosAngleDelta < 90 && boatEndPosAngleDelta > -60)
            {
                lastCannonUsedRight++;
                if (lastCannonUsedRight > cannonPointsRight.Count - 1)
                {
                    lastCannonUsedRight = 0;
                }
                useCannonPoint = cannonPointsRight[lastCannonUsedRight].position;
            }
            else
            {
                return;
            }


            GameObject cannonBallClone = Instantiate(cannonBall);
            Vector2 controlPoint = endPos;
            cannonBallClone.GetComponent<CannonBallBehaviour>().StartCannonBallRoute(useCannonPoint, endPos, controlPoint, Vector2.Distance(transform.position, endPos) / 3, true);
        }
    }

    Vector2 AngleToVector(float angleDeg)
    {
        float rad = angleDeg * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    List<Transform> GetChildrenWithName(Transform parent, string name)
    {
        List<Transform> matchingChildren = new List<Transform>();

        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                matchingChildren.Add(child);
            }

            // Recursive call for grandchildren and deeper children
            matchingChildren.AddRange(GetChildrenWithName(child, name));
        }

        return matchingChildren;
    }
}
