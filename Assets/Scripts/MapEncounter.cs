using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MapEncounter : MonoBehaviour, IAmDestination
{
    [SerializeField] GameObject startButton;
    [SerializeField] EnemySquad enemySquad;
    [SerializeField] int encounterMoney;
    PlayerSquad playerSquad;
    Vector3 position;
    bool instantiatedCorrectly = false;

    public static MapEncounter Create(GameObject prefab, Encounter encounter)
    {
        MapEncounter mapEncounter = Instantiate(prefab).GetComponent<MapEncounter>();
        mapEncounter.position = encounter.position;
        mapEncounter.transform.position = mapEncounter.position;
        mapEncounter.enemySquad = encounter.enemySquad;
        mapEncounter.encounterMoney = encounter.encounterMoney;
        mapEncounter.instantiatedCorrectly = true;
        return mapEncounter;
    }

    private void Start()
    {
        if (!instantiatedCorrectly) Utils.IncorrectInitialization("MapEncounter");
    }

    private void OnMouseDown()
    {
        if (StrategyManager.Instance.strategyState == StrategyState.STRONGHOLD)
        {
            
        }
    }

    public void SquadArrived(PlayerSquad squad)
    {
        StrategyManager.Instance.mustInteracts++;
        playerSquad = squad;
        startButton.SetActive(true);
    }

    public void StartCombat()
    {
        PersistData.combatSquad = playerSquad;
        PersistData.enemySquad = enemySquad;
        PersistData.currentEncounterMoney = encounterMoney;
        StrategyEvents.Instance.LoadToPersistData();
        SceneManager.LoadScene("Combat");
    }
}
