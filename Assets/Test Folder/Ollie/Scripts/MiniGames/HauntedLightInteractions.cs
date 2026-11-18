
using UnityEngine;
using UnityEngine.InputSystem;

public class HauntedLightInteraction : MonoBehaviour
{
    public LightRepairMinigame repairGame;
    public GhostHauntTarget hauntTarget;

    private bool playerNearby;
    private bool minigameOpen;

    void Update()
    {
        if (!playerNearby || minigameOpen)
            return;

        if (hauntTarget == null || !hauntTarget.IsHaunted)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            minigameOpen = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            repairGame.OpenMinigame();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }
}
