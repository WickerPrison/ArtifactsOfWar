using UnityEngine;

public class UnitSlotDisplay : MonoBehaviour
{
    [SerializeField] SpriteRenderer outline;
    [SerializeField] SpriteRenderer innerGlow;
    Collider2D hitBox;
    Color defaultColor;
    [System.NonSerialized] public UnitRow row;

    private void Awake()
    {
        hitBox = GetComponent<Collider2D>();
    }

    private void Start()
    {
        defaultColor = GameManager.Instance.rowColorDict[row];
        SetDefaultColor();
        SetInnerGlowAlpha(0);
    }

    void SetDefaultColor()
    {
        outline.color = defaultColor;
        innerGlow.color = defaultColor;
    }

    public void Activate()
    {
        SetInnerGlowAlpha(0.5f);
    }

    public void Deactivate()
    {
        SetDefaultColor();
        SetInnerGlowAlpha(0);
    }

    public void HoverOver()
    {
        outline.color = Color.white;
        innerGlow.color = Color.white;
        SetInnerGlowAlpha(0.5f);
    }

    public void EndHover()
    {
        SetDefaultColor();
        SetInnerGlowAlpha(0.5f);
    }

    public void Hide()
    {
        outline.enabled = false;
        innerGlow.enabled = false;
        hitBox.enabled = false;
    }

    public void UnHide()
    {
        outline.enabled = true;
        innerGlow.enabled = true;
        hitBox.enabled = true;
    }

    void SetInnerGlowAlpha(float alpha)
    {
        innerGlow.color = new Vector4(innerGlow.color.r, innerGlow.color.g, innerGlow.color.b, alpha);
    }
}
