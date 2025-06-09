using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class PlayerController : MonoBehaviour
{
    public PlayerSquadTesting rawSquad;
    List<PlayerUnit> squad = new List<PlayerUnit>();
    [SerializeField] GameObject unitPrefab;
    int frontlinePop = 0;
    int backlinePop = 0;

    private void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            if (rawSquad.frontline[i] == null) continue;
            PlayerUnit playerUnit = Instantiate(unitPrefab).GetComponent<PlayerUnit>();
            playerUnit.unitStats = new PlayerUnitStats(rawSquad.frontline[i]);
            squad.Add(playerUnit);
            playerUnit.SetSlot(UnitSlotGroups.Instance.playerFrontline[i]);
            ChangeRowPopulation(UnitRow.FRONTLINE, 1);
        }

        for (int i = 0; i < 3; i++)
        {
            if (rawSquad.backline[i] == null) continue;
            PlayerUnit playerUnit = Instantiate(unitPrefab).GetComponent<PlayerUnit>();
            playerUnit.unitStats = new PlayerUnitStats(rawSquad.backline[i]);
            squad.Add(playerUnit);
            playerUnit.SetSlot(UnitSlotGroups.Instance.playerBackline[i]);
            ChangeRowPopulation(UnitRow.BACKLINE, 1);
        }
        CheckForCollapse();
    }

    //void AddToPrefferedPosition(PlayerUnit unit)
    //{
    //    switch (unit.unitStats.unitType.prefferedRow)
    //    {
    //        case UnitRow.FRONTLINE:
    //            AddToFrontRow(unit, () => AddToBackRow(unit, () => Debug.LogError("both rows full")));
    //            break;
    //        case UnitRow.BACKLINE:
    //            AddToBackRow(unit, () => AddToFrontRow(unit, () => Debug.LogError("both rows full")));
    //            break;
    //    }
    //}

    //void AddToFrontRow(PlayerUnit unit, Action callback)
    //{
    //    AddToRow(unit, callback, UnitSlotGroups.Instance.playerFrontline);
    //}

    //void AddToBackRow(PlayerUnit unit, Action callback)
    //{
    //    AddToRow(unit, callback, UnitSlotGroups.Instance.playerBackline);
    //}

    //void AddToRow(PlayerUnit unit, Action callback, UnitSlot[] line)
    //{
    //    for(int i = 0; i < 3; i++)
    //    {
    //        if(line[i].occupation == null)
    //        {
    //            unit.SetSlot(line[i]);
    //            if(line == UnitSlotGroups.Instance.playerFrontline)
    //            {
    //                frontlinePop += 1;
    //            }
    //            else
    //            {
    //                backlinePop += 1;
    //            }
    //            return;
    //        }
    //    }
    //    callback();
    //}

    private void Gm_onTurnMeter(object sender, EventArgs e)
    {
        foreach(PlayerUnit playerUnit in squad)
        {
            if (playerUnit.GainTurnMeter())
            {
                GameManager.Instance.readyForTurn.Enqueue(playerUnit);
            }
        }
    }

    void CheckForCollapse()
    {
        if(frontlinePop <= 0 || backlinePop <= 0)
        {
            foreach (UnitSlot slot in UnitSlotGroups.Instance.playerBackAndFront)
            {
                slot.Collapse();
            }
            frontlinePop = 0;
            backlinePop = 0;
        }
    }

    public void Uncollapse(PlayerUnit playerUnit, UnitRow direction)
    {
        foreach(UnitSlot slot in UnitSlotGroups.Instance.playerCollapsedline)
        {
            if (slot.occupation == null)
            {
                slot.Uncollapse(UnitRow.NONE);
            }
            if((UnityEngine.Object)slot.occupation == playerUnit)
            {
                slot.Uncollapse(direction);
                ChangeRowPopulation(direction, 1);
            }
            else
            {
                slot.Uncollapse(CombatUtils.oppositeRow[direction]);
                ChangeRowPopulation(CombatUtils.oppositeRow[direction], 1);
            }
        }
    }

    void ChangeRowPopulation(UnitRow row, int amount)
    {
        switch (row)
        {
            case UnitRow.FRONTLINE:
                frontlinePop += amount;
                break;
            case UnitRow.BACKLINE:
                backlinePop += amount;
                break;
        }
    }

    void PrintRowPop()
    {
        Debug.Log("Frontline: " + frontlinePop.ToString() + " Backline: " + backlinePop.ToString());

    }

    private void Global_onPlayerFlip(object sender, UnitRow destination)
    {
        ChangeRowPopulation(CombatUtils.oppositeRow[destination], -1);
        ChangeRowPopulation(destination, 1);
        CheckForCollapse();
    }

    private void Global_onPlayerDeath(object sender, PlayerUnit playerUnit)
    {
        squad.Remove(playerUnit);
        ChangeRowPopulation(playerUnit.row, -1);
        CheckForCollapse();
        if(squad.Count == 0)
        {
            Debug.LogWarning("Player Loses");
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.onTurnMeter += Gm_onTurnMeter;
        GlobalEvents.Instance.onPlayerDeath += Global_onPlayerDeath;
        GlobalEvents.Instance.onPlayerFlip += Global_onPlayerFlip;
    }

    private void OnDisable()
    {
        GameManager.Instance.onTurnMeter -= Gm_onTurnMeter;
        GlobalEvents.Instance.onPlayerDeath -= Global_onPlayerDeath;
        GlobalEvents.Instance.onPlayerFlip -= Global_onPlayerFlip;
    }
}
