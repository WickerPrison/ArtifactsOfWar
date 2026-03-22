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
    TwoWayMap<ActionMap, InputActionMap> mapMap;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        mapMap.Add(ActionMap.STRATEGY, inputActions.Strategy);
    }

    public void SetMap(ActionMap actionMap)
    {
        mapMap.Foreach(map => map.Disable());
        mapMap.Get(actionMap).Enable();
    }
}
