using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum TravelSquadState
{
    UNSELECTED, SELECTED, CHOOSE_DESTINATION
}

public class TravelSquad : MonoBehaviour
{
    public PlayerSquad squad;
    [System.NonSerialized] public Vector3 destination;
    float speed = 1f;
    bool instantiatedCorrectly = false;
    [SerializeField] SpriteRenderer selected;
    [SerializeField] SpriteRenderer destinationMarker;
    TravelSquadState state = TravelSquadState.UNSELECTED;

    public static TravelSquad Create(GameObject prefab, TravelSquadData data)
    {
        TravelSquad squad = Instantiate(prefab).GetComponent<TravelSquad>();
        squad.squad = data.squad;
        squad.destination = data.destination;
        squad.speed = data.speed;
        squad.transform.position = data.position;
        squad.instantiatedCorrectly = true;
        return squad;
    }

    private void Awake()
    {
        InputManager inputManager = StrategyEvents.Instance.GetComponent<InputManager>();
        inputManager.inputActions.Strategy.LeftClick.performed += ctx => LeftClick();
    }

    private void Start()
    {
        if (!instantiatedCorrectly) Utils.IncorrectInitialization("TravelSquad");
        selected.enabled = false;
        destinationMarker.enabled = false;
    }

    private void Strategy_onNextDay(object sender, System.EventArgs e)
    {
        if(Vector3.Distance(destination, transform.position) <= speed)
        {
            transform.position = destination;
            destinationMarker.enabled = false;
        }
        else
        {
            Vector3 direction = destination - transform.position;
            transform.position += direction.normalized * speed;
            destinationMarker.transform.position = destination;
        }

        StrategyEvents.Instance.UpdateSquadPosition(this);
    }

    void LeftClick()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (state != TravelSquadState.SELECTED) return;
        Vector3 clickPos = Mouse.current.position.ReadValue();
        clickPos = Camera.main.ScreenToWorldPoint(clickPos);
        clickPos.z = 0;
        SetDestination(clickPos);
    }

    void SetDestination(Vector3 newDestination)
    {
        destination = newDestination;
        destinationMarker.enabled = true;
        destinationMarker.transform.position = destination;
    }

    void Select()
    {
        selected.enabled = true;
        state = TravelSquadState.SELECTED;
        if(destination != transform.position)
        {
            destinationMarker.enabled = true;
        }
    }

    void Unselect()
    {
        selected.enabled = false;
        state = TravelSquadState.UNSELECTED;
        destinationMarker.enabled = false;
    }

    private void OnMouseDown()
    {
        if (StrategyManager.Instance.strategyState == StrategyState.UNSELECTED)
        {
            Select();
            StrategyEvents.Instance.SelectTravelSquad(this);
        }
    }

    private void Strategy_onChangeStrategyState(object sender, StrategyState strategyState)
    {
        if(strategyState != StrategyState.SQUAD)
        {
            Unselect();
        }
    }

    private void OnEnable()
    {
        StrategyEvents.Instance.onNextDay += Strategy_onNextDay;
        StrategyEvents.Instance.onChangeStrategyState += Strategy_onChangeStrategyState;
    }

    private void OnDisable()
    {
        StrategyEvents.Instance.onNextDay -= Strategy_onNextDay;
        StrategyEvents.Instance.onChangeStrategyState -= Strategy_onChangeStrategyState;
    }
}
