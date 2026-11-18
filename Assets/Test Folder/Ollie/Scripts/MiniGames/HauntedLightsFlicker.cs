
using UnityEngine;

public class HauntedLightFlicker : MonoBehaviour
{
    [Header("Light Settings")]
    public Light targetLight;

    [Header("Flicker Settings")]
    public float minFlickerTime = 0.1f;
    public float maxFlickerTime = 0.8f;

    private float timer;
    private bool beingHaunted;
    private bool repaired;
    private bool lightOriginalState;

    void Start()
    {
        if (targetLight != null)
            lightOriginalState = targetLight.enabled;
    }

    void Update()
    {
        // Do nothing if the ghost isn't haunting the light.
        if (!beingHaunted || repaired || targetLight == null)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            targetLight.enabled = !targetLight.enabled;

            timer = Random.Range(
                minFlickerTime,
                maxFlickerTime
            );
        }
    }

    // Called when the ghost starts haunting the light.
    public void StartFlickering()
    {
        if (repaired) return;

        beingHaunted = true;
        timer = 0;

        Debug.Log("Ghost is messing with the light!");
    }

    // Called when the ghost stops haunting the light.
    public void StopFlickering()
    {
        beingHaunted = false;

        if (!repaired && targetLight != null)
            targetLight.enabled = lightOriginalState;

        Debug.Log("Ghost stopped messing with the light.");
    }

    // Called when the player repairs the light.
    public void RepairLight()
    {
        repaired = true;
        beingHaunted = false;

        if (targetLight != null)
            targetLight.enabled = true;

        Debug.Log("Light repaired!");
    }
}
