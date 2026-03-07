using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestScenario", menuName = "Scriptable Objects/TestScenario")]
public class TestScenario : ScriptableObject
{
    public bool strongholdsRandomlyGenerateOnLoad;
    public List<TestStronghold> strongholds;
}
