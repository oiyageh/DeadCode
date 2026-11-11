using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

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

    [Header("Settings Menu")]
    public GameObject settingsPanel;
    public Slider sensitivitySlider;
    public TMP_Text sensitivityText;

    private Camera CameraObj;
    private bool settingsOpen = false;

    [Header("YarnStuff")]
    public DialogueRunner dialogueRunner;
    private bool dialogueActive = false;

    private void Start()
    {
        //Get the character conmtroller
        controller = GetComponent<CharacterController>();
        CameraObj = GetComponentInChildren<Camera>();
        // make either scenemanager or playermovemnt a singleton    <-- TO DO
        //THIS IS VERY VERY BAD
        SceneManager.Camera = this;


        // lock the cursor and make the cursor invisibe to not dirstupt game play
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        // Set up sensitivity slider
        sensitivitySlider.minValue = 10f;
        sensitivitySlider.maxValue = 300f;
        sensitivitySlider.value = mouseSensitivity;

        sensitivitySlider.onValueChanged.AddListener(ChangeSensitivity);

        // Hide settings menu at startup
        settingsPanel.SetActive(false);
        UpdateSensitivityText(mouseSensitivity);

        //Listening for when the dialouge starts and stops 
        dialogueRunner.onDialogueStart.AddListener(DialogueStarted);
        dialogueRunner.onDialogueComplete.AddListener(DialogueEnded);
    }

    private void Update()
    {
        if (dialogueActive)
            return;

        // Open or close settings with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsOpen)
                CloseSettings();
            else
                OpenSettings();
        }

        // Stop player controls while settings are open
        if (settingsOpen)
            return;

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


    // Change mouse sensitivity
    public void ChangeSensitivity(float value)
    {
        mouseSensitivity = value;
        UpdateSensitivityText(value);
    }

    // Update sensitivity number on screen
    private void UpdateSensitivityText(float value)
    {
        sensitivityText.text = "Mouse Sensitivity: " +
            Mathf.RoundToInt(value);
    }

    // Open settings
    public void OpenSettings()
    {
        settingsOpen = true;

        settingsPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    // Close settings
    public void CloseSettings()
    {
        settingsOpen = false;

        settingsPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }

    public void DisableCamera()
    {
        CameraObj.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void EnableCamera()
    {
        CameraObj.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void DialogueStarted()
    {
        // Dialogue is active
        dialogueActive = true;

        // Unlock mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Freeze player
        if (controller != null)
        {
            controller.enabled = false;
        }
    }


    void DialogueEnded()
    {
        // Dialogue is over
        dialogueActive = false;

        // Lock mouse again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Allow player movement again
        if (controller != null)
        {
            controller.enabled = true;
        }
    }

    private void OnDestroy()
    {
        if (dialogueRunner != null)
        {
            dialogueRunner.onDialogueStart.RemoveListener(DialogueStarted);
            dialogueRunner.onDialogueComplete.RemoveListener(DialogueEnded);
        }
    }
}
