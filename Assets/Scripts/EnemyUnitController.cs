using UnityEngine;

public class EnemyUnitController : MonoBehaviour
{
    [System.NonSerialized] public EnemyUnit enemyUnit;
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

    public bool MoveForward()
    {
        return MoveToRow(UnitSlotGroups.Instance.enemyFrontline);
    }

    public bool MoveBackward()
    {
        return MoveToRow(UnitSlotGroups.Instance.enemyBackline);
    }

    bool MoveToRow(UnitSlot[] line)
    {
        for (int i = 0; i < line.Length; i++)
        {
            if (line[i].IsEmpty())
            {
                enemyUnit.MoveToSlot(line[i]);
                return true;
            }
        }
        return false;
    }

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
