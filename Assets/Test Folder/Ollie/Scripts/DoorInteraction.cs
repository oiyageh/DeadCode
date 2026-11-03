using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public Camera playerCamera;

    public float interactDistance = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = playerCamera.ScreenPointToRay(
                new Vector3(Screen.width / 2, Screen.height / 2)
            );

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                Door door = hit.collider.GetComponentInParent<Door>();

                if (door != null)
                {
                    door.OpenDoor(transform.position);
                }
            }
        }
    }
}
