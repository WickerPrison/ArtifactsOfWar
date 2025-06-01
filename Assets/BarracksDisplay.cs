using UnityEngine;
using System.Collections.Generic;

public class BarracksDisplay : MonoBehaviour
{
    [SerializeField] GameObject displayUnitPrefab;
    List<UnitMenuCard> units = new List<UnitMenuCard>();
    UnitCardButton buttonMode = UnitCardButton.NONE;

    public void UpdateDisplay(Stronghold stronghold)
    {
        units.Clear();
        foreach (PlayerUnitStats unit in stronghold.GetBarracksCount())
        {
            UnitMenuCard unitCard = Instantiate(displayUnitPrefab).GetComponent<UnitMenuCard>();
            unitCard.transform.SetParent(transform);
            unitCard.unitStats = unit;
            unitCard.stronghold = stronghold;
            units.Add(unitCard);
        }
        ApplyButtonMode();
    }

    public void ClearUnits()
    {
        foreach (UnitMenuCard unitCard in units)
        {
            Destroy(unitCard.gameObject);
        }
        units.Clear();
    }

    public void SetButtonMode(UnitCardButton newButtonMode)
    {
        buttonMode = newButtonMode;
        ApplyButtonMode();
    }

    void ApplyButtonMode()
    {
        foreach (UnitMenuCard unitCard in units)
        {
            unitCard.SetButtonMode(buttonMode);
        }
    }
}
