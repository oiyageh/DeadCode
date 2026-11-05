using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using Unity.Cinemachine;
using Unity.VisualScripting;

public class SceneManager : MonoBehaviour
{
    public static PlayerMovement Camera;

    [YarnCommand("start_prizes")]
    public static void LoadPrizes()
    {
        LoadScene("PrizeGame");
    }

    [YarnCommand ("start_wires")]
    public static void LoadWires()
    {
        LoadScene("WireGame");
    }
    [YarnCommand("start_sort")]
    public static void LoadSort()
    {
        LoadScene("SortingGame");
    }

    public static void LoadScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name, LoadSceneMode.Additive);
        Camera.DisableCamera();
        
    }

    public static void UnloadScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(name);
        Camera.EnableCamera();
    }


}
