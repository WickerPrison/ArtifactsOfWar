using UnityEngine;
using UnityEngine.UI;

public class MainBaseMenu : MonoBehaviour
{
    [SerializeField] Image menu;
    [SerializeField] Transform availableToRecruit;
    [SerializeField] GameObject unitOptionPrefab;

    private void Start()
    {
        menu.gameObject.SetActive(false);
        for(int i = 0; i < 5; i++)
        {
            Instantiate(unitOptionPrefab).transform.SetParent(availableToRecruit);
        }
    }

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
