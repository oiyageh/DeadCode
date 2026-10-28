using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;
using Yarn.Unity;
public class WIreManager : MonoBehaviour
{
    public List<Transform> LeftWires;

    private List<Transform> TrueLeftWires;
    public List<Transform> RightWires;
    private List<Transform> TrueRightWires;

    public List<Color> WireColors;

    private bool dragging;
    private bool buttonsClickable;

    public float wireWidth;

    public UnityEvent OnGameWin;
    private LineRenderer currentLine;
    [ContextMenu("Start Minigame")]
    [YarnCommand("StartMinigame")]
    //Call this to start the minigame!
    public void StartMinigame()
    {
        lastButtonIndex = -1;
        buttonsClickable = true;
        InitializeButtons();
    }
    private void InitializeButtons()
    {
        List<Transform> tempWires = new List<Transform>();
        tempWires.AddRange(LeftWires);
        List<Transform> randomized = new List<Transform>();
        
        for(int i = 0; i < LeftWires.Count; i++)
        {
            int rand = Random.Range(0, tempWires.Count);
            randomized.Add(tempWires[rand]);
            tempWires.RemoveAt(rand);
        }

        TrueLeftWires = randomized;

        tempWires = new List<Transform>();
        tempWires.AddRange(RightWires);
        randomized = new List<Transform>();

        for (int i = 0; i < RightWires.Count; i++)
        {
            int rand = Random.Range(0, tempWires.Count);
            randomized.Add(tempWires[rand]);
            tempWires.RemoveAt(rand);
        }

        TrueRightWires = randomized;

        for(int i = 0; i < TrueLeftWires.Count; i++)
        {
            TrueLeftWires[i].gameObject.GetComponent<Image>().color = WireColors[i];
            TrueRightWires[i].gameObject.GetComponent<Image>().color = WireColors[i];
        }
    }
    public Material lineMaterial;
    private int lastButtonIndex;
    public void WireClicked(Transform buttonSender)
    {
        if (!buttonsClickable) return;

        if (!dragging)
        {
            int buttonIndex = TrueLeftWires.IndexOf(buttonSender);
            if (buttonIndex == -1) buttonIndex = TrueRightWires.IndexOf(buttonSender);
            currentLine = buttonSender.gameObject.AddComponent<LineRenderer>();
            lastButtonIndex = buttonIndex;

            currentLine.startColor = WireColors[buttonIndex];
            currentLine.endColor = WireColors[buttonIndex];
            currentLine.sharedMaterial = lineMaterial;
            currentLine.startWidth = wireWidth;
            currentLine.endWidth = wireWidth;
            currentLine.SetPosition(0, buttonSender.position);
            dragging = true;

            buttonSender.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            int buttonIndex = TrueLeftWires.IndexOf(buttonSender);
            if (buttonIndex == -1) buttonIndex = TrueRightWires.IndexOf(buttonSender);
            if (buttonIndex == lastButtonIndex)
            {
                lastButtonIndex = -1;
                dragging = false;
                currentLine.SetPosition(1, buttonSender.position);
                currentLine = null;

                buttonSender.gameObject.GetComponent<Button>().interactable = false;
            }
        }
        if (CheckCompleted())
        {
            buttonsClickable = false;
            Debug.Log("Game Won!");
            OnGameWin?.Invoke();
        }
    }
    private bool CheckCompleted()
    {
        for(int i = 0; i < LeftWires.Count; i++)
        {
            if (LeftWires[i].gameObject.GetComponent<Button>().interactable) return false;
        }

        for(int i = 0; i < RightWires.Count; i++)
        {
            if (RightWires[i].gameObject.GetComponent<Button>().interactable) return false;
        }
        return true;
    }

    void Update()
    {
        
    }
}
