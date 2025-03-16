using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class AbilitiesHolder : MonoBehaviour
{
    [SerializeField] GameObject endTurnButton;
    [SerializeField] GameObject abilityButtonPrefab;
    List<AbilityUI> abilityButtons = new List<AbilityUI>();
    [System.NonSerialized] public PlayerUnit playerUnit;
    EnemyUnit enemyUnit;

    private void Gm_onNewTurn(object sender, ITakeTurns takeTurns)
    {
        if (takeTurns.turnState == GameState.PLAYER_TURN)
        {
            enemyUnit = null;
            playerUnit = takeTurns.gameObject.GetComponent<PlayerUnit>();
            ClearButtons();
            SetupUnitAbilities();
            endTurnButton.SetActive(true);
            endTurnButton.transform.SetAsLastSibling();
        }
        else
        {
            playerUnit = null;
            enemyUnit = takeTurns.gameObject.GetComponent<EnemyUnit>();
            ClearButtons();
        }
    }

    void ClearButtons()
    {
        foreach (AbilityUI button in abilityButtons)
        {
            Destroy(button.gameObject);
        }
        abilityButtons.Clear();
        endTurnButton.SetActive(false);
    }

    void SetupUnitAbilities()
    {
        List<Ability> unitAbilities = playerUnit.GetAbilities();
        foreach(Ability ability in unitAbilities)
        {
            AbilityUI abilityButton = Instantiate(abilityButtonPrefab).GetComponent<AbilityUI>();
            abilityButton.ability = ability;
            abilityButton.abilitiesHolder = this;
            abilityButtons.Add(abilityButton);
            abilityButton.transform.SetParent(transform);
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.onNewTurn += Gm_onNewTurn;
    }

    private void OnDisable()
    {
        GameManager.Instance.onNewTurn -= Gm_onNewTurn;
    }
}
