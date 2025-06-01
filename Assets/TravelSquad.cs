using UnityEngine;
using System.Collections.Generic;

public class TravelSquad : MonoBehaviour
{
    public List<PlayerUnitStats> squad = new List<PlayerUnitStats>(); 
    [System.NonSerialized] public IAmOrigin origin;
    [System.NonSerialized] public IAmDestination destination;
    [System.NonSerialized] public int totalDays;
    int passedDays;

}
