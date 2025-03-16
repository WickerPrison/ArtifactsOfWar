using UnityEngine;
using System;

public class EnemyMovementEventArgs : EventArgs
{
    public EnemyUnit enemyUnit;
    public UnitRow direction;
    public EnemyMovementEventArgs(EnemyUnit EnemyUnit, UnitRow Direction)
    {
        enemyUnit = EnemyUnit;
        direction = Direction;
    }
}
