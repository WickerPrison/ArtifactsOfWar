using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class AbilityUI : MonoBehaviour
{
    public Ability ability;
    [SerializeField] TextMeshProUGUI abilityName;
    [System.NonSerialized] public AbilitiesHolder abilitiesHolder;
    TargetingManager targetingManager;

    private void Start()
    {
        targetingManager = GameManager.Instance.GetComponent<TargetingManager>();
        abilityName.text = ability.abilityName;
    }

    public void ActivateAbility()
    {
        targetingManager.StartTargeting(ability, abilitiesHolder.playerUnit);
    }
}
