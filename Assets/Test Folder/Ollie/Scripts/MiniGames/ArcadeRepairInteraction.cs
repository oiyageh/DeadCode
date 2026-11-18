
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
        if (!playerNearby || repairComplete)
            return;

        // Only allow repairs if the arcade is haunted.
        if (hauntTarget == null || !hauntTarget.IsHaunted)
        {
            repairPanel.SetActive(false);
            return;
        }

        repairPanel.SetActive(true);

        // Hold E to fill the repair bar.
        if (Keyboard.current != null &&
            Keyboard.current.eKey.isPressed)
        {
            repairProgress += Time.deltaTime / repairTime;
            repairProgress = Mathf.Clamp01(repairProgress);

            repairSlider.value = repairProgress;
        }

        // Repair is complete when the bar reaches 100%.
        if (repairProgress >= 1f)
        {
            FinishRepair();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            repairPanel.SetActive(false);
        }
    }

    void FinishRepair()
    {
        repairComplete = true;

        hauntTarget.SolveHaunting();

        repairPanel.SetActive(false);

        Debug.Log("Arcade repaired!");
    }
}
