using UnityEngine;
using System.Collections;

public class SkeletonController : EnemyUnitController
{
    [SerializeField] EnemyAbility bowShot;

    public override void StartTurn()
    {
        base.StartTurn();
        StartCoroutine(DummyTurn());
    }

    IEnumerator DummyTurn()
    {
        yield return new WaitForSeconds(0.5f);

        switch (enemyUnit.row)
        {
            case UnitRow.FRONTLINE:
                MoveBackward();
                break;
            case UnitRow.COLLAPSED:
                if(enemyUnit.squad.Count > 1)
                {
                    GlobalEvents.Instance.OnEnemyUncollapse(enemyUnit, UnitRow.BACKLINE);
                }
                break;
            case UnitRow.BACKLINE:
                BowShot();
                break;
        }
        GameManager.Instance.EndTurn();
    }

    void BowShot()
    {
        UnitSlot targetSlot = targetingManager.EnemyTargeting(bowShot.targetingType);
        if (targetSlot != null)
        {
            IAmUnit playerUnit = targetSlot.occupation;
            playerUnit.LoseHealth(bowShot.damage);
        }
    }
}
