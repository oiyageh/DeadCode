
using UnityEngine;

public class ParanormalProblem : MonoBehaviour
{
    [Header("Problem Settings")]
    public float problemValue = 20;

    [Header("Indicator Settings")]
    public GameObject indicator;
    public float heightAboveObject = 1f;

    private bool problemActive;
    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;

        // Hide the indicator when the game starts.
        if (indicator != null)
            indicator.SetActive(false);
    }

    void LateUpdate()
    {
        if (!problemActive || indicator == null)
            return;

        // Position indicator above the object.
        Collider objectCollider = GetComponent<Collider>();

        if (objectCollider != null)
        {
            indicator.transform.position = new Vector3(
                objectCollider.bounds.center.x,
                objectCollider.bounds.max.y + heightAboveObject,
                objectCollider.bounds.center.z
            );
        }
        else
        {
            indicator.transform.position =
                transform.position + Vector3.up * heightAboveObject;
        }

        // Make indicator face the player.
        if (playerCamera != null)
        {
            Vector3 direction =
                playerCamera.transform.position -
                indicator.transform.position;

            indicator.transform.rotation =
                Quaternion.LookRotation(direction);
        }
    }

    public void StartProblem()
    {
        if (problemActive) return;

        problemActive = true;

        BullshitMeter.Instance.AddProblem(this);

        if (indicator != null)
            indicator.SetActive(true);

        Debug.Log(gameObject.name + " problem started!");
    }

    public void EndProblem()
    {
        if (!problemActive) return;

        problemActive = false;

        BullshitMeter.Instance.SolveProblem(this);

        if (indicator != null)
            indicator.SetActive(false);

        Debug.Log(gameObject.name + " problem solved!");
    }
}
