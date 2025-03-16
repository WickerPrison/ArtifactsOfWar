using UnityEngine;
using System;
using System.Collections.Generic;

public enum GameState
{
    PLAYER_TURN, ENEMY_TURN, PAUSED, TURN_METER
}

public class GameManager : MonoBehaviour
{
    public ColorScheme colorScheme;
    public Dictionary<UnitRow, Color> rowColorDict;
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    public GameState gameState { get; private set; }
    public event EventHandler onTurnMeter;
    public event EventHandler onEndTurn;
    public event EventHandler<ITakeTurns> onNewTurn;
    public Queue<ITakeTurns> readyForTurn = new Queue<ITakeTurns>();
    ITakeTurns currentTurn;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        rowColorDict = new Dictionary<UnitRow, Color>()
        {
            { UnitRow.FRONTLINE, colorScheme.frontline },
            { UnitRow.BACKLINE, colorScheme.backline },
            { UnitRow.COLLAPSED, colorScheme.collapsed }
        };
    }

    private void Start()
    {
        gameState = GameState.TURN_METER;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.TURN_METER:
                NextTurn();
                break;
        }
    }

    public void NextTurn()
    {
        if(readyForTurn.Count > 0)
        {
            currentTurn = readyForTurn.Dequeue();
            gameState = currentTurn.turnState;
            onNewTurn?.Invoke(this, currentTurn);
            currentTurn.StartTurn();
            return;
        }
        onTurnMeter?.Invoke(this, EventArgs.Empty);
    }

    public void EndTurnButton()
    {
        if(gameState == GameState.PLAYER_TURN)
        {
            EndTurn();
        }
    }

    public void EndTurn()
    {
        onEndTurn?.Invoke(this, EventArgs.Empty);
        currentTurn.EndTurn();
        currentTurn = null;
        gameState = GameState.TURN_METER;
        NextTurn();
    }
}
