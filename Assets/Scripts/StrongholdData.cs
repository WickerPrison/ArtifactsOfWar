using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class StrongholdData : IHaveGuid
{
    public static StrongholdData CreateData(Stronghold stronghold)
    {
        StrongholdData data = new StrongholdData();
        data.availableRecruits = new List<PlayerUnitStats>(stronghold.availableRecruits);
        data.barracks = new List<PlayerUnitStats>(stronghold.barracks);
        data.position = stronghold.transform.position;
        data.guid = stronghold.guid;
        return data;
    }

    public static StrongholdData CreateFromTestStronghold(TestStronghold testStronghold)
    {
        StrongholdData data = new StrongholdData();
        data.availableRecruits = testStronghold.availableUnits.Select(unitType => new PlayerUnitStats(unitType)).ToList();
        data.barracks = testStronghold.barracks.Select(unitType => new PlayerUnitStats(unitType)).ToList();
        data.position = testStronghold.position;
        return data;
    }

    public Vector2 position;
    public List<PlayerUnitStats> availableRecruits;
    public List<PlayerUnitStats> barracks;
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
}
