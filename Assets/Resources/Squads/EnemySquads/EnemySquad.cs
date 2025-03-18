using UnityEngine;

[CreateAssetMenu(fileName = "EnemySquad", menuName = "Scriptable Objects/EnemySquad")]
public class EnemySquad : ScriptableObject
{
    public GameObject[] frontline;
    public GameObject[] backline;
}
