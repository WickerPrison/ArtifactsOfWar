using UnityEngine;
using System.Collections.Generic;


public class PlayerUnitStats
{
    public UnitType unitType { get; private set; }
    public float speed;
    public float turnMeter;
    public int maxHealth;
    public int health;

    public PlayerUnitStats(UnitType type)
    {
        unitType = type;
        speed = type.baseSpeed;
        turnMeter = Random.Range(0, 100);
        maxHealth = type.baseHealth;
        health = maxHealth;
    }

    public List<Ability> GetAbilities(UnitRow row)
    {
        return unitType.GetAbilities(row);
    }
}
