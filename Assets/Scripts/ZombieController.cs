using UnityEngine;
using System;
using System.Collections;

public class ZombieController : EnemyUnitController
{
    [SerializeField] EnemyAbility zombieClaw;

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
                ClawAttack();
                break;
            case UnitRow.COLLAPSED:
                bool moveForward = false;
                foreach(UnitSlot slot in UnitSlotGroups.Instance.enemyCollapsedline)
                {
                    if (slot.occupation.gameObject.GetComponent<EnemyUnit>().prefferedRow == UnitRow.BACKLINE)
                    {
                        moveForward = MoveForward();
                        break;
                    }
                }
                if (!moveForward) ClawAttack();
                break;
            case UnitRow.BACKLINE:
                MoveForward();
                break;
        }
        GameManager.Instance.EndTurn();
    }

    void ClawAttack()
    {
        UnitSlot targetSlot = targetingManager.EnemyTargeting(zombieClaw.targetingType);
        if (targetSlot != null)
        {
            IAmUnit playerUnit = targetSlot.occupation;
            playerUnit.LoseHealth(zombieClaw.damage);
        }
    }
}
