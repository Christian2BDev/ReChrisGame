using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
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

    private void Start()
    {
        GetPlayerReference();
        PickTargetMovePos();
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
}
