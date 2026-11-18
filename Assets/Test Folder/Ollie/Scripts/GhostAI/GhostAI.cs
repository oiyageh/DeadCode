
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    [Header("Movement")]
    public NavMeshAgent agent;
    public float wanderRadius = 15f;
    public float waitTime = 5f;

    [Header("Ghost Model")]
    public Transform ghostModel;

    [Header("Floating")]
    public float floatHeight = 0.3f;
    public float floatSpeed = 2f;

    [Header("Haunting")]
    public GhostHauntTarget[] hauntTargets;
    public float hauntDuration = 5f;
    public float hauntDistance = 2f;

    private Vector3 startPosition;
    private float timer;

    private GhostHauntTarget currentTarget;
    private bool isHaunting = false;

    void Start()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (agent == null || !agent.enabled)
        {
            Debug.LogError("Ghost needs a NavMeshAgent!");
            enabled = false;
            return;
        }

        if (ghostModel != null)
            startPosition = ghostModel.localPosition;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(
            transform.position, out hit, 5f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }
        else
        {
            Debug.LogError("Ghost could not find a NavMesh!");
            enabled = false;
            return;
        }

        PickRandomPosition();
    }

    void Update()
    {
        if (agent == null || !agent.enabled || !agent.isOnNavMesh)
            return;

        FloatGhost();

        // If currently haunting an object
        if (isHaunting)
        {
            timer += Time.deltaTime;

            if (timer >= hauntDuration)
            {
                isHaunting = false;
                currentTarget = null;

                PickNextTarget();
            }

            return;
        }

        // If moving toward a haunting target
        if (currentTarget != null)
        {
            if (!agent.pathPending &&
                agent.remainingDistance <= hauntDistance)
            {
                StartHaunting();
            }

            return;
        }

        // If wandering, choose a new target after waiting
        timer += Time.deltaTime;

        if (!agent.pathPending &&
            agent.remainingDistance <= 1f &&
            timer >= waitTime)
        {
            PickNextTarget();
        }
    }

    void FloatGhost()
    {
        if (ghostModel == null) return;

        float newY = Mathf.Sin(Time.time * floatSpeed)
                     * floatHeight;

        ghostModel.localPosition = new Vector3(
            startPosition.x,
            startPosition.y + newY,
            startPosition.z
        );
    }

    void PickNextTarget()
    {
        timer = 0;

        // Collect available targets
        System.Collections.Generic.List<GhostHauntTarget> available =
            new System.Collections.Generic.List<GhostHauntTarget>();

        foreach (GhostHauntTarget target in hauntTargets)
        {
            if (target != null && !target.IsHaunted)
            {
                available.Add(target);
            }
        }

        // Choose a random object to haunt
        if (available.Count > 0)
        {
            int randomIndex = Random.Range(0, available.Count);

            currentTarget = available[randomIndex];

            Transform destination = currentTarget.hauntPoint != null
                ? currentTarget.hauntPoint
                : currentTarget.transform;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(
                destination.position, out hit, 3f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
            else
            {
                currentTarget = null;
                PickRandomPosition();
            }
        }
        else
        {
            // No available objects, continue wandering
            currentTarget = null;
            PickRandomPosition();
        }
    }

    void StartHaunting()
    {
        if (currentTarget == null) return;

        agent.ResetPath();

        currentTarget.StartHaunting();

        isHaunting = true;
        timer = 0;
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

        timer = 0;
    }
}
