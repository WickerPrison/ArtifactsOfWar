using UnityEngine;
using System.Collections.Generic;
using System;

public class Stronghold : MonoBehaviour, IAmOrigin, IAmDestination
{
    [System.NonSerialized] public List<PlayerUnitStats> availableRecruits = new List<PlayerUnitStats>();
    [System.NonSerialized] public List<PlayerUnitStats> barracks = new List<PlayerUnitStats>();
    bool instantiatedCorrectly = false;
    [System.NonSerialized] public Guid guid;

    public static Stronghold Create(GameObject prefab, StrongholdData data)
    {
        Stronghold stronghold = Instantiate(prefab).GetComponent<Stronghold>();
        stronghold.transform.position = data.position;
        stronghold.availableRecruits = new List<PlayerUnitStats>(data.availableRecruits);
        stronghold.barracks = new List<PlayerUnitStats>(data.barracks);
        stronghold.guid = data.guid;
        stronghold.instantiatedCorrectly = true;
        return stronghold;
    }

    private void Start()
    {
        if (!instantiatedCorrectly) Utils.IncorrectInitialization("Stronghold");
        if (PersistData.strongholdsRandomlyGenerateOnLoad)
        {
           for(int i = 0; i < 5; i++)
           {
               availableRecruits.Add(GenerateNewRecruit.Instance.GetNewRecruit());
           }
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

    private void Strategy_onLoadToPersistentData(object sender, EventArgs e)
    {
        StrongholdData data = StrongholdData.CreateData(this);
        Utils.UpsertPersistentList<StrongholdData>(PersistData.strongholds, data);
    }

    private void OnEnable()
    {
        StrategyEvents.Instance.onLoadToPersistData += Strategy_onLoadToPersistentData;
    }

    private void OnDisable()
    {
        StrategyEvents.Instance.onLoadToPersistData -= Strategy_onLoadToPersistentData;
    }
}
