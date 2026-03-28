using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Encounter", menuName = "Scriptable Objects/Encounter")]
public class Encounter : ScriptableObject, IHaveGuid
{
    public Vector3 position;
    public EnemySquad enemySquad;
    public int encounterMoney;
    Guid _guid;
    public Guid guid
    {
        get
        {
            if (_guid == Guid.Empty)
            {
                _guid = Guid.NewGuid();
            }
            return _guid;
        }
        private set => _guid = value;
    }
}
