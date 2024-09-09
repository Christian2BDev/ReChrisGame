using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
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
        if(pickPlayerCentre)
        {
            targetMovePos = player.transform.position;
        }
        else
        {
            targetMovePos = new Vector2(player.transform.position.x + randomXDeviation, player.transform.position.y + randomyDeviation);
        }
        playerPosAtPosPickedMoment = player.transform.position;
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

        rb.velocity = ((Vector3)targetMovePos - transform.position).normalized * speed;
    }
}
