using UnityEngine;
using System.Collections.Generic;

public static class CombatUtils
{
    public static readonly Dictionary<UnitRow, UnitRow> oppositeRow = new Dictionary<UnitRow, UnitRow>()
    {
        {UnitRow.FRONTLINE, UnitRow.BACKLINE },
        {UnitRow.BACKLINE, UnitRow.FRONTLINE }
    };
}
