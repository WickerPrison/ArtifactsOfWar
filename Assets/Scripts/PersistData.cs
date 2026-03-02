using UnityEngine;
using System.Collections.Generic;

public static class PersistData
{
    private static int _money;
    public static int money {
        get { return _money; }
        set 
        {
            StrategyEvents.Instance.SetMoney(value);
            _money = value;
        } 
    }

    public static List<PlayerUnitStats> army = new List<PlayerUnitStats>();
    public static List<StrongholdData> strongholds;

    public static PlayerSquad combatSquad;
    public static EnemySquad enemySquad;
    public static int currentEncounterMoney;

    public static void ClearState()
    {
        strongholds.Clear();
    }

    public static void GainEncounterRewards()
    {
        money += currentEncounterMoney;
        currentEncounterMoney = 0;
    }
}
