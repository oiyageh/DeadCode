using System.Collections;
using UnityEngine;

public class TempoaryText : MonoBehaviour
{
    public GameObject textObject;
    public float displayDuraition = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (textObject != null)
        {
            textObject.SetActive(false);
        }
    }
    public void ShowText()
    {
        StopAllCoroutines();
        StartCoroutine(ShowAndHideRoutine());
    }
    
    private IEnumerator ShowAndHideRoutine()
    {
        textObject.SetActive (true);
        yield return new WaitForSeconds(displayDuraition);
        textObject.SetActive (false);
    }
}
