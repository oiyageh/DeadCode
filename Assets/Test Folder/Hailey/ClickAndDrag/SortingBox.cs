using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.Events;
public class SortingBox : MonoBehaviour, IDropHandler
{

    private BoxCollider2D boxCollider;
    public enum SortingType
    {
        LeftSide,
        RightSide
    }
    public SortingType sortingType = SortingType.LeftSide;
    public int numCaptured = 0;
    [System.Serializable]    
    
    public struct OnCaptureEvents
    {
        public UnityEvent onCapture;
        public UnityEvent onFull;
    }

    [Header("Events")]
    public OnCaptureEvents onCaptureEvents;

    [Tooltip("How many objects need to be captured in this box for it to be considered full.")]
    public int totalToCapture = 1;


    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag);
        if((sortingType == SortingType.LeftSide && eventData.pointerDrag.transform.CompareTag("SortLeftSide")) || (sortingType == SortingType.RightSide && eventData.pointerDrag.transform.CompareTag("SortRightSide")))
        {
            if(eventData.pointerDrag.GetComponent<Moveable>() != null)
            {
                eventData.pointerDrag.transform.GetComponent<Moveable>().isMovable = false;
                eventData.pointerDrag.transform.position = transform.position;
                numCaptured++;
                onCaptureEvents.onCapture?.Invoke();
                if (numCaptured >= totalToCapture)
                {
                    onCaptureEvents.onFull?.Invoke();
                }
            }
        }
    }
}
