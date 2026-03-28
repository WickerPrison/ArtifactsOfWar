using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestStronghold", menuName = "Scriptable Objects/TestStronghold")]
public class TestStronghold : ScriptableObject
{
    public Vector3 position;
    public List<UnitType> barracks;
    public List<UnitType> availableUnits;
}
