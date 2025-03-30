using System;
using UnityEngine;

public class StrategyEvents : MonoBehaviour
{
    private static StrategyEvents _instance;
    public static StrategyEvents Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public event EventHandler<Stronghold> onOpenStrongholdMenu;
    public event EventHandler<Stronghold> onUpdateStrongholdUnits;
    public event EventHandler<int> onMoneyChange;

    public void OpenStrongholdMenu(Stronghold stronghold)
    {
        onOpenStrongholdMenu?.Invoke(this, stronghold);
    }

    public void UpdateStrongholdUnits(Stronghold stronghold)
    {
        onUpdateStrongholdUnits?.Invoke(this, stronghold);
    }

    public void SetMoney(int amount)
    {
        onMoneyChange?.Invoke(this, amount);
    }
}
