using UnityEngine;
using System;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public List<GameObject> rawSquad = new List<GameObject>();
    List<EnemyUnit> squad = new List<EnemyUnit>();

    private void Start()
    {
        foreach (GameObject enemyPrefab in rawSquad)
        {
            EnemyUnit enemyUnit = Instantiate(enemyPrefab).GetComponent<EnemyUnit>();
            squad.Add(enemyUnit);
            AddToPrefferedPosition(enemyUnit);
        }
    }

    void AddToPrefferedPosition(EnemyUnit unit)
    {
        switch (unit.prefferedRow)
        {
            case UnitRow.FRONTLINE:
                AddToFrontRow(unit, () => AddToBackRow(unit, () => Debug.LogError("both rows full")));
                break;
            case (UnitRow.BACKLINE):
                AddToBackRow(unit, () => AddToFrontRow(unit, () => Debug.LogError("both rows full")));
                break;
        }
    }

    void AddToFrontRow(EnemyUnit unit, Action callback)
    {
        AddToRow(unit, callback, UnitSlotGroups.Instance.enemyFrontline);
    }

    void AddToBackRow(EnemyUnit unit, Action callback)
    {
        AddToRow(unit, callback, UnitSlotGroups.Instance.enemyBackline);
    }

    void AddToRow(EnemyUnit unit, Action callback, UnitSlot[] line)
    {
        for (int i = 0; i < 3; i++)
        {
            if (line[i].occupation == null)
            {
                unit.SetSlot(line[i]);
                return;
            }
        }
        callback();
    }

    private void Gm_onTurnMeter(object sender, EventArgs e)
    {
        foreach (EnemyUnit enemyUnit in squad)
        {
            if (enemyUnit.GainTurnMeter())
            {
                GameManager.Instance.readyForTurn.Enqueue(enemyUnit);
            }
        }
    }

    private void Global_onEnemyDeath(object sender, EnemyUnit enemyUnit)
    {
        squad.Remove(enemyUnit);
        if(squad.Count == 0)
        {
            Debug.LogWarning("Player Wins");
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.onTurnMeter += Gm_onTurnMeter;
        GlobalEvents.Instance.onEnemyDeath += Global_onEnemyDeath;
    }

    private void OnDisable()
    {
        GameManager.Instance.onTurnMeter -= Gm_onTurnMeter;
        GlobalEvents.Instance.onEnemyDeath -= Global_onEnemyDeath;
    }
}
