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

    private void OnMouseDown()
    {
        if(StrategyManager.Instance.strategyState == StrategyState.UNSELECTED)
        {
            StrategyEvents.Instance.SelectStronghold(this);
        }
    }
}
