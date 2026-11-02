using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
[RequireComponent(typeof(BoxCollider2D))]
public class SortingBox : MonoBehaviour, IDropHandler
{
    public List<GameObject> capturedObjects = new List<GameObject>();
    private BoxCollider2D boxCollider;
    public enum SortingType
    {
        LeftSide,
        RightSide
    }
    public SortingType sortingType = SortingType.LeftSide;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag);
        if((sortingType == SortingType.LeftSide && eventData.pointerDrag.transform.CompareTag("SortLeftSide")) || (sortingType == SortingType.RightSide && eventData.pointerDrag.transform.CompareTag("SortRightSide")))
        {
            if(eventData.pointerDrag.GetComponent<Moveable>() != null)
            {
                eventData.pointerDrag.transform.GetComponent<Moveable>().isMovable = false;
                eventData.pointerDrag.transform.position = transform.position;
                capturedObjects.Add(eventData.pointerDrag);
            }
        }
    }
}
