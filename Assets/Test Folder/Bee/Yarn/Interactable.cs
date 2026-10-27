using UnityEngine;
using Yarn.Unity;

public class PlayDialogue : MonoBehaviour
{

    public string node;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<DialogueRunner>().StartDialogue(node);
        }
    }
}

