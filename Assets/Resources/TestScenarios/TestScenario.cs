using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestScenario", menuName = "Scriptable Objects/TestScenario")]
public class TestScenario : ScriptableObject
{
    public bool strongholdsRandomlyGenerateOnLoad;
    public int money;
    public List<TestStronghold> strongholds;
    public List<Encounter> encounters;
    public List<TestTravelSquad> travelSquads;
}
