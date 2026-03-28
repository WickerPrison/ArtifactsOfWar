using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSquad", menuName = "Scriptable Objects/PlayerSquad")]
public class PlayerSquadTesting : ScriptableObject
{
    public UnitType[] frontline;
    public UnitType[] backline;
}
