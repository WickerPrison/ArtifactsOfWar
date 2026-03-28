using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class AssignSquadMenu : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] Transform squadMembers;
    [SerializeField] GameObject travelSquadPrefab;
    [SerializeField] StrategyManager strategyManager;
    int currentSquadSize;
    int maxSquadSize = 4;
    [System.NonSerialized] public Action closeStrongholdMenu;

    PlayerUnitStats[] frontRow = { null, null, null };
    PlayerUnitStats[] backRow = { null, null, null };

    private void Start()
    {
        background.SetActive(false);
    }

    public void ActivateMenu(Action closeMenuCallback)
    {
        closeStrongholdMenu = closeMenuCallback;
        background.SetActive(true);
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


    public void Depart()
    {
        if (currentSquadSize == 0) return;
        PlayerSquad squad = new PlayerSquad((PlayerUnitStats[])frontRow.Clone(), (PlayerUnitStats[])backRow.Clone());

        Vector3 squadPosition = strategyManager.stronghold.transform.position + new Vector3(0.5f, 0.5f, 0);
        TravelSquadData travelSquadData = new TravelSquadData(squad, squadPosition);
        TravelSquad.Create(travelSquadPrefab, travelSquadData);
        closeStrongholdMenu();
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
        if(squadCards.Length > 0)
        {
            StrategyEvents.Instance.UpdateStrongholdUnits(squadCards[0].stronghold);
        }
        ClearSquad();
        background.SetActive(false);
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
}
