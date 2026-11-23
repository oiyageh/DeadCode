
using UnityEngine;

public class HauntedLightFlicker : MonoBehaviour
{
    public Light targetLight;

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
        if (!beingHaunted || repaired || targetLight == null)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            targetLight.enabled = !targetLight.enabled;
            timer = Random.Range(minFlickerTime, maxFlickerTime);
        }
    }

    public void StartFlickering()
    {
        // Allow the ghost to haunt the light again.
        repaired = false;
        beingHaunted = true;
        timer = 0;

        Debug.Log("Ghost is messing with the light!");
    }

    public void StopFlickering()
    {
        beingHaunted = false;

        if (!repaired && targetLight != null)
            targetLight.enabled = lightOriginalState;
    }

    public void RepairLight()
    {
        repaired = true;
        beingHaunted = false;

        if (targetLight != null)
            targetLight.enabled = true;

        Debug.Log("Light repaired!");
    }
}
