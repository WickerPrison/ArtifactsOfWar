using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TravelSquadData : IHaveGuid
{
    public PlayerSquad squad;
    public Vector3 destination;
    public Vector3 position;
    public float speed;
    Guid _guid;
    public Guid guid
    {
        get
        {
            if (_guid == Guid.Empty)
            {
                _guid = Guid.NewGuid();
            }
            return _guid;
        }
        private set => _guid = value;
    }

    public static TravelSquadData CreateFromTestSquad(TestTravelSquad testTravelSquad)
    {
        PlayerUnitStats[] frontRow = new PlayerUnitStats[3];
        PlayerUnitStats[] backRow = new PlayerUnitStats[3];
        for(int i = 0; i < 3; i++)
        {
            if (testTravelSquad.frontRow[i] != null) frontRow[i] = new PlayerUnitStats(testTravelSquad.frontRow[i]);
            if (testTravelSquad.backRow[i] != null) backRow[i] = new PlayerUnitStats(testTravelSquad.backRow[i]);
        }
        PlayerSquad squad = new PlayerSquad(frontRow, backRow);
        return new TravelSquadData(squad, testTravelSquad.position);
    }

    public static TravelSquadData CreateData(TravelSquad travelSquad)
    {
        TravelSquadData squadData = new TravelSquadData(travelSquad.squad, travelSquad.transform.position);
        squadData.guid = travelSquad.guid;
        return squadData;
    }

    public TravelSquadData(PlayerSquad playerSquad, Vector3 squadPosition)
    {
        squad = playerSquad;
        position = squadPosition;
        destination = squadPosition;
        speed = 1f;
    }
}