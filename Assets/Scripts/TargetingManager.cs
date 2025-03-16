using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TargetingManager : MonoBehaviour
{
    EnactPlayerAbilities enactPlayerAbilities;

    private void Start()
    {
        enactPlayerAbilities = GetComponent<EnactPlayerAbilities>();
    }

    public void StartTargeting(Ability ability, PlayerUnit activeUnit)
    {
        switch (ability.targetingType)
        {
            case TargetingType.EMPTY_SPACE:
                foreach(UnitSlot slot in UnitSlotGroups.Instance.playerBackline)
                {
                    if (slot.IsEmpty()) slot.Activate(() =>
                    {
                        enactPlayerAbilities.EnactAbility(slot, activeUnit, ability);
                    });
                }
                foreach(UnitSlot slot in UnitSlotGroups.Instance.playerFrontline)
                {
                    if (slot.IsEmpty()) slot.Activate(() =>
                    {
                        enactPlayerAbilities.EnactAbility(slot, activeUnit, ability);
                    });
                }
                break;
            case TargetingType.SELF:
                foreach (UnitSlot slot in UnitSlotGroups.Instance.allPlayerSlots)
                {
                    if(!slot.IsEmpty() && (Object)slot.occupation == activeUnit)
                    {
                        slot.Activate(() =>
                        {
                            enactPlayerAbilities.EnactAbility(slot, activeUnit, ability);
                        });
                        break;
                    }
                }
                break;
            case TargetingType.FRONTLINE_ENEMY:
                foreach(UnitSlot slot in UnitSlotGroups.Instance.enemyFrontline)
                {
                    if(!slot.IsEmpty()) slot.Activate(() =>
                    {
                        enactPlayerAbilities.EnactAbility(slot, activeUnit, ability);
                    });
                }
                break;
            case TargetingType.BACKLINE_ENEMY:
                foreach(UnitSlot slot in UnitSlotGroups.Instance.enemyBackline)
                {
                    if (!slot.IsEmpty()) slot.Activate(() =>
                    {
                        enactPlayerAbilities.EnactAbility(slot, activeUnit, ability);
                    });
                }
                break;
        }
    }

    public UnitSlot EnemyTargeting(EnemyTargetingType targetingType)
    {
        switch (targetingType)
        {
            case EnemyTargetingType.RANDOM_FRONTLINE:
                List<UnitSlot> occupiedSlots = GetOccupiedSlots(UnitSlotGroups.Instance.playerFrontline);
                if (occupiedSlots.Count == 0) return null;
                int randInt = Random.Range(0, occupiedSlots.Count);
                return occupiedSlots[randInt];
            default: return null;
        }
    }

    List<UnitSlot> GetOccupiedSlots(UnitSlot[] row)
    {
        List<UnitSlot> output = GetOccupiedSlotsInRow(row);
        if (output.Count == 0)
        {
            output = GetOccupiedSlotsInRow(UnitSlotGroups.Instance.playerCollapsedline);
        }
        return output;
    }

    List<UnitSlot> GetOccupiedSlotsInRow(UnitSlot[] row)
    {
        List<UnitSlot> output = new List<UnitSlot>();
        foreach (UnitSlot slot in row)
        {
            if (!slot.IsEmpty())
            {
                output.Add(slot);
            }
        }
        return output;
    }
}
