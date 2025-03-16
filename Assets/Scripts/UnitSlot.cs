using UnityEngine;
using System;
using System.Collections.Generic;

public enum SlotType
{
    PLAYER_FRONTLINE, PLAYER_BACKLINE, PLAYER_COLLAPSED, ENEMY_FRONTLINE, ENEMY_BACKLINE, ENEMY_COLLAPSED
}

public class UnitSlot : MonoBehaviour
{
    public UnitRow row;
    [SerializeField] UnitSlot collapseTo;
    [SerializeField] UnitSlot forwardSlot;
    [SerializeField] UnitSlot backwardSlot;
    Dictionary<UnitRow, UnitSlot> uncollapseDict = new Dictionary<UnitRow, UnitSlot>(); 
    bool selectable = false;
    UnitSlotDisplay display;
    [SerializeField] SlotType slotType;
    Action callback;
    [System.NonSerialized] public IAmUnit occupation;
    UnitDisplay _occupationDisplay;
    IAmUnit _occupationCache;
    UnitDisplay occupationDisplay 
    { 
        get
        {
            if (occupation == null) return null;
            if(_occupationCache != occupation)
            {
                _occupationCache = occupation;
                _occupationDisplay = occupation.gameObject.GetComponent<UnitDisplay>();
            }
            return _occupationDisplay;
        } 
    }

    private void Awake()
    {
        display = GetComponent<UnitSlotDisplay>();
        display.row = row;
        if(row == UnitRow.COLLAPSED)
        {
            display.Hide();
        }
    }

    private void Start()
    {
        uncollapseDict.Add(UnitRow.FRONTLINE, forwardSlot);
        uncollapseDict.Add(UnitRow.BACKLINE, backwardSlot);
    }

    public void Activate(Action newCallback)
    {
        callback = newCallback;
        selectable = true;
        display.Activate();
        if(occupation != null)
        {
            occupationDisplay.SetSelectable(true);
        }
    }

    public void Deactivate()
    {
        selectable = false;
        callback = null;
        display.Deactivate();
        if(occupation != null)
        {
            occupationDisplay.SetSelectable(false);
            occupationDisplay.SetHighlighted(false);
        }
    }

    public void GetClicked()
    {
        if (selectable)
        {
            callback();
            Deactivate();
        }
    }

    public void SetOccupation(IAmUnit unit)
    {
        SubscribeToUnit(unit);
        occupation = unit;
    }

    private void Unit_onLeaveSlot(object sender, EventArgs e)
    {
        UnsubscribeToUnit();
        occupation = null;
    }

    public void Collapse()
    {
        display.Hide();
        collapseTo.Unhide();
        if (occupation == null) return;
        occupation.CollapseToSlot(collapseTo);
        UnsubscribeToUnit();
        occupation = null;
    }

    public void Uncollapse(UnitRow direction)
    {
        forwardSlot.Unhide();
        backwardSlot.Unhide();
        display.Hide();
        if (direction == UnitRow.NONE) return;
        occupation.UncollapseToSlot(uncollapseDict[direction]);
    }

    public void Unhide()
    {
        display.UnHide();
    }

    private void OnMouseEnter()
    {
        if (selectable)
        {
            display.HoverOver();
            if(occupation != null)
            {
                occupationDisplay.SetHighlighted(true);
            }
        }
    }

    private void OnMouseExit()
    {
        if (selectable)
        {
            display.EndHover();
            if (occupation != null)
            {
                occupationDisplay.SetHighlighted(false);
            }
        }
    }

    private void OnMouseDown()
    {
        GetClicked();
    }

    public bool IsEmpty()
    {
        return occupation == null;
    }

    private void Gm_onEndTurn(object sender, System.EventArgs e)
    {
        Deactivate();
    }

    private void OnEnable()
    {
        GameManager.Instance.onEndTurn += Gm_onEndTurn;
    }

    void SubscribeToUnit(IAmUnit unit)
    {
        unit.onLeaveSlot += Unit_onLeaveSlot;
    }

    private void OnDisable()
    {
        GameManager.Instance.onEndTurn -= Gm_onEndTurn;
    }

    void UnsubscribeToUnit()
    {
        occupation.onLeaveSlot -= Unit_onLeaveSlot;
    }
}
