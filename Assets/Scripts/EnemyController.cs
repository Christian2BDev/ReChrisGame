using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    List<Transform> cannonPointsLeft;

    [SerializeField]
    List<Transform> cannonPointsRight;

    int lastCannonUsedRight = 0;
    int lastCannonUsedLeft = 1;

    [SerializeField]
    GameObject cannonBall;

    [SerializeField]
    PolygonCollider2D boatCollider;

    [SerializeField]
    float maxShootDistance = 2;

    [SerializeField]
    float maxHealth = 25;

    [SerializeField]
    float health = 25;

    [SerializeField]
    float speed = 0.7f;

    [SerializeField]
    float stopDistance = 0.1f;

    [SerializeField]
    float newPickMoveDistance = 2f;

    GameObject player;

    [SerializeField]
    Vector2 targetMovePos;

    Vector2 playerPosAtPosPickedMoment;

    [SerializeField]
    NavMeshAgent agent;

    [SerializeField]
    Transform healthBarFill;

    [SerializeField]
    float minDelayBetweenShots = 1;
    float timeToShoot;

    float passedTime;

    private void Start()
    {
        cannonPointsLeft = GetChildrenWithName(transform, "CannonPointLeft");
        cannonPointsRight = GetChildrenWithName(transform, "CannonPointRight");
        GetPlayerReference();
        PickTargetMovePos();
    }

    private void Update()
    {
        //Code used to shoot cannonballs from the enemy side
        passedTime += Time.deltaTime;
        if(timeToShoot == -1)
        {
            passedTime = 0;
            timeToShoot = Random.Range(minDelayBetweenShots, minDelayBetweenShots * 4);
        }

        if(Vector2.Distance(transform.position, player.transform.position) > maxShootDistance)
        {
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, maxShootDistance, LayerMask.GetMask("Player"));
        if (hit.collider == null)
        {
            return;
        }
        if (passedTime >= timeToShoot)
        {
            float beginEndDistance = Vector2.Distance(player.transform.position, transform.position);
            Vector2 playerDirection = (player.transform.position - transform.position);
            //check if the player is at the side of the turrets
            float angleToPlayer = Vector2.SignedAngle(agent.velocity.normalized, playerDirection);
            Vector2 useCannonPoint;
            if(angleToPlayer > 30 && angleToPlayer < 100)
            {
                lastCannonUsedLeft++;
                if(lastCannonUsedLeft > cannonPointsLeft.Count - 1)
                {
                    lastCannonUsedLeft = 0;
                }
                useCannonPoint = cannonPointsLeft[lastCannonUsedLeft].position;
            }
            else if (angleToPlayer < -30 && angleToPlayer > -100)
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
            float randomXdeviation = Random.Range(-beginEndDistance / 2, beginEndDistance / 2);
            float randomYdeviation = Random.Range(-beginEndDistance / 2, beginEndDistance / 2);
            Vector2 endPos = new Vector2(player.transform.position.x + randomXdeviation, player.transform.position.y + randomYdeviation);
            Vector2 controlPoint = endPos;
            //Set the end point to the player's transform if the endPos happens to be on the enemy itself
            if (boatCollider.OverlapPoint(endPos))
            {
                endPos = player.transform.position;
            }
            cannonBallClone.GetComponent<CannonBallBehaviour>().StartCannonBallRoute(useCannonPoint, endPos, controlPoint, Vector2.Distance(transform.position, endPos));
            timeToShoot = -1;
            passedTime = 0;
        }
    }

    void GetPlayerReference()
    {
        player = PlayerController.playerReference;
    }

    void PickTargetMovePos(bool pickPlayerSide = false) {
        if (player == null)
        {
            GetPlayerReference();
        }
        float randomXDeviation = Random.Range(-3.5f, 3.5f);
        float randomyDeviation = Random.Range(-3.5f, 3.5f);
        //TODO: pick a point at the side of the boat instead of trying to crash into the player
        if(pickPlayerSide)
        {
            float moveDirection = player.transform.eulerAngles.z + 90;
            moveDirection = Random.Range(0, 2) == 0 ? moveDirection + 90 : moveDirection - 90;
            targetMovePos = (Vector2)player.transform.position + AngleToVector(moveDirection);
        }
        else
        {
            targetMovePos = new Vector2(player.transform.position.x + randomXDeviation, player.transform.position.y + randomyDeviation);
        }
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetMovePos, out hit, 2f, NavMesh.AllAreas))
        {
            targetMovePos = hit.position;
        }
        playerPosAtPosPickedMoment = player.transform.position;
        agent.SetDestination(targetMovePos);
    }

    private void FixedUpdate()
    {
        if(player == null)
        {
            GetPlayerReference();
            PickTargetMovePos();
            return;
        }

        float distance = Vector3.Distance(targetMovePos, transform.position);
        float playerMovedDistance = Vector3.Distance(playerPosAtPosPickedMoment, player.transform.position);

        if (playerMovedDistance > newPickMoveDistance)
        {
            //Player is moving, just go to the centre of the players boat.
            PickTargetMovePos(true);
        }

        if (distance < stopDistance)
        {
            PickTargetMovePos();
        }
    }

    public void ChangeHealthAmount(float amount)
    {
        health += amount;
        if(health <= 0)
        {
            Camera.main.transform.GetComponent<SoundManager>().PlayBoatSink();
            Destroy(transform.parent.gameObject);
            Inventory.ChangeItemAmount(Inventory.Materials.gold, Random.Range(1, 5));
        }

        //+ 0.000001f so we never devide by zero, it should never be 0, but just in case
        healthBarFill.localScale = new Vector3((health + 0.000001f) / maxHealth, healthBarFill.localScale.y, healthBarFill.localScale.z);
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
