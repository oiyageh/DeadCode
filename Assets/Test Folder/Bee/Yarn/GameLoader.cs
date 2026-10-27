using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class SceneManager : MonoBehaviour
{
    [YarnCommand("start_prizes")]
    public static void LoadPrizes()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PrizeGame");
    }

    [YarnCommand ("start_wires")]
    public static void LoadWires()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("WireGame");
    }

}
