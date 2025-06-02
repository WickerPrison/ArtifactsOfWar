using UnityEngine;
using System.Collections.Generic;

public class TravelSquad : MonoBehaviour
{
    public List<PlayerUnitStats> squad = new List<PlayerUnitStats>(); 
    [System.NonSerialized] public IAmOrigin origin;
    [System.NonSerialized] public IAmDestination destination;
    [System.NonSerialized] public int totalDays;
    int passedDays;

    private void Strategy_onNextDay(object sender, System.EventArgs e)
    {
        passedDays++;
        transform.position = Vector3.Lerp(
            origin.transform.position,
            destination.transform.position,
            (float)passedDays / totalDays
         );
        if(passedDays == totalDays)
        {
            destination.SquadArrived(squad);
        }
    }

    private void OnEnable()
    {
        StrategyEvents.Instance.onNextDay += Strategy_onNextDay;
    }

    private void OnDisable()
    {
        StrategyEvents.Instance.onNextDay -= Strategy_onNextDay;
    }
}
