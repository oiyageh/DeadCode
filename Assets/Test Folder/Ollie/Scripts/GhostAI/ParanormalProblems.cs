
using UnityEngine;

public class ParanormalProblem : MonoBehaviour
{
    [Header("Problem Settings")]
    public float problemValue = 20;

    [Header("Indicator Settings")]
    public GameObject indicatorPrefab;
    public Vector3 indicatorOffset = new Vector3(0, 2.5f, 0);

    private bool problemActive = false;
    private GameObject indicatorInstance;
    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (!problemActive || indicatorInstance == null)
            return;

        // Keep the Canvas above the problem object.
        indicatorInstance.transform.position =
            transform.position + indicatorOffset;

        // Make the Canvas face the player's camera.
        if (playerCamera != null)
        {
            indicatorInstance.transform.rotation =
                Quaternion.LookRotation(
                    playerCamera.transform.position -
                    indicatorInstance.transform.position
                );
        }
    }

    public void StartProblem()
    {
        // Prevent creating duplicate indicators.
        if (problemActive)
            return;

        problemActive = true;

        // Add this problem to the Bullshit Meter.
        if (BullshitMeter.Instance != null)
        {
            BullshitMeter.Instance.AddProblem(this);
        }

        // Create a separate Canvas indicator for this object.
        if (indicatorPrefab != null)
        {
            indicatorInstance = Instantiate(
                indicatorPrefab,
                transform.position + indicatorOffset,
                Quaternion.identity
            );
        }

        Debug.Log(gameObject.name + " problem started!");
    }

    public void EndProblem()
    {
        if (!problemActive)
            return;

        problemActive = false;

        // Remove this problem from the Bullshit Meter.
        if (BullshitMeter.Instance != null)
        {
            BullshitMeter.Instance.SolveProblem(this);
        }

        // Destroy only this object's indicator.
        if (indicatorInstance != null)
        {
            Destroy(indicatorInstance);
            indicatorInstance = null;
        }

        Debug.Log(gameObject.name + " problem solved!");
    }
}
