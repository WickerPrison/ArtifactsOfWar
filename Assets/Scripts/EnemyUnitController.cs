using UnityEngine;

public class EnemyUnitController : MonoBehaviour
{
    EnemyUnit enemyUnit;
    [System.NonSerialized] public TargetingManager targetingManager;

    private void Awake()
    {
        enemyUnit = GetComponent<EnemyUnit>();
    }

    private void Start()
    {
        targetingManager = GameManager.Instance.GetComponent<TargetingManager>();
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
