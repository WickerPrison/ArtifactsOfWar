using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class AssignSquadMenu : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] Transform squadMembers;
    int currentSquadSize;
    int maxSquadSize = 4;
    StrategyPath strategyPath;

    PlayerUnitStats[] frontRow = { null, null, null };
    PlayerUnitStats[] backRow = { null, null, null };

    private void Start()
    {
        background.SetActive(false);
    }

    public bool AddUnit(UnitRow row, int pos, DragNDropData data)
    {
        if (currentSquadSize >= maxSquadSize && !frontRow.Concat(backRow).ToArray().Contains(data.unitStats)) return false;
        if(row == UnitRow.FRONTLINE)
        {
            if (frontRow[pos] != null) return false;
            frontRow[pos] = data.unitStats;
        }
        else
        {
            if (backRow[pos] != null) return false;
            backRow[pos] = data.unitStats;
        }
        currentSquadSize += 1;
        data.unitCard.transform.SetParent(squadMembers);
        data.leaveFunc();
        data.leaveFunc = () =>
        {
            currentSquadSize -= 1;
            if(row == UnitRow.FRONTLINE)
            {
                frontRow[pos] = null;
            }
            else
            {
                backRow[pos] = null;
            }
        };
        return true;
    }

    private void Strategy_onCreatePath(object sender, StrategyPath newStrategyPath)
    {
        strategyPath = newStrategyPath;
        background.SetActive(true);
    }


    public void Depart()
    {
        if (currentSquadSize == 0) return;
        PlayerSquad squad = new PlayerSquad(frontRow, backRow);

        strategyPath.Depart(squad);
        ClearSquad();
        background.SetActive(false);
    }

    public void CloseAssignSquadMenu()
    {
        UnitMenuCard[] squadCards = squadMembers.GetComponentsInChildren<UnitMenuCard>();
        foreach (UnitMenuCard card in squadCards)
        {
            card.stronghold.AddToBarracks(card.unitStats);
        }
        StrategyEvents.Instance.UpdateStrongholdUnits(squadCards[0].stronghold);
        ClearSquad();
        background.SetActive(false);
        strategyPath.DestroyStrategyPath();
    }

    void ClearSquad()
    {
        Array.Clear(frontRow, 0, 3);
        Array.Clear(backRow, 0, 3);
        currentSquadSize = 0;
        for (int i = 0; i < squadMembers.childCount; i++)
        {
            Destroy(squadMembers.GetChild(i).gameObject);
        }
    }

    private void Strategy_onDeselectStronghold(object sender, System.EventArgs e)
    {
        CloseAssignSquadMenu();
    }

    private void OnEnable()
    {
        StrategyEvents.Instance.onCreatePath += Strategy_onCreatePath;
        StrategyEvents.Instance.onDeselectStronghold += Strategy_onDeselectStronghold;
    }

    private void OnDisable()
    {
        StrategyEvents.Instance.onCreatePath -= Strategy_onCreatePath;
        StrategyEvents.Instance.onDeselectStronghold += Strategy_onDeselectStronghold;
    }
}
