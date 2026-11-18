
using UnityEngine;

public class ParanormalProblem : MonoBehaviour
{
    [Header("Problem Settings")]
    public float problemValue = 20;

    private bool problemActive = false;

    public void StartProblem()
    {
        if (problemActive) return;

        problemActive = true;

        BullshitMeter.Instance.AddProblem(this);

        Debug.Log("A new problem occurred!");
    }

    public void EndProblem()
    {
        if (!problemActive) return;

        problemActive = false;

        BullshitMeter.Instance.SolveProblem(this);

        Debug.Log("Problem solved!");
    }
}
