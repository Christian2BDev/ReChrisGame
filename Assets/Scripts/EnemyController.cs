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
    Rigidbody2D rb;

    [SerializeField]
    float speed;

    [SerializeField]
    float stopDistance = 0.1f;

    [SerializeField]
    float newPickMoveDistance = 4f;

    GameObject player;

    [SerializeField]
    Vector2 targetMovePos;

    Vector2 playerPosAtPosPickedMoment;

    [SerializeField]
    NavMeshAgent agent;

    NavMeshSurface surface;

    private void Start()
    {
        GetPlayerReference();
        PickTargetMovePos();
    }

    void GetPlayerReference()
    {
        player = PlayerController.playerReference;
    }

    void PickTargetMovePos(bool pickPlayerCentre = false) {
        if (player == null)
        {
            GetPlayerReference();
        }
        float randomXDeviation = Random.Range(-3.5f, 3.5f);
        float randomyDeviation = Random.Range(-3.5f, 3.5f);
        //TODO: pick a point at the side of the boat instead of trying to crash into the player
        if(pickPlayerCentre)
        {
            targetMovePos = player.transform.position;
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
}
