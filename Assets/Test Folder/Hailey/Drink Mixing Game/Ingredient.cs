using UnityEngine;

public class Ingredient : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 initialPosition;
    private Transform initialParent;

    public bool inCookingStation = false;

    public Transform MixingStation;

    void Start()
    {
        initialPosition = transform.position;
        initialParent = transform.parent;
    }


    public void OnMouseUp()
    {
        isDragging = false;
        // Check if dropped on a valid cooking station
        if (inCookingStation)
        {
            transform.SetParent(MixingStation);
            Debug.Log("Ingredient placed on cooking station!");
        }
        else
        {
            // Snap back to original position if dropped incorrectly
            transform.position = initialPosition;
            transform.SetParent(initialParent);
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            newPosition.z = transform.position.z; // Keep the same z position
            transform.position = newPosition;
        }
    }
}