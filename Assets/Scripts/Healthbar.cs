using UnityEngine;
using TMPro;

public class Healthbar : MonoBehaviour
{
    [SerializeField] SpriteRenderer fill;
    [SerializeField] TextMeshProUGUI healthText;
    IAmUnit unit;

    private void Awake()
    {
        unit = GetComponentInParent<IAmUnit>();
    }

    private void Start()
    {
        fill.color = GameManager.Instance.colorScheme.frontline;
    }

    private void Unit_onUpdateHealth(int health, int maxHealth)
    {
        float healthRat = (float)health / (float)maxHealth;
        fill.transform.localScale = new Vector3(
            Mathf.Lerp(0f, 6.4f, healthRat),
            fill.transform.localScale.y,
            fill.transform.localScale.z);
        healthText.text = health.ToString() + "/" + maxHealth.ToString();
    }

    private void OnEnable()
    {
        unit.onUpdateHealth += Unit_onUpdateHealth;
    }

    private void OnDisable()
    {
        unit.onUpdateHealth -= Unit_onUpdateHealth;
    }
}
