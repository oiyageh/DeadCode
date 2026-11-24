using UnityEngine;
using Yarn.Unity;
using TMPro;

public class NPCManager : MonoBehaviour
{
    public GameObject currentNPC;
    public Interactable Penny;
    public GameObject penny;
    public Interactable Rosaline;
    public Interactable Alcoholic;
    public Interactable Russell;
    public int activeTasks = 0;

    private DialogueRunner dialogueRunner;

    public TMP_Text taskNotif;

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

    [YarnCommand("reset_text")]
    public void TextReset()
    {
    taskNotif.text = "";
    }

void ShowTask(string message)
{
    taskNotif.text = message;
}

    void Start()
    {   
         activeTasks++;

        dialogueRunner = FindFirstObjectByType<DialogueRunner>();

        Penny.ActiveTask = true;
        Invoke("RosalineTask", 35f);

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
        ShowTask("It seems like Rosaline wants something.");
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
        ShowTask("Something is happening at the bar.");

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
            ShowTask("Someone is at the bar.");

        Russell.gameObject.SetActive(true);
        Russell.ActiveTask = true;
    }
}