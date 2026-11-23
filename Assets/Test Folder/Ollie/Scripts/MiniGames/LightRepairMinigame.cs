
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class LightRepairMinigame : MonoBehaviour
{
    [Header("UI Objects")]
    public GameObject repairPanel;
    public RectTransform oldBulb;
    public GameObject emptySocket;
    public RectTransform newBulb;
    public RectTransform socket;

    [Header("Repair Settings")]
    public float turnsRequired = 2f;

    [Header("Player Controls")]
    public MonoBehaviour playerMovement;
    public MonoBehaviour cameraLook;

    [Header("Repair References")]
    public GhostHauntTarget hauntTarget;
    public HauntedLightFlicker flicker;

    private enum RepairStep
    {
        UnscrewBulb,
        ReplaceBulb,
        Finished
    }

    private RepairStep currentStep;

    private float rotationProgress;
    private float previousAngle;
    private bool rotatingBulb;

    private Vector2 newBulbStartPosition;
    private bool draggingNewBulb;

    private Canvas canvas;

    void Start()
    {
        canvas = repairPanel.GetComponentInParent<Canvas>();
        repairPanel.SetActive(false);
    }

    void Update()
    {
        if (!repairPanel.activeSelf)
            return;

        // STEP 1: Rotate the old bulb.
        if (currentStep == RepairStep.UnscrewBulb)
        {
            HandleOldBulb();
        }

        // STEP 2: Drag the new bulb.
        if (currentStep == RepairStep.ReplaceBulb)
        {
            HandleNewBulb();
        }
    }

    public void OpenMinigame()
    {
        repairPanel.SetActive(true);

        oldBulb.gameObject.SetActive(true);
        emptySocket.SetActive(false);
        newBulb.gameObject.SetActive(false);

        rotationProgress = 0;
        rotatingBulb = false;
        draggingNewBulb = false;

        currentStep = RepairStep.UnscrewBulb;

        newBulb.anchoredPosition = newBulbStartPosition;

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (cameraLook != null)
            cameraLook.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // OLD BULB ROTATION
    void HandleOldBulb()
    {
        if (Mouse.current == null)
            return;

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(
                oldBulb, mousePosition, null))
            {
                rotatingBulb = true;
                previousAngle = GetMouseAngle(mousePosition);
            }
        }

        if (Mouse.current.leftButton.isPressed && rotatingBulb)
        {
            float currentAngle = GetMouseAngle(mousePosition);

            float difference =
                Mathf.DeltaAngle(previousAngle, currentAngle);

            rotationProgress += difference;

            rotationProgress = Mathf.Clamp(
                rotationProgress, 0, turnsRequired * 360f
            );

            oldBulb.Rotate(0, 0, difference);

            previousAngle = currentAngle;

            if (rotationProgress >= turnsRequired * 360f)
            {
                OldBulbRemoved();
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            rotatingBulb = false;
        }
    }

    float GetMouseAngle(Vector2 mousePosition)
    {
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            oldBulb,
            mousePosition,
            null,
            out localPoint
        );

        return Mathf.Atan2(
            localPoint.y, localPoint.x
        ) * Mathf.Rad2Deg;
    }

    // REMOVE OLD BULB
    void OldBulbRemoved()
    {
        rotatingBulb = false;

        currentStep = RepairStep.ReplaceBulb;

        oldBulb.gameObject.SetActive(false);
        emptySocket.SetActive(true);
        newBulb.gameObject.SetActive(true);
    }

    // NEW BULB DRAGGING
    void HandleNewBulb()
    {
        if (Mouse.current == null)
            return;

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(
                newBulb, mousePosition, null))
            {
                draggingNewBulb = true;
            }
        }

        if (Mouse.current.leftButton.isPressed && draggingNewBulb)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                mousePosition,
                null,
                out Vector2 localPosition
            );

            newBulb.anchoredPosition = localPosition;
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame &&
            draggingNewBulb)
        {
            draggingNewBulb = false;

            bool insideSocket =
                RectTransformUtility.RectangleContainsScreenPoint(
                    socket, mousePosition, null);

            if (insideSocket)
            {
                NewBulbInstalled();
            }
            else
            {
                newBulb.anchoredPosition = newBulbStartPosition;
            }
        }
    }

    // FINISH REPAIR
    void NewBulbInstalled()
    {
        currentStep = RepairStep.Finished;

        if (flicker != null)
            flicker.RepairLight();

        if (hauntTarget != null)
            hauntTarget.SolveHaunting();

        repairPanel.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = true;

        if (cameraLook != null)
            cameraLook.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Haunted light successfully repaired!");
    }
}

