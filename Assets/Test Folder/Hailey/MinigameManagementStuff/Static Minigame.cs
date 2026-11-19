using UnityEngine;

[CreateAssetMenu(fileName = "StaticMinigame", menuName = "Scriptable Objects/StaticMinigame")]
public class StaticMinigame : ScriptableObject
{
    //game start
    public bool gameStarted;
    //game end
    public bool gameEnded;
    //cooldown time
    public float coolDownTime;
    //interactable?
    public bool interactable;
}
