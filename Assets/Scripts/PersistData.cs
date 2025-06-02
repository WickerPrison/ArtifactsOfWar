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

    public static List<PlayerUnitStats> combatSquad = new List<PlayerUnitStats>();
    public static EnemySquad enemySquad;
}
