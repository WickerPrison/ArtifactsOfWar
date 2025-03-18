using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSquad", menuName = "Scriptable Objects/PlayerSquad")]
public class PlayerSquad : ScriptableObject
{
    public UnitType[] frontline;
    public UnitType[] backline;
}
