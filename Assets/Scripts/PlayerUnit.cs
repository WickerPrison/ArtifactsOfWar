using System;
using UnityEngine;
using System.Collections.Generic;

public class PlayerUnit : MonoBehaviour, ITakeTurns, IAmUnit
{
    [System.NonSerialized] public PlayerUnitStats unitStats;
    public UnitRow row;
    public event Action<UnitRow> onRowChange;
    public event EventHandler onStartTurn;
    public event EventHandler onEndTurn;
    public event Action<float> onUpdateTurnMeter;
    public event Action<int, int> onUpdateHealth;
    public event EventHandler onLeaveSlot;
    public GameState turnState { get { return GameState.PLAYER_TURN; } set { } }

    private void Start()
    {
        UpdateHealth();
    }

    public void SetSlot(UnitSlot slot)
    {
        onLeaveSlot?.Invoke(this, EventArgs.Empty);
        transform.position = slot.transform.position;
        slot.SetOccupation(this);
        row = slot.row;
        onRowChange?.Invoke(slot.row);
    }

    public void MoveToSlot(UnitSlot slot)
    {
        UnitRow initialRow = row;
        SetSlot(slot);
        if (slot.row != initialRow) Flip(slot.row);
    }

    public void Flip(UnitRow destination)
    {
        GlobalEvents.Instance.OnPlayerFlip(destination);
    }

    public void CollapseToSlot(UnitSlot destination)
    {
        row = UnitRow.COLLAPSED;
        transform.position = destination.transform.position;
        destination.SetOccupation(this);
        onRowChange?.Invoke(destination.row);
    }

    public void UncollapseToSlot(UnitSlot destination)
    {
        SetSlot(destination);
    }

    public bool GainTurnMeter()
    {
        unitStats.turnMeter += unitStats.speed;
        if(unitStats.turnMeter >= 100)
        {
            unitStats.turnMeter = 100;
            onUpdateTurnMeter?.Invoke(unitStats.turnMeter);
            return true;
        }
        onUpdateTurnMeter?.Invoke(unitStats.turnMeter);
        return false;
    }

    public int GetArmor()
    {
        return unitStats.GetArmor(row);
    }

    public void LoseHealth(int damage)
    {
        unitStats.health -= damage;
        if(unitStats.health <= 0)
        {
            Death();
        }
        UpdateHealth();
    }

    void UpdateHealth()
    {
        onUpdateHealth?.Invoke(unitStats.health, unitStats.maxHealth);
    }

    void Death()
    {
        onLeaveSlot?.Invoke(this, EventArgs.Empty);
        GlobalEvents.Instance.OnPlayerDeath(this);
        Destroy(gameObject);
    }

    public UnitDisplayData GetDisplayData()
    {
        UnitDisplayData data = new UnitDisplayData();
        data.unitName = unitStats.unitType.className;
        data.unitImage = unitStats.unitType.classImage;
        return data;
    }

    public List<Ability> GetAbilities()
    {
        return unitStats.GetAbilities(row);
    }

    public void StartTurn()
    {
        onStartTurn?.Invoke(this, EventArgs.Empty);
    }

    public void EndTurn()
    {
        unitStats.turnMeter = 0;
        onUpdateTurnMeter?.Invoke(unitStats.turnMeter);
        onEndTurn?.Invoke(this, EventArgs.Empty);
    }
}
