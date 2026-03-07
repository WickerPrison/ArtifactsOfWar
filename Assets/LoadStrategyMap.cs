using UnityEngine;

public class LoadStrategyMap : MonoBehaviour
{
    [SerializeField] GameObject strongholdPrefab;

    void Start()
    {
        LoadStrongholds();
    }

    void LoadStrongholds()
    {
        foreach(StrongholdData stronghold in PersistData.strongholds)
        {
            Stronghold.Create(strongholdPrefab, stronghold);
        }
    }
}
