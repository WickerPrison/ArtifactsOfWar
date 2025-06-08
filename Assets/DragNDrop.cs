using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class DragNDrop : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] bool dragNDropEnabled;
    RectTransform rectTransform;
    [System.NonSerialized] public Vector2 homePosition;
    public Func<bool> dropAction;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (!dragNDropEnabled)
        {
            eventData.pointerDrag = null;
            return;
        }
        homePosition = rectTransform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (StrategyManager.Instance.dropSpot == null || !dropAction())
        {
            StartCoroutine(GoToPosition(eventData.position, homePosition));
        }
    }

    IEnumerator GoToPosition(Vector2 startPos, Vector2 finalPos)
    {
        dragNDropEnabled = false;
        float maxTime = 0.2f;
        float timer = 0;
        while(timer < maxTime)
        {
            rectTransform.position = Vector2.Lerp(startPos, finalPos, timer / maxTime);
            timer += Time.deltaTime;
            yield return null;
        }
        rectTransform.position = finalPos;
        dragNDropEnabled = true;
    }
}
