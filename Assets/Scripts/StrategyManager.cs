using UnityEngine;

public enum StrategyState
{
    UNSELECTED, STRONGHOLD
}

public class StrategyManager : MonoBehaviour
{
    private static StrategyManager _instance;
    public static StrategyManager Instance { get { return _instance; } }

    public ColorScheme colorScheme;
    [System.NonSerialized] public StrategyState strategyState;
    [SerializeField] GameObject pathPrefab;
    Stronghold stronghold;
    [System.NonSerialized] public int mustInteracts;
    [System.NonSerialized] public DropSpot dropSpot;

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

    void Start()
    {
        strategyState = StrategyState.UNSELECTED;
        PersistData.money = 500;
    }

    public void NextDay()
    {
        if (mustInteracts > 0) return; 
        StrategyEvents.Instance.NextDay();
    }

    public void CreatePath(IAmDestination destination)
    {
        StrategyPath strategyPath = Instantiate(pathPrefab).GetComponent<StrategyPath>();
        strategyPath.Setup(stronghold, destination);
        StrategyEvents.Instance.CreatePath(strategyPath);
    }

    private void Strategy_onSelectStronghold(object sender, Stronghold selectedStronghold)
    {
        strategyState = StrategyState.STRONGHOLD;
        stronghold = selectedStronghold;
    }

    private void Strategy_onDeselectStronghold(object sender, System.EventArgs e)
    {
        strategyState = StrategyState.UNSELECTED;
        stronghold = null;
    }

    private void OnEnable()
    {
        StrategyEvents.Instance.onSelectStronghold += Strategy_onSelectStronghold;
        StrategyEvents.Instance.onDeselectStronghold += Strategy_onDeselectStronghold;
    }

    private void OnDisable()
    {
        StrategyEvents.Instance.onSelectStronghold -= Strategy_onSelectStronghold;
        StrategyEvents.Instance.onDeselectStronghold -= Strategy_onDeselectStronghold;
    }
}
