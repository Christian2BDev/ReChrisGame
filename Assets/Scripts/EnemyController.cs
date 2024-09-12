using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    List<Transform> cannonPoints;

    [SerializeField]
    GameObject cannonBall;

    [SerializeField]
    float maxShootDistance = 2;

    [SerializeField]
    float health = 25;

    [SerializeField]
    float speed = 0.7f;

    [SerializeField]
    float stopDistance = 0.1f;

    [SerializeField]
    float newPickMoveDistance = 1f;

    GameObject player;

    [SerializeField]
    Vector2 targetMovePos;

    Vector2 playerPosAtPosPickedMoment;

    [SerializeField]
    NavMeshAgent agent;

    [SerializeField]
    int maxCannonsPerSecond = 1;

    int cannonsShotLastSecond;

    float passedTime;

    private void Start()
    {
        //agent.updateRotation = false;
        cannonPoints = GetChildrenWithName(transform, "CannonPoint");
        GetPlayerReference();
        PickTargetMovePos();
    }

    private void Update()
    {
        //Vector2 agentDir = agent.
        //float angle = Mathf.Atan2(agent.velocity.y, agent.velocity.x) * Mathf.Rad2Deg - 90;
        //Debug.Log(angle);
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, Mathf.Lerp(agent.velocity.x, transform.position.x, Time.deltaTime) - 90));
        passedTime += Time.deltaTime;
        if(passedTime > 1)
        {
            passedTime = 0;
            cannonsShotLastSecond = 0;
        }

        if(cannonsShotLastSecond >= maxCannonsPerSecond)
        {
            return;
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
        if (maxCannonsPerSecond * passedTime - 1 < cannonsShotLastSecond)
        {
            GameObject cannonBallClone = Instantiate(cannonBall);
            Vector2 controlPoint = new Vector2(player.transform.position.x, player.transform.position.y + 1);
            cannonBallClone.gameObject.GetComponent<CannonBallBehaviour>().StartCannonBallRoute(transform.position, player.transform.position, controlPoint, hit.distance);
            cannonsShotLastSecond += 1;
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
            Vector2 moveDirectionVector = player.GetComponent<Rigidbody2D>().velocity.normalized;
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
        health -= amount;
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
