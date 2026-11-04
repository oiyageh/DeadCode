using UnityEngine;
using Yarn.Unity;

public class NPCManager : MonoBehaviour
{
    public GameObject currentNPC;

    public void SetNPC(GameObject npc)
    {
        currentNPC = npc;
    }

    [YarnCommand("deactivate_npc")]
    public void DeactivateNPC()
    {
        currentNPC.SetActive(false);
    }
}