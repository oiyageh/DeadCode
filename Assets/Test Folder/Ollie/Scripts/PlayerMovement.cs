using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float gravity = -9f;

    public Transform cameraPivot;
    public float mouseSensitivity = 100;
    public float lookLimit = 85f;

    private CharacterController controller;
    private float verticalVelocity;
    private float xRotation;

    private void Start()
    {
        //Get the character conmtroller
        controller = GetComponent<CharacterController>();

        // lock the cursor and make the cursor invisibe to not dirstupt game play
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Mouse controls where player is lookjing
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -lookLimit, lookLimit);

        if (cameraPivot != null)
        {
            cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        // WASD movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move = Vector3.ClampMagnitude(move, 1f);

        // Hold Left Shift to run
        float currentSpeed = Input.GetKey(KeyCode.LeftShift)
            ? runSpeed
            : walkSpeed;

        // Apply gravity
        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        move.y = verticalVelocity;

        controller.Move(move * currentSpeed * Time.deltaTime);
    }

}
