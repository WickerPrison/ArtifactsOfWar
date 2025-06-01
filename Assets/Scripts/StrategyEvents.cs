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

    public event EventHandler<Stronghold> onSelectStronghold;
    public event EventHandler onDeselectStronghold;
    public event EventHandler<Stronghold> onUpdateStrongholdUnits;
    public event EventHandler<int> onMoneyChange;
    public event EventHandler<StrategyPath> onCreatePath;
    public event EventHandler<PlayerUnitStats> onAddUnitToSquad;

    public void SelectStronghold(Stronghold stronghold)
    {
        onSelectStronghold?.Invoke(this, stronghold);
    }

    public void DeselectStronghold()
    {
        onDeselectStronghold?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateStrongholdUnits(Stronghold stronghold)
    {
        onUpdateStrongholdUnits?.Invoke(this, stronghold);
    }

    public void SetMoney(int amount)
    {
        onMoneyChange?.Invoke(this, amount);
    }

    public void CreatePath(StrategyPath strategyPath)
    {
        onCreatePath?.Invoke(this, strategyPath);
    }

    public void AddUnitToSquad(PlayerUnitStats unit)
    {
        onAddUnitToSquad?.Invoke(this, unit);
    }
}
