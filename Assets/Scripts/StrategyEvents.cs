using System;
using UnityEngine;

public class StrategyEvents : MonoBehaviour
{
    private static StrategyEvents _instance;
    public static StrategyEvents Instance { get { return _instance; } }

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

    public event EventHandler onOpenMainBaseMenu;
    public event EventHandler<int> onMoneyChange;

    public void OpenMainBaseMenu()
    {
        onOpenMainBaseMenu?.Invoke(this, EventArgs.Empty);
    }

    public void SetMoney(int amount)
    {
        onMoneyChange?.Invoke(this, amount);
    }
}
