
using UnityEngine;

public class Door : MonoBehaviour
{
    public float openForce = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OpenDoor(Vector3 playerPosition)
    {
        Vector3 direction = transform.position - playerPosition;

        direction.y = 0;

        rb.AddForce(direction.normalized * openForce, ForceMode.Impulse);
    }
}
