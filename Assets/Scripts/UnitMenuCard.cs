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
    RectTransform rectTransform;
    public Action leaveFunc;
    DragNDropData _data;
    DragNDropData data
    {
        get
        {
            if(_data == null)
            {
                _data = new DragNDropData();
                _data.unitStats = unitStats;
                _data.unitCard = this;
                _data.leaveFunc = leaveFunc;
            }
            return _data;
        }
    }
    DragNDrop _dragNDrop;
    DragNDrop dragNDrop
    {
        get
        {
            if(_dragNDrop == null)
            {
                _dragNDrop = GetComponent<DragNDrop>();
            }
            return _dragNDrop;
        }
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backlineColor = "#" + ColorUtility.ToHtmlStringRGB(StrategyManager.Instance.colorScheme.backline);
        frontlineColor = "#" + ColorUtility.ToHtmlStringRGB(StrategyManager.Instance.colorScheme.frontline);
        collapsedColor = "#" + ColorUtility.ToHtmlStringRGB(StrategyManager.Instance.colorScheme.collapsed);
        UpdateUI();
        dragNDrop.dropAction = DragNDropAction;
        rectTransform = GetComponent<RectTransform>();
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

    public void EnableDragNDrop(bool enable)
    {
        dragNDrop.dragNDropEnabled = enable;
    }

    bool DragNDropAction()
    {
        if(StrategyManager.Instance.dropSpot.DropFunc == null) return false;
        return StrategyManager.Instance.dropSpot.DropFunc(data);
    }

    public void GoToPosition(Vector2 destination)
    {
        StartCoroutine(dragNDrop.GoToPosition(rectTransform.position, destination));
    }
}
