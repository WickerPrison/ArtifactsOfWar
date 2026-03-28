using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class MapEncounter : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    [SerializeField] EnemySquad enemySquad;
    [SerializeField] int encounterMoney;
    PlayerSquad playerSquad;
    Vector3 position;
    bool instantiatedCorrectly = false;
    float interactDistance = 1f;
    [System.NonSerialized] public Guid guid;

    public static MapEncounter Create(GameObject prefab, Encounter encounter)
    {
        MapEncounter mapEncounter = Instantiate(prefab).GetComponent<MapEncounter>();
        mapEncounter.position = encounter.position;
        mapEncounter.transform.position = mapEncounter.position;
        mapEncounter.enemySquad = encounter.enemySquad;
        mapEncounter.encounterMoney = encounter.encounterMoney;
        mapEncounter.guid = encounter.guid;
        mapEncounter.instantiatedCorrectly = true;
        return mapEncounter;
    }

    private void Start()
    {
        if (!instantiatedCorrectly) Utils.IncorrectInitialization("MapEncounter");
    }

    void ShowStartButton(bool show)
    {
        startButton.SetActive(show);
    }

    public void StartCombat()
    {
        PersistData.combatSquad = playerSquad;
        PersistData.enemySquad = enemySquad;
        PersistData.currentEncounterMoney = encounterMoney;
        Utils.RemovePersistentList(PersistData.encounters, guid);
        StrategyEvents.Instance.LoadToPersistData();
        SceneManager.LoadScene("Combat");
    }

    private void OnEnable()
    {
        StrategyEvents.Instance.onUpdateSquadPosition += Strategy_onUpdateSquadPosition;
        StrategyEvents.Instance.onChangeStrategyState += Strategy_onChangeStrategyState;
    }

    private void OnDisable()
    {
        StrategyEvents.Instance.onUpdateSquadPosition -= Strategy_onUpdateSquadPosition;   
        StrategyEvents.Instance.onChangeStrategyState -= Strategy_onChangeStrategyState;
    }

    private void Strategy_onUpdateSquadPosition(object sender, TravelSquad travelSquad)
    {
        if(Vector3.Distance(travelSquad.transform.position, transform.position) <= interactDistance)
        {
            playerSquad = travelSquad.squad;
            ShowStartButton(true);
        }
    }

    private void Strategy_onChangeStrategyState(object sender, StrategyState strategyState)
    {
        if (strategyState != StrategyState.SQUAD)
        {
            ShowStartButton(false);
            return;
        }

        if(Vector3.Distance(StrategyManager.Instance.travelSquad.transform.position, transform.position) <= interactDistance)
        {
            ShowStartButton(true);
        }
    }
}
