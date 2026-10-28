using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Prize", menuName = "Scriptable Objects/Prize")]
public class Prize : ScriptableObject
{
    public string prizeName;
    public GameObject prizePrefab;
}
