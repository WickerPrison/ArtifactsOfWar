using UnityEngine;
using System.Collections.Generic;

public class AssignSquadMenu : MonoBehaviour
{
    List<PlayerUnitStats> squad = new List<PlayerUnitStats>();
    [SerializeField] GameObject background;
    [SerializeField] List<UnitMenuCard> squadCards = new List<UnitMenuCard>();
    int maxSquadSize = 4;
    StrategyPath strategyPath;

    private void Start()
    {
        background.SetActive(false);
    }

    private void Strategy_onCreatePath(object sender, StrategyPath newStrategyPath)
    {
        strategyPath = newStrategyPath;
        background.SetActive(true);
        UpdateAssembleSquadMenu();
    }

    private void Strategy_onAddUnitToSquad(object sender, PlayerUnitStats newUnit)
    {
        if(squad.Count >= maxSquadSize)
        {
            return;
        }

        squad.Add(newUnit);
        UpdateAssembleSquadMenu();
    }

    void UpdateAssembleSquadMenu()
    {
        for (int i = 0; i < maxSquadSize; i++)
        {
            if(squad.Count > i)
            {
                squadCards[i].UpdateUnit(squad[i]);
            }
            else
            {
                squadCards[i].UpdateUnit(null);
            }
        }
    }

    public void Depart()
    {
        if (squad.Count <= 0) return;
        strategyPath.Depart(squad);
        CloseAssignSquadMenu();
    }

    public void CloseAssignSquadMenu()
    {
        squad.Clear();
        UpdateAssembleSquadMenu();
        background.SetActive(false);
    }

    private void Strategy_onDeselectStronghold(object sender, System.EventArgs e)
    {
        CloseAssignSquadMenu();
    }

    private void OnEnable()
    {
        StrategyEvents.Instance.onCreatePath += Strategy_onCreatePath;
        StrategyEvents.Instance.onAddUnitToSquad += Strategy_onAddUnitToSquad;
        StrategyEvents.Instance.onDeselectStronghold += Strategy_onDeselectStronghold;
    }

    private void OnDisable()
    {
        StrategyEvents.Instance.onCreatePath -= Strategy_onCreatePath;
        StrategyEvents.Instance.onAddUnitToSquad -= Strategy_onAddUnitToSquad;
        StrategyEvents.Instance.onDeselectStronghold += Strategy_onDeselectStronghold;

    }
}
