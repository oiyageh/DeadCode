

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ArcadeRepairInteraction : MonoBehaviour
{
    [Header("Repair Settings")]
    public GhostHauntTarget hauntTarget;
    public float repairTime = 4f;

    [Header("UI")]
    public GameObject repairPanel;
    public Slider repairSlider;

    private bool playerNearby;
    private bool repairComplete;
    private float repairProgress;

    void Start()
    {
        repairPanel.SetActive(false);
        repairSlider.value = 0;
    }

    void Update()
    {
        // Only allow repairing when the player is nearby.
        if (!playerNearby || repairComplete)
            return;

        // Only show the repair UI when the arcade is haunted.
        if (hauntTarget == null || !hauntTarget.IsHaunted)
        {
            repairPanel.SetActive(false);
            return;
        }

        repairPanel.SetActive(true);

        // Hold E to repair.
        if (Keyboard.current != null &&
            Keyboard.current.eKey.isPressed)
        {
            repairProgress += Time.deltaTime / repairTime;
            repairProgress = Mathf.Clamp01(repairProgress);

            repairSlider.value = repairProgress;
        }

        // Finish repairing.
        if (repairProgress >= 1f)
        {
            FinishRepair();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            repairPanel.SetActive(false);
        }
    }

    public void ResetRepair()
    {
        repairComplete = false;
        repairProgress = 0;

        repairSlider.value = 0;
        repairPanel.SetActive(false);

        Debug.Log("Arcade can be broken again!");
    }

    void FinishRepair()
    {
        repairComplete = true;

        repairProgress = 1;
        repairSlider.value = 1;

        hauntTarget.SolveHaunting();

        repairPanel.SetActive(false);

        Debug.Log("Arcade repaired!");
    }
}
