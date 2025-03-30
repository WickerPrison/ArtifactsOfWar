using UnityEngine;
using System.Collections.Generic;


public class PlayerUnitStats
{
    public UnitType unitType { get; private set; }
    public float speed;
    public float turnMeter;
    public int maxHealth;
    public int health;
    public string unitName;
    public string id;

    public PlayerUnitStats(UnitType type)
    {
        unitType = type;
        speed = type.baseSpeed;
        turnMeter = Random.Range(0, 100);
        maxHealth = type.baseHealth;
        health = maxHealth;
        id = System.Guid.NewGuid().ToString();
    }

    public List<Ability> GetAbilities(UnitRow row)
    {
        return unitType.abilitiesDict[row];
    }

    public int GetArmor(UnitRow row)
    {
        return unitType.armorDict[row];
    }
}
