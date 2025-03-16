using UnityEngine;
using TMPro;

public class UnitDisplay : MonoBehaviour
{
    [SerializeField] SpriteRenderer outline;
    [SerializeField] TextMeshProUGUI unitName;
    [SerializeField] SpriteRenderer glow;
    [SerializeField] SpriteRenderer image;
    [SerializeField] SpriteRenderer turnMeterFill;
    [SerializeField] float maxTurnMeter;
    IAmUnit unit;
    Color defaultColor;

    private void Awake()
    {
        unit = GetComponent<IAmUnit>();
    }

    private void Start()
    {
        glow.enabled = false;
        UpdateUnitDisplay();
    }

    private void Unit_onStartTurn(object sender, System.EventArgs e)
    {
        glow.enabled = true;
    }

    private void Unit_onEndTurn(object sender, System.EventArgs e)
    {
        glow.enabled = false;
    }

    public void SetSelectable(bool isSelectable)
    {
        glow.enabled = isSelectable;
    }

    public void SetHighlighted(bool isHighlighted)
    {
        if (isHighlighted)
        {
            glow.color = Color.white;
            outline.color = Color.white;
        }
        else
        {
            outline.color = defaultColor;
            glow.color = defaultColor;
        }
    }

    private void Unit_onUpdateTurnMeter(float turnMeter)
    {
        turnMeterFill.transform.localScale = new Vector3(
            Mathf.Lerp(0, maxTurnMeter, turnMeter / 100),
            turnMeterFill.transform.localScale.y,
            turnMeterFill.transform.localScale.z);
    }

    void UpdateUnitDisplay()
    {
        UnitDisplayData data = unit.GetDisplayData();
        unitName.text = data.unitName;
        image.sprite = data.unitImage;
    }

    private void Unit_onRowChange(UnitRow row)
    {
        defaultColor = GameManager.Instance.rowColorDict[row];
        outline.color = defaultColor;
        glow.color = defaultColor;
    }

    private void OnEnable()
    {
        unit.onRowChange += Unit_onRowChange;
        unit.onStartTurn += Unit_onStartTurn;
        unit.onEndTurn += Unit_onEndTurn;
        unit.onUpdateTurnMeter += Unit_onUpdateTurnMeter;
    }

    private void OnDisable()
    {
        unit.onRowChange -= Unit_onRowChange;
        unit.onStartTurn -= Unit_onStartTurn;
        unit.onEndTurn -= Unit_onEndTurn;
        unit.onUpdateTurnMeter -= Unit_onUpdateTurnMeter;
    }
}
