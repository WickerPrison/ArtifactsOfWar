using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UnitType", menuName = "Scriptable Objects/UnitType")]
public class UnitType : ScriptableObject
{
    public string className;
    public Sprite classImage;
    public UnitRow prefferedRow;
    public float baseSpeed;
    public int baseHealth;

    public List<Ability> frontlineAbilities;
    public List<Ability> backlineAbilities;
    public List<Ability> collapsedAbilities;
    Dictionary<UnitRow, List<Ability>> _abilitiesDict;
    Dictionary<UnitRow, List<Ability>> abilitiesDict 
    { 
        get
        {
            if(_abilitiesDict == null)
            {
                _abilitiesDict = new Dictionary<UnitRow, List<Ability>>()
                {
                    { UnitRow.FRONTLINE, frontlineAbilities },
                    { UnitRow.BACKLINE, backlineAbilities },
                    { UnitRow.COLLAPSED, collapsedAbilities }
                };
            }
            return _abilitiesDict;
        } 
    }

    public List<Ability> GetAbilities(UnitRow row)
    {
        return abilitiesDict[row];
    }
}
