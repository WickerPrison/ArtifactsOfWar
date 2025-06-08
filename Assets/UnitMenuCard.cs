using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UnitMenuCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI unitName;
    [SerializeField] TextMeshProUGUI className;
    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] TextMeshProUGUI defense;
    [SerializeField] Image unitImage;
    public TextMeshProUGUI cost;
    [System.NonSerialized] public PlayerUnitStats unitStats;
    [System.NonSerialized] public Stronghold stronghold;
    string backlineColor;
    string frontlineColor;
    string collapsedColor;
    public event EventHandler onUpdateUI;
    DragNDrop dragNDrop;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backlineColor = "#" + ColorUtility.ToHtmlStringRGB(StrategyManager.Instance.colorScheme.backline);
        frontlineColor = "#" + ColorUtility.ToHtmlStringRGB(StrategyManager.Instance.colorScheme.frontline);
        collapsedColor = "#" + ColorUtility.ToHtmlStringRGB(StrategyManager.Instance.colorScheme.collapsed);
        UpdateUI();
        dragNDrop = GetComponent<DragNDrop>();
        dragNDrop.dropAction = DragNDropAction;
    }

    void UpdateUI()
    {
        if (unitStats == null) return;
        unitName.text = unitStats.unitName;
        className.text = $"Class: {unitStats.unitType.className}";
        hp.text = $"HP: {unitStats.maxHealth}";
        speed.text = $"Speed: {unitStats.speed}";
        defense.text = $"Defense: " +
            $"<color={backlineColor}>{unitStats.unitType.backlineArmor}</color> " +
            $"<color={collapsedColor}>{unitStats.unitType.collapsedArmor}</color> " +
            $"<color={frontlineColor}>{unitStats.unitType.frontlineArmor}</color>";
        unitImage.sprite = unitStats.unitType.classImage;
        cost.text = $"Cost: {unitStats.unitType.cost}";
        onUpdateUI?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateUnit(PlayerUnitStats newUnit)
    {
        unitStats = newUnit;
        UpdateUI();
    }

    void AddToSquad()
    {
        stronghold.RemoveFromBarracks(unitStats);
        StrategyEvents.Instance.AddUnitToSquad(unitStats);
    }

    bool DragNDropAction()
    {
        DragNDropData data = new DragNDropData();
        data.unitStats = unitStats;
        data.unitCard = this;
        return StrategyManager.Instance.dropSpot.DropFunc(data);
    }
}
