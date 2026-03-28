using UnityEngine;
using TMPro;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        SetMoney(PersistData.money);
    }

    void SetMoney(int amount)
    {
        text.text = "$ " + amount.ToString();
    }

    private void Strategy_onMoneyChange(object sender, int amount)
    {
        SetMoney(amount);
    }

    private void OnEnable()
    {
        StrategyEvents.Instance.onMoneyChange += Strategy_onMoneyChange;
    }

    private void OnDisable()
    {
        StrategyEvents.Instance.onMoneyChange -= Strategy_onMoneyChange;
    }
}
