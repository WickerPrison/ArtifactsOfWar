using UnityEngine;

public interface ITakeTurns
{
    public void StartTurn();
    public void EndTurn();
    GameState turnState { get; set; }
    GameObject gameObject { get; }
}
