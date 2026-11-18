
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

        // Follow the problem object.
        indicatorInstance.transform.position =
            transform.position + indicatorOffset;

        // Face the player.
        if (playerCamera != null)
        {
            Vector3 direction =
                playerCamera.transform.position -
                indicatorInstance.transform.position;

            indicatorInstance.transform.rotation =
                Quaternion.LookRotation(direction);
        }
    }

    public void StartProblem()
    {
        if (problemActive) return;

        problemActive = true;

        BullshitMeter.Instance.AddProblem(this);

        // Create an indicator instance.
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
        if (!problemActive) return;

        problemActive = false;

        BullshitMeter.Instance.SolveProblem(this);

        // Remove this problem's indicator.
        if (indicatorInstance != null)
        {
            Destroy(indicatorInstance);
            indicatorInstance = null;
        }

        Debug.Log(gameObject.name + " problem solved!");
    }
}
