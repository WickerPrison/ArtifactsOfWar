using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DropSpot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Func<DragNDropData, bool> DropFunc;

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        StrategyManager.Instance.dropSpot = null;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        StrategyManager.Instance.dropSpot = this;
    }
}
