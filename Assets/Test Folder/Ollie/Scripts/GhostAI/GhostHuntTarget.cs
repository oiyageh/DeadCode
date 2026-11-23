
using UnityEngine;

public class GhostHauntTarget : MonoBehaviour
{
    [Header("Haunting Settings")]
    public ParanormalProblem problem;
    public Transform hauntPoint;

    [Header("Light Effect")]
    public HauntedLightFlicker hauntedLight;

    [Header("Arcade Repair")]
    public ArcadeRepairInteraction arcadeRepair;

    public bool IsHaunted { get; private set; }

    public void StartHaunting()
    {
        if (IsHaunted) return;

        IsHaunted = true;

        if (problem != null)
            problem.StartProblem();

        if (hauntedLight != null)
            hauntedLight.StartFlickering();

        // Reset arcade repair whenever the ghost returns.
        if (arcadeRepair != null)
            arcadeRepair.ResetRepair();

        Debug.Log(gameObject.name + " is being haunted!");
    }

    public void SolveHaunting()
    {
        if (!IsHaunted) return;

        IsHaunted = false;

        if (problem != null)
            problem.EndProblem();

        if (hauntedLight != null)
            hauntedLight.StopFlickering();

        Debug.Log(gameObject.name + " is no longer haunted!");
    }
}
