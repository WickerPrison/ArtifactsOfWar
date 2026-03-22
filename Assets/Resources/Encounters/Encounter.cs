using UnityEngine;

[CreateAssetMenu(fileName = "Encounter", menuName = "Scriptable Objects/Encounter")]
public class Encounter : ScriptableObject
{
    public Vector3 position;
    public EnemySquad enemySquad;
    public int encounterMoney;
}
