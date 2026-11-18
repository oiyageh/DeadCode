
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    [Header("Movement")]
    public NavMeshAgent agent;

    public float wanderRadius = 15f;
    public float waitTime = 5f;

    private float timer;

    void Start()
    {
        timer = waitTime;
        PickRandomPosition();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime && agent.remainingDistance <= 1f)
        {
            PickRandomPosition();
            timer = 0;
        }
    }

    void PickRandomPosition()
    {
        Vector3 randomDirection =
            Random.insideUnitSphere * wanderRadius;

        randomDirection += transform.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(
            randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
