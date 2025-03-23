using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecruitUnit : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI unitName;
    [SerializeField] TextMeshProUGUI className;
    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] TextMeshProUGUI defense;
    [SerializeField] Image unitImage;
    [SerializeField] TextMeshProUGUI cost;
    PlayerUnitStats unitStats;
    string backlineColor;
    string frontlineColor;
    string collapsedColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unitStats = GenerateNewRecruit.Instance.GetNewRecruit();
        backlineColor = "#" + ColorUtility.ToHtmlStringRGB(StrategyManager.Instance.colorScheme.backline);
        frontlineColor = "#" + ColorUtility.ToHtmlStringRGB(StrategyManager.Instance.colorScheme.frontline);
        collapsedColor = "#" + ColorUtility.ToHtmlStringRGB(StrategyManager.Instance.colorScheme.collapsed);
        UpdateUI();
    }

    void UpdateUI()
    {
        unitName.text = unitStats.unitName;
        className.text = $"Class: {unitStats.unitType.className}";
        hp.text = $"HP: {unitStats.maxHealth}";
        speed.text = $"Speed: {unitStats.speed}";
        defense.text = $"Defense: " +
            $"<color={backlineColor}>{unitStats.unitType.backlineArmor}</color> " +
            $"<color={collapsedColor}>{unitStats.unitType.collapsedArmor}</color> " +
            $"<color={frontlineColor}>{unitStats.unitType.frontlineArmor}</color>";
        unitImage.sprite = unitStats.unitType.classImage;
        cost.text = $"Recruit: {unitStats.unitType.cost}";
    }

    public void BuyUnit()
    {
        if(PersistData.money >= unitStats.unitType.cost)
        {
            PersistData.money -= unitStats.unitType.cost;
            PersistData.army.Add(unitStats);
            Destroy(gameObject);
        }
    }
}
