using UnityEngine;

public enum EnemyAbilityType
{
    ATTACK, MOVEMENT
}

public enum EnemyTargetingType
{
    RANDOM_FRONTLINE
}

[CreateAssetMenu(fileName = "EnemyAbliity", menuName = "Scriptable Objects/EnemyAbliity")]
public class EnemyAbility : ScriptableObject
{
    public string abilityName;
    public EnemyTargetingType targetingType;
    public EnemyAbilityType abilityType;
    public int damage;
}
