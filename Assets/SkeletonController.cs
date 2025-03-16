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
        GameManager.Instance.EndTurn();
    }
}
