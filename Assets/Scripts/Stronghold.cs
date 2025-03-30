using UnityEngine;
using System.Collections.Generic;

public class Stronghold : MonoBehaviour
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

    public List<PlayerUnitStats> GetBarracksCount()
    {
        return barracks;
    }

    private void OnMouseDown()
    {
        StrategyEvents.Instance.OpenStrongholdMenu(this);
    }
}
