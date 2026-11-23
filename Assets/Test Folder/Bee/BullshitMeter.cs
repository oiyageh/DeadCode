
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BullshitMeter : MonoBehaviour
{
    public static BullshitMeter Instance;

    [Header("Meter Settings")]
    public Slider slider;

    public float maxProblems = 100;
    public float currentProblems = 0;
    public float meterSpeed = 0.5f;

    private HashSet<ParanormalProblem> activeProblems =
        new HashSet<ParanormalProblem>();
    private NPCManager npcManager;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        slider.maxValue = maxProblems;
        slider.value = currentProblems;
        npcManager = FindFirstObjectByType<NPCManager>();
    }

    void Update()
    {
        float target = 0;
        target += npcManager.activeTasks * 25;
        foreach (ParanormalProblem problem in activeProblems)
        {
            target += problem.problemValue;
        }

        target = Mathf.Clamp(target, 0, maxProblems);

        currentProblems = Mathf.MoveTowards(
            currentProblems,
            target,
            Time.deltaTime * meterSpeed
        );

        slider.value = currentProblems;
    }

    public void AddProblem(ParanormalProblem problem)
    {
        activeProblems.Add(problem);
    }

    public void SolveProblem(ParanormalProblem problem)
    {
        activeProblems.Remove(problem);
    }
}
