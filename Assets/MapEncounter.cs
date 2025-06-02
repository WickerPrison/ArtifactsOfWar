using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MapEncounter : MonoBehaviour, IAmDestination
{
    [SerializeField] GameObject startButton;
    [SerializeField] EnemySquad enemySquad;
    List<PlayerUnitStats> playerSquad;

    private void OnMouseDown()
    {
        if (StrategyManager.Instance.strategyState == StrategyState.STRONGHOLD)
        {
            StrategyManager.Instance.CreatePath(this);
        }
    }

    public void SquadArrived(List<PlayerUnitStats> squad)
    {
        StrategyManager.Instance.mustInteracts++;
        playerSquad = squad;
        startButton.SetActive(true);
    }

    public void StartCombat()
    {
        PersistData.combatSquad = new List<PlayerUnitStats>(playerSquad);
        PersistData.enemySquad = enemySquad;
        SceneManager.LoadScene("Combat");
    }
}
