using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] private Transform selectedCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (selectedCam == null)
        {
            selectedCam = Camera.main.transform;
        }
    }

    public Transform GetCam()
    {
        return selectedCam;
    }
    public void TurnToFace(Transform target)
    {
        selectedCam.LookAt(target);
    }
}
