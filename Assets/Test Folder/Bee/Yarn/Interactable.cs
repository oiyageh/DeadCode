using UnityEngine;
using Yarn.Unity;

public class Interactable : MonoBehaviour
{
    public string idleNode;
    public string taskNode;
    public string completedNode;

    public bool ActiveTask = false;
    public bool TaskComplete = false;
    public bool idlePlayed = false;

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

            if (idlePlayed && !ActiveTask && !TaskComplete)
                return;

            string node = idleNode;

            if (ActiveTask)
                node = taskNode;

            if (TaskComplete)
                node = completedNode;

            if (!string.IsNullOrEmpty(node))
            {
                if (!ActiveTask && !TaskComplete)
                    idlePlayed = true;

                FindFirstObjectByType<DialogueRunner>().StartDialogue(node);
            }
        }
    }
}