using UnityEngine;

public class GenerateNewRecruit : MonoBehaviour
{
    private static GenerateNewRecruit _instance;
    public static GenerateNewRecruit Instance { get { return _instance; } }

    [SerializeField] UnitType[] possibleTypes;
    string[] firstNames = { "Steve", "Bob", "Susan", "Larry" };
    string[] lastNames = {"Stevenson", "Smith", "Zhang", "Black"};

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public PlayerUnitStats GetNewRecruit()
    {
        PlayerUnitStats unitStats = new PlayerUnitStats(GetUnitType());
        unitStats.unitName = GetRandomName();
        return unitStats;
    }

    UnitType GetUnitType()
    {
        int randInt = Random.Range(0, possibleTypes.Length);
        return possibleTypes[randInt];
    }

    string GetRandomName()
    {
        string name = "";
        int randInt = Random.Range(0, firstNames.Length);
        name += firstNames[randInt];
        randInt = Random.Range(0, lastNames.Length);
        name += " " + lastNames[randInt];
        return name;
    }
}
