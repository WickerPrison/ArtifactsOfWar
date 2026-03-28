using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TestTravelSquad", menuName = "Scriptable Objects/TestTravelSquad")]
public class TestTravelSquad : ScriptableObject
{
    public Vector3 position;
    public List<UnitType> frontRow;
    public List<UnitType> backRow;
}
