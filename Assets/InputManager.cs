using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public enum ActionMap 
{
    STRATEGY
}

public class InputManager : MonoBehaviour
{
    InputSystem_Actions inputActions;
    TwoWayMap<ActionMap, InputActionMap> mapMap = new TwoWayMap<ActionMap, InputActionMap>();

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        mapMap.Add(ActionMap.STRATEGY, inputActions.Strategy);
        inputActions.Strategy.RightClick.performed += ctx => StrategyEvents.Instance.DeselectAll();
    }

    public void SetMap(ActionMap actionMap)
    {
        mapMap.Foreach(map => map.Disable());
        mapMap.Get(actionMap).Enable();
    }
}
