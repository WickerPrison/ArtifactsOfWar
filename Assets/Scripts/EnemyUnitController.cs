using UnityEngine;

public class EnemyUnitController : MonoBehaviour
{
    EnemyUnit enemyUnit;

    private void Awake()
    {
        enemyUnit = GetComponent<EnemyUnit>();
    }

    public virtual void StartTurn() { }

    private void OnEnable()
    {
        enemyUnit.onStartTurn += EnemyUnit_onStartTurn;
    }

    private void OnDisable()
    {
        enemyUnit.onEndTurn -= EnemyUnit_onStartTurn;
    }

    private void EnemyUnit_onStartTurn(object sender, System.EventArgs e)
    {
        StartTurn();
    }
}
