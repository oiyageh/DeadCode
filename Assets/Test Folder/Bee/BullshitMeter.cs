using UnityEngine;
using UnityEngine.UI;

public class BullshitMeter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float count = GameObject.FindGameObjectsWithTag("NPC").Length / 10f;

        Slider slider = GetComponent<Slider>();
        slider.value = Mathf.MoveTowards(slider.value, count, Time.deltaTime * 0.5f);
    }
}
