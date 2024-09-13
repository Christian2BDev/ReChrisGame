using UnityEngine;
using UnityEngine.AI;

public class PlayerBoatAgentController : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    float agentStuckDistance = 1f;

    private void Update()
    {
        SetAgentPos();
        //Toggle the agent off, set the position and toggle it on again if the agent got stuck.
        if (Vector3.Distance(agent.transform.localPosition, Vector3.zero) > agentStuckDistance)
        {
            agent.enabled = false;
            SetAgentPos();
            agent.enabled = true;
        }
    }

    void SetAgentPos()
    {
        agent.transform.position = transform.parent.position;
    }
}
