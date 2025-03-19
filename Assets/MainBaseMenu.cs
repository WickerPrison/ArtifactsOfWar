using UnityEngine;
using UnityEngine.UI;

public class MainBaseMenu : MonoBehaviour
{
    [SerializeField] Image menu;

    private void Strategy_onOpenMainBaseMenu(object sender, System.EventArgs e)
    {
        menu.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        menu.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StrategyEvents.Instance.onOpenMainBaseMenu += Strategy_onOpenMainBaseMenu;
    }

    private void OnDisable()
    {
        StrategyEvents.Instance.onOpenMainBaseMenu -= Strategy_onOpenMainBaseMenu;
    }
}
