using UnityEngine;

public class SortTracker : MonoBehaviour
{
    [Header("Sorting box Scripts")] //will make this variable a list later.

    public SortingBox boxOne;
    public SortingBox boxTwo;

    public void Update()
    {
        if ((boxOne.numCaptured >= boxOne.totalToCapture) && (boxTwo.numCaptured >= boxTwo.totalToCapture))
        {
            //FOR PLAYTESTING PURPOSES: 
            UnityEngine.SceneManagement.SceneManager.LoadScene("EnviormentTesting");
        }
    }
}
