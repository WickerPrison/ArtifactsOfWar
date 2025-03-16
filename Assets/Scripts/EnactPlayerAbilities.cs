using UnityEngine;

public class EnactPlayerAbilities : MonoBehaviour
{
    PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void EnactAbility(UnitSlot targetSlot, PlayerUnit playerUnit, Ability ability)
    {
        switch (ability.abilityType)
        {
            case AbilityType.MOVEMENT:
                MoveAbility(targetSlot, playerUnit, ability);
                break;
            case AbilityType.ATTACK:
                AttackAbility(targetSlot, playerUnit, ability);
                break;
        }
    }

    void MoveAbility(UnitSlot targetSlot, PlayerUnit playerUnit, Ability ability)
    {
        if(ability.uncollapseDirection != UnitRow.NONE)
        {
            playerController.Uncollapse(playerUnit, ability.uncollapseDirection);
        }
        else
        {
            playerUnit.MoveToSlot(targetSlot);
        }
        GameManager.Instance.EndTurn();
    }

    void AttackAbility(UnitSlot targetSlot, PlayerUnit playerUnit, Ability ability)
    {
        targetSlot.occupation.LoseHealth(ability.damage);
        GameManager.Instance.EndTurn();
    }
}
