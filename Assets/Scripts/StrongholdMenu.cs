using UnityEngine;
using UnityEngine.UI;

public class StrongholdMenu : MonoBehaviour
{
    [SerializeField] Image menu;
    [SerializeField] Transform availableToRecruit;
    [SerializeField] Transform barracks;
    [SerializeField] GameObject recruitUnitPrefab;
    [SerializeField] GameObject displayUnitPrefab;

    private void Start()
    {
        menu.gameObject.SetActive(false);
    }

    private void Strategy_onOpenStrongholdMenu(object sender, Stronghold stronghold)
    {
        menu.gameObject.SetActive(true);
        UpdateUnitDisplays(stronghold);
    }

    void UpdateUnitDisplays(Stronghold stronghold)
    {
        ClearUnitDisplays();
        foreach (PlayerUnitStats unit in stronghold.availableRecruits)
        {
            UnitMenuCard unitCard = Instantiate(recruitUnitPrefab).GetComponent<UnitMenuCard>();
            unitCard.transform.SetParent(availableToRecruit);
            unitCard.unitStats = unit;
            unitCard.stronghold = stronghold;
        }

        foreach(PlayerUnitStats unit in stronghold.GetBarracksCount())
        {
            UnitMenuCard unitCard = Instantiate(displayUnitPrefab).GetComponent<UnitMenuCard>();
            unitCard.transform.SetParent(barracks);
            unitCard.unitStats = unit;
            unitCard.stronghold = stronghold;
        }
    }

    void ClearUnitDisplays()
    {
        for (int i = 0; i < availableToRecruit.childCount; i++)
        {
            Destroy(availableToRecruit.GetChild(i).gameObject);
        }

        for (int i = 0; i < barracks.childCount; i++)
        {
            Destroy(barracks.GetChild(i).gameObject);
        }
    }

    public void Barracks()
    {
        availableToRecruit.gameObject.SetActive(false);
        barracks.gameObject.SetActive(true);
    }

    public void NewRecruits()
    {
        availableToRecruit.gameObject.SetActive(true);
        barracks.gameObject.SetActive(false);
    }

    public void CloseMenu()
    {
        ClearUnitDisplays();
        menu.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StrategyEvents.Instance.onOpenStrongholdMenu += Strategy_onOpenStrongholdMenu;
        StrategyEvents.Instance.onUpdateStrongholdUnits += Strategy_onUpdateStrongholdUnits;
    }

    private void OnDisable()
    {
        StrategyEvents.Instance.onOpenStrongholdMenu -= Strategy_onOpenStrongholdMenu;
        StrategyEvents.Instance.onUpdateStrongholdUnits -= Strategy_onUpdateStrongholdUnits;
    }

    private void Strategy_onUpdateStrongholdUnits(object sender, Stronghold stronghold)
    {
        UpdateUnitDisplays(stronghold);
    }
}
