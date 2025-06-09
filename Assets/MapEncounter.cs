using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MapEncounter : MonoBehaviour, IAmDestination
{
    [SerializeField] GameObject startButton;
    [SerializeField] EnemySquad enemySquad;
    PlayerSquad playerSquad;

    private void OnMouseDown()
    {
        if (StrategyManager.Instance.strategyState == StrategyState.STRONGHOLD)
        {
            StrategyManager.Instance.CreatePath(this);
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
        SceneManager.LoadScene("Combat");
    }
}
