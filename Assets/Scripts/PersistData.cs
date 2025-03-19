using UnityEngine;

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
}
