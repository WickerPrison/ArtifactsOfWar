using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public enum ActionMap 
{
    STRATEGY
}

public class InputManager : MonoBehaviour
{
    [System.NonSerialized] public InputSystem_Actions inputActions;
    TwoWayMap<ActionMap, InputActionMap> mapMap = new TwoWayMap<ActionMap, InputActionMap>();

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        mapMap.Add(ActionMap.STRATEGY, inputActions.Strategy);
        inputActions.Strategy.RightClick.performed += ctx => StrategyEvents.Instance.DeselectAll();
    }

    public void SetMap(ActionMap actionMap)
    {
        DisableAll();
        mapMap.Get(actionMap).Enable();
    }

    void DisableAll()
    {
        mapMap.Foreach(map => map.Disable());
    }

    private void OnDisable()
    {
        DisableAll();
    }
}
