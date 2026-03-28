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
    public static List<StrongholdData> strongholds = new List<StrongholdData>();
    public static bool strongholdsRandomlyGenerateOnLoad = false;
    public static List<Encounter> encounters = new List<Encounter>();
    public static List<TravelSquadData> travelSquads = new List<TravelSquadData>();

    public static PlayerSquad combatSquad;
    public static EnemySquad enemySquad;
    public static int currentEncounterMoney;

    public static void GainEncounterRewards()
    {
        money += currentEncounterMoney;
        currentEncounterMoney = 0;
    }

    public static void SetMoneyNoEvent(int newTotal)
    {
        _money = newTotal;
    }
}
