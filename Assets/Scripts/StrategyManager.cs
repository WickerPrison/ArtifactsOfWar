using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public enum StrategyState
{
    UNSELECTED, STRONGHOLD, SQUAD
}

public class StrategyManager : MonoBehaviour
{
    private static StrategyManager _instance;
    public static StrategyManager Instance { get { return _instance; } }

    public ColorScheme colorScheme;
    [System.NonSerialized] public StrategyState strategyState;
    [SerializeField] GameObject pathPrefab;
    [System.NonSerialized] public Stronghold stronghold;
    [System.NonSerialized] public TravelSquad travelSquad;
    [System.NonSerialized] public int mustInteracts;
    [System.NonSerialized] public DropSpot dropSpot;
    InputManager inputManager;

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

        inputManager = GetComponent<InputManager>();
        inputManager.SetMap(ActionMap.STRATEGY);
    }

    void Start()
    {
        strategyState = StrategyState.UNSELECTED;
        StrategyEvents.Instance.ChangeStrategyState(strategyState);
    }

    public void NextDay()
    {
        if (mustInteracts > 0) return; 
        DeselectAll();
        StrategyEvents.Instance.NextDay();
    }

    private void Strategy_onSelectStronghold(object sender, Stronghold selectedStronghold)
    {
        strategyState = StrategyState.STRONGHOLD;
        StrategyEvents.Instance.ChangeStrategyState(strategyState);
        stronghold = selectedStronghold;
        travelSquad = null;
    }

    private void Strategy_onDeselectAll(object sender, System.EventArgs e)
    {
        DeselectAll();
    }

    void DeselectAll()
    {
        strategyState = StrategyState.UNSELECTED;
        StrategyEvents.Instance.ChangeStrategyState(StrategyState.UNSELECTED);
        stronghold = null;
        travelSquad = null;
    }

    private void Strategy_onSelectTravelSquad(object sender, TravelSquad selectedTravelSquad)
    {
        strategyState = StrategyState.SQUAD;
        StrategyEvents.Instance.ChangeStrategyState(StrategyState.SQUAD);
        stronghold = null;
        travelSquad = selectedTravelSquad;
    }

    private void OnEnable()
    {
        StrategyEvents.Instance.onSelectStronghold += Strategy_onSelectStronghold;
        StrategyEvents.Instance.onSelectTravelSquad += Strategy_onSelectTravelSquad;
        StrategyEvents.Instance.onDeselectAll += Strategy_onDeselectAll;
    }

    private void OnDisable()
    {
        StrategyEvents.Instance.onSelectStronghold -= Strategy_onSelectStronghold;
        StrategyEvents.Instance.onSelectTravelSquad -= Strategy_onSelectTravelSquad;
        StrategyEvents.Instance.onDeselectAll -= Strategy_onDeselectAll;
    }
}
