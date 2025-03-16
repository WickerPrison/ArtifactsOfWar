using UnityEngine;
using System;
using System.Collections;

public class ZombieController : EnemyUnitController
{
    [SerializeField] EnemyAbility zombieClaw;
    TargetingManager targetingManager;

    private void Start()
    {
        targetingManager = GameManager.Instance.GetComponent<TargetingManager>();
    }

    public override void StartTurn()
    {
        base.StartTurn();
        StartCoroutine(DummyTurn());
    }

    IEnumerator DummyTurn()
    {
        yield return new WaitForSeconds(0.5f);
        UnitSlot targetSlot = targetingManager.EnemyTargeting(zombieClaw.targetingType);
        if(targetSlot != null)
        {
            IAmUnit playerUnit = targetSlot.occupation;
            playerUnit.LoseHealth(zombieClaw.damage);
        }
        GameManager.Instance.EndTurn();
    }
}
