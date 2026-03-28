using UnityEngine;

public class LoadStrategyMap : MonoBehaviour
{
    [SerializeField] GameObject strongholdPrefab;
    [SerializeField] GameObject encounterPrefab;
    [SerializeField] GameObject travelSquadPrefab;

    void Start()
    {
        LoadStrongholds();
        LoadEncounters();
        LoadTravelSquads();
    }

    void LoadStrongholds()
    {
        int recruitsToGenerate = 0;
        if (PersistData.strongholdsRandomlyGenerateOnLoad)
        {
            recruitsToGenerate = 5;
            PersistData.strongholdsRandomlyGenerateOnLoad = false;
        }
        foreach(StrongholdData stronghold in PersistData.strongholds)
        {
            Stronghold.Create(strongholdPrefab, stronghold, recruitsToGenerate);
        }
    }

    void LoadEncounters()
    {
        foreach(Encounter encounter in PersistData.encounters)
        {
            MapEncounter.Create(encounterPrefab, encounter);
        }
    }

    void LoadTravelSquads()
    {
        foreach(TravelSquadData travelSquad in PersistData.travelSquads)
        {
            TravelSquad.Create(travelSquadPrefab, travelSquad);
        }
    }
}
