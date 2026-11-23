using UnityEngine;
using Yarn.Unity;

public class NPCManager : MonoBehaviour
{
    public GameObject currentNPC;
    public Interactable Penny;
    public Interactable Rosaline;
    public Interactable Alcoholic;
    public Interactable Russell;
    public int activeTasks = 0;
    private DialogueRunner dialogueRunner;

    public void SetNPC(GameObject npc)
    {
        currentNPC = npc;
    }

    [YarnCommand("deactivate_npc")]
    public void DeactivateNPC()
    {
        currentNPC.SetActive(false);
    }

    [YarnCommand("completed_task")]
    public void CompleteTask()
{
    currentNPC.GetComponent<Interactable>().TaskComplete = true;
    currentNPC.GetComponent<Interactable>().ActiveTask = false;
    Debug.Log("Current NPC's task has been completed.");
    activeTasks--;
}

    void Start()
    {   
         activeTasks++;

        dialogueRunner = FindFirstObjectByType<DialogueRunner>();

        Penny.ActiveTask = true;
        Invoke("RosalineTask", 60f);
    }

    void RosalineTask()
    {   Debug.Log("Rosaline wants something.");

         if (dialogueRunner.IsDialogueRunning)
            {
                Invoke("RosalineTask", 1f);
            }
            else
            {
        Rosaline.ActiveTask = true;
                activeTasks++;
        Invoke ("AlcoholicTask", 30f);
            }
    }

    void AlcoholicTask()
    {   Debug.Log("Someone wants something.");
               if (dialogueRunner.IsDialogueRunning)
            {
                Invoke("AlcoholicTask", 1f);
            }
        Alcoholic.gameObject.SetActive(true);
        Alcoholic.ActiveTask = true;
                activeTasks++;

        Invoke ("RussellTask", 60f);
    }

    void RussellTask()
    {
        Debug.Log("Russell wants something.");
        if (dialogueRunner.IsDialogueRunning)
        {
            Invoke("RussellTask", 1f);
        }
          activeTasks++;

        Russell.gameObject.SetActive(true);
        Russell.ActiveTask = true;
    }
}