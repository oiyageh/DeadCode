using UnityEngine;
using Yarn.Unity;

public class PlayDialogue : MonoBehaviour
{

    public string node;
    private NPCManager npcManager;

    void Start()
{
    npcManager = FindFirstObjectByType<NPCManager>();
}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npcManager.SetNPC(gameObject);
            FindFirstObjectByType<DialogueRunner>().StartDialogue(node);
        }
        Debug.Log("this is an interaction");
    }
}

