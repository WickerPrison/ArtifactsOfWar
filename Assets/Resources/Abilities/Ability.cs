using UnityEngine;
using System.Collections.Generic;

public enum AbilityType
{
    MOVEMENT, ATTACK
}

public enum TargetingType
{
    EMPTY_SPACE, SELF, FRONTLINE_ENEMY, BACKLINE_ENEMY, ENEMY
}

[CreateAssetMenu(fileName = "Ability", menuName = "Scriptable Objects/Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;
    public AbilityType abilityType;
    public TargetingType targetingType;

    // Movement only properties
    public UnitRow uncollapseDirection;

    // Attack only properties
    public int damage;
}
