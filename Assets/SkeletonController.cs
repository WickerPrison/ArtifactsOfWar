using UnityEngine;
using System.Collections;

public class SkeletonController : EnemyUnitController
{
    public override void StartTurn()
    {
        base.StartTurn();
        StartCoroutine(DummyTurn());
    }

    IEnumerator DummyTurn()
    {
        yield return new WaitForSeconds(0.5f);
        if(enemyUnit.row == UnitRow.COLLAPSED && enemyUnit.squad.Count > 1)
        {
            GlobalEvents.Instance.OnEnemyUncollapse(enemyUnit, UnitRow.BACKLINE);
        }
        GameManager.Instance.EndTurn();
    }
}
