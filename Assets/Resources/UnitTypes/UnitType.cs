using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UnitType", menuName = "Scriptable Objects/UnitType")]
public class UnitType : ScriptableObject
{
    public string className;
    public Sprite classImage;
    public UnitRow prefferedRow;
    public int cost;

    public float baseSpeed;
    public int baseHealth;
    public int frontlineArmor;
    public int backlineArmor;
    public int collapsedArmor;

    public List<Ability> frontlineAbilities;
    public List<Ability> backlineAbilities;
    public List<Ability> collapsedAbilities;


    Dictionary<UnitRow, int> _armorDict;
    public Dictionary<UnitRow, int> armorDict
    {
        get
        {
            if(_armorDict == null)
            {
                _armorDict = new Dictionary<UnitRow, int>()
                {
                    { UnitRow.FRONTLINE, frontlineArmor },
                    { UnitRow.BACKLINE, backlineArmor },
                    { UnitRow.COLLAPSED, collapsedArmor }
                };
            }
            return _armorDict;
        }
    }

    Dictionary<UnitRow, List<Ability>> _abilitiesDict;
    public Dictionary<UnitRow, List<Ability>> abilitiesDict 
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
}
