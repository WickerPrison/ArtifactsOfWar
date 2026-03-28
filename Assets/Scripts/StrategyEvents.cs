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
    public event EventHandler<TravelSquad> onSelectTravelSquad;
    public event EventHandler<Stronghold> onUpdateStrongholdUnits;
    public event EventHandler<int> onMoneyChange;
    public event EventHandler<PlayerUnitStats> onAddUnitToSquad;
    public event EventHandler onNextDay;
    public event EventHandler onLoadToPersistData;
    public event EventHandler<StrategyState> onChangeStrategyState;
    public event EventHandler onDeselectAll;
    public event EventHandler<TravelSquad> onUpdateSquadPosition;

    public void SelectStronghold(Stronghold stronghold)
    {
        onSelectStronghold?.Invoke(this, stronghold);
    }

    public void SelectTravelSquad(TravelSquad travelSquad)
    {
        onSelectTravelSquad?.Invoke(this, travelSquad);
    }

    public void UpdateStrongholdUnits(Stronghold stronghold)
    {
        onUpdateStrongholdUnits?.Invoke(this, stronghold);
    }

    public void SetMoney(int amount)
    {
        onMoneyChange?.Invoke(this, amount);
    }

    public void NextDay()
    {
        onNextDay?.Invoke(this, EventArgs.Empty);
    }

    public void LoadToPersistData()
    {
        onLoadToPersistData?.Invoke(this, EventArgs.Empty);
    }

    public void ChangeStrategyState(StrategyState newState)
    {
        onChangeStrategyState?.Invoke(this, newState);
    }

    public void DeselectAll()
    {
        onDeselectAll?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateSquadPosition(TravelSquad squad)
    {
        onUpdateSquadPosition?.Invoke(this, squad);
    }
}
