
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    [Header("Ghost Settings")]
    public NavMeshAgent agent;
    public float hauntDuration = 10f;
    public float wanderRadius = 10f;

    [Header("Haunting Targets")]
    public GhostHauntTarget[] targets;

    private GhostHauntTarget currentTarget;

    private enum GhostState
    {
        ChoosingTarget,
        MovingToTarget,
        Haunting,
        MovingAway
    }

    private GhostState currentState;

    private float hauntTimer;

    void Start()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("Ghost needs a NavMeshAgent!");
            enabled = false;
            return;
        }

        currentState = GhostState.ChoosingTarget;
    }

    void Update()
    {
        switch (currentState)
        {
            case GhostState.ChoosingTarget:
                ChooseRandomTarget();
                break;

            case GhostState.MovingToTarget:
                CheckTargetArrival();
                break;

            case GhostState.Haunting:
                HauntTarget();
                break;

            case GhostState.MovingAway:
                CheckWanderArrival();
                break;
        }
    }

    void ChooseRandomTarget()
    {
        List<GhostHauntTarget> availableTargets =
            new List<GhostHauntTarget>();

        // Find targets that have not been haunted yet.
        foreach (GhostHauntTarget target in targets)
        {
            if (target != null && !target.IsHaunted)
            {
                availableTargets.Add(target);
            }
        }

        // Wait if every target already has a problem.
        if (availableTargets.Count == 0)
            return;

        // Pick a random target.
        int randomIndex = Random.Range(0, availableTargets.Count);

        currentTarget = availableTargets[randomIndex];

        // Move toward the target.
        Vector3 destination = currentTarget.transform.position;

        if (currentTarget.hauntPoint != null)
            destination = currentTarget.hauntPoint.position;

        agent.isStopped = false;
        agent.SetDestination(destination);

        currentState = GhostState.MovingToTarget;

        Debug.Log("Ghost chose: " + currentTarget.gameObject.name);
    }

    void CheckTargetArrival()
    {
        if (currentTarget == null)
        {
            currentState = GhostState.ChoosingTarget;
            return;
        }

        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance + 0.2f)
        {
            // Check that another event hasn't haunted this target already.
            if (!currentTarget.IsHaunted)
            {
                currentTarget.StartHaunting();

                hauntTimer = hauntDuration;
                currentState = GhostState.Haunting;
            }
            else
            {
                currentTarget = null;
                currentState = GhostState.ChoosingTarget;
            }
        }
    }

    void HauntTarget()
    {
        hauntTimer -= Time.deltaTime;

        // Leave after the haunting time ends.
        if (hauntTimer <= 0)
        {
            currentTarget = null;
            MoveAway();
        }
    }

    void MoveAway()
    {
        Vector3 randomDirection =
            Random.insideUnitSphere * wanderRadius;

        randomDirection += transform.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(
            randomDirection,
            out hit,
            wanderRadius,
            NavMesh.AllAreas))
        {
            agent.isStopped = false;
            agent.SetDestination(hit.position);

            currentState = GhostState.MovingAway;
        }
        else
        {
            currentState = GhostState.ChoosingTarget;
        }
    }

    void CheckWanderArrival()
    {
        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance + 0.2f)
        {
            currentState = GhostState.ChoosingTarget;
        }
    }
}
