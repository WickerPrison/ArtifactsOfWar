using UnityEngine;
using System.Collections.Generic;

public class Stronghold : MonoBehaviour, IAmOrigin, IAmDestination
{
    [System.NonSerialized] public List<PlayerUnitStats> availableRecruits = new List<PlayerUnitStats>();
    List<PlayerUnitStats> barracks = new List<PlayerUnitStats>();

    private void Start()
    {
       for(int i = 0; i < 5; i++)
       {
           availableRecruits.Add(GenerateNewRecruit.Instance.GetNewRecruit());
       }
    }

    public void AddToBarracks(PlayerUnitStats newUnit)
    {
        barracks.Add(newUnit);
    }

    public void RemoveFromBarracks(PlayerUnitStats unit)
    {
        barracks.Remove(unit);
        StrategyEvents.Instance.UpdateStrongholdUnits(this);
    }

    public List<PlayerUnitStats> GetBarracksCount()
    {
        return barracks;
    }

    public void PrepareDeparture()
    {

    }

    public void SquadArrived(PlayerSquad squad)
    {

    }

    private void OnMouseDown()
    {
        if (StrategyManager.Instance.strategyState == StrategyState.UNSELECTED)
        {
            StrategyEvents.Instance.SelectStronghold(this);
        }
    }

    private void OnEnable()
    {
        StrategyEvents.Instance.onSaveState += Strategy_onSaveState;
    }

    private void OnDisable()
    {
        StrategyEvents.Instance.onSaveState -= Strategy_onSaveState;
    }

    private void Strategy_onSaveState(object sender, System.EventArgs e)
    {
        StrongholdData data = new StrongholdData();
        data.availableRecruits = new List<PlayerUnitStats>(availableRecruits);
        data.barracks = new List<PlayerUnitStats>(barracks);
        data.position = transform.position;
        PersistData.strongholds.Add(data);
    }
}
