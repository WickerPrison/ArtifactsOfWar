using UnityEngine;
using System;
using System.Collections.Generic;

public class EnemyUnit : MonoBehaviour, ITakeTurns, IAmUnit
{
    public UnitRow prefferedRow;
    [SerializeField] string enemyName;
    [SerializeField] float speed;
    [SerializeField] int maxHealth;
    [SerializeField] int frontlineArmor;
    [SerializeField] int backlineArmor;
    [SerializeField] int collapsedArmor;
    Dictionary<UnitRow, int> armorDict = new Dictionary<UnitRow, int>();
    int health;
    [SerializeField] Sprite enemyImage;
    float turnMeter;
    [System.NonSerialized] public UnitRow row;
    public event Action<UnitRow> onRowChange;
    public event EventHandler onStartTurn;
    public event EventHandler onEndTurn;
    public event Action<float> onUpdateTurnMeter;
    public event Action<int, int> onUpdateHealth;
    public event EventHandler onLeaveSlot;
    public event EventHandler onCollapse;
    public GameState turnState { get { return GameState.ENEMY_TURN; } set { } }
    [System.NonSerialized] public List<EnemyUnit> squad;

    private void Awake()
    {
        armorDict.Add(UnitRow.FRONTLINE, frontlineArmor);
        armorDict.Add(UnitRow.BACKLINE, backlineArmor);
        armorDict.Add(UnitRow.COLLAPSED, collapsedArmor);
    }

    private void Start()
    {
        turnMeter = UnityEngine.Random.Range(0, 100);
        health = maxHealth;
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

    void Flip(UnitRow destination)
    {
        GlobalEvents.Instance.OnEnemyFlip(destination);
    }

    public bool GainTurnMeter()
    {
        turnMeter += speed;
        if (turnMeter >= 100)
        {
            turnMeter = 100;
            onUpdateTurnMeter?.Invoke(turnMeter);
            return true;
        }
        onUpdateTurnMeter?.Invoke(turnMeter);
        return false;
    }

    public int GetArmor()
    {
        return armorDict[row];
    }

    public void LoseHealth(int damage)
    {
        damage = Math.Max(damage - GetArmor(), 0);
        health -= damage;
        if(health <= 0)
        {
            Death();
        }
        UpdateHealth();
    }

    public void Collapse()
    {
        onCollapse?.Invoke(this, EventArgs.Empty);
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

    void Death()
    {
        onLeaveSlot?.Invoke(this, EventArgs.Empty);
        GlobalEvents.Instance.OnEnemyDeath(this);
        Destroy(gameObject);
    }

    void UpdateHealth()
    {
        onUpdateHealth?.Invoke(health, maxHealth);
    }

    public UnitDisplayData GetDisplayData()
    {
        UnitDisplayData data = new UnitDisplayData();
        data.unitName = enemyName;
        data.unitImage = enemyImage;
        return data;
    }

    public string GetName()
    {
        return enemyName;
    }

    public void StartTurn()
    {
        onStartTurn?.Invoke(this, EventArgs.Empty);
    }

    public void EndTurn()
    {
        turnMeter = 0;
        onUpdateTurnMeter?.Invoke(turnMeter);
        onEndTurn?.Invoke(this, EventArgs.Empty);
    }
}
