using UnityEngine;
using System;

public class GlobalEvents : MonoBehaviour
{
    private static GlobalEvents _instance;
    public static GlobalEvents Instance { get { return _instance; } }

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

    public event EventHandler<EnemyUnit> onEnemyDeath;
    public event EventHandler<PlayerUnit> onPlayerDeath;
    public event EventHandler<UnitRow> onPlayerFlip;
    public event EventHandler<UnitRow> onEnemyFlip;
    public event EventHandler<EnemyMovementEventArgs> onEnemyUncollapse;

    public void OnEnemyDeath(EnemyUnit enemyUnit)
    {
        onEnemyDeath?.Invoke(this, enemyUnit);
    }

    public void OnPlayerDeath(PlayerUnit playerUnit)
    {
        onPlayerDeath?.Invoke(this, playerUnit);
    }

    public void OnPlayerFlip(UnitRow destination)
    {
        onPlayerFlip?.Invoke(this, destination);
    }

    public void OnEnemyFlip(UnitRow destination)
    {
        onEnemyFlip?.Invoke(this, destination);
    }

    public void OnEnemyUncollapse(EnemyUnit enemyUnit, UnitRow direction)
    {
        onEnemyUncollapse?.Invoke(this, new EnemyMovementEventArgs(enemyUnit, direction));
    }
}
