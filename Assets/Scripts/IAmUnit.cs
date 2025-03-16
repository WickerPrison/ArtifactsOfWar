using UnityEngine;
using System;

public interface IAmUnit
{
    public GameObject gameObject { get; }
    public event Action<UnitRow> onRowChange;
    public event EventHandler onStartTurn;
    public event EventHandler onEndTurn;
    public event Action<float> onUpdateTurnMeter;
    public event EventHandler onLeaveSlot;
    public event Action<int, int> onUpdateHealth;
    public void LoseHealth(int damage);
    public UnitDisplayData GetDisplayData();
    public void CollapseToSlot(UnitSlot destination);
    public void UncollapseToSlot(UnitSlot destination);
}
