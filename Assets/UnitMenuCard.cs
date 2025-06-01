using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public enum UnitCardButton
{
    NONE, ADD_TO_SQUAD
}

public class UnitMenuCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI unitName;
    [SerializeField] TextMeshProUGUI className;
    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] TextMeshProUGUI defense;
    [SerializeField] Image unitImage;
    [SerializeField] Button genericButton;
    [SerializeField] TextMeshProUGUI genericButtonText;
    [System.NonSerialized] public PlayerUnitStats unitStats;
    [System.NonSerialized] public Stronghold stronghold;
    string backlineColor;
    string frontlineColor;
    string collapsedColor;
    public event EventHandler onUpdateUI;
    UnitCardButton buttonMode = UnitCardButton.NONE;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backlineColor = "#" + ColorUtility.ToHtmlStringRGB(StrategyManager.Instance.colorScheme.backline);
        frontlineColor = "#" + ColorUtility.ToHtmlStringRGB(StrategyManager.Instance.colorScheme.frontline);
        collapsedColor = "#" + ColorUtility.ToHtmlStringRGB(StrategyManager.Instance.colorScheme.collapsed);
        UpdateUI();
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
        onUpdateUI?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateUnit(PlayerUnitStats newUnit)
    {
        unitStats = newUnit;
        UpdateUI();
    }

    public void GenericButton()
    {
        switch (buttonMode)
        {
            case UnitCardButton.ADD_TO_SQUAD:
                AddToSquad();
                break;
        }
    }

    public void SetButtonMode(UnitCardButton newMode)
    {
        buttonMode = newMode;
        switch (newMode)
        {
            case UnitCardButton.NONE:
                genericButton.gameObject.SetActive(false);
                break;
            case UnitCardButton.ADD_TO_SQUAD:
                genericButton.gameObject.SetActive(true);
                
                break;
        }
    }

    void AddToSquad()
    {
        stronghold.RemoveFromBarracks(unitStats);
        StrategyEvents.Instance.AddUnitToSquad(unitStats);
    }
}
