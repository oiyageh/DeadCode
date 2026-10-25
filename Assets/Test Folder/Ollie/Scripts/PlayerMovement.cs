using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Speeds")]
    public float walkSpeed = 5.0f;
    public float runSpeed = 10.0f;

    private CharacterController controller;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        float currentSpeed = walkSpeed;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        }

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        controller.Move(moveDirection * currentSpeed * Time.deltaTime);
    }
}
