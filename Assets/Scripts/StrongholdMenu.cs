using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StrongholdMenu : MonoBehaviour
{
    [SerializeField] Image menu;
    [SerializeField] Transform availableToRecruit;
    [SerializeField] Transform barracks;
    [SerializeField] GameObject unitOptionPrefab;
    [SerializeField] DropSpot buyUnit;

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

        UpdateUnitDisplay(stronghold.availableRecruits, stronghold, availableToRecruit, true);
        UpdateUnitDisplay(stronghold.GetBarracksCount(), stronghold, barracks);
    }

    void ClearUnitDisplays()
    {
        for (int i = 0; i < availableToRecruit.childCount; i++)
        {
            Destroy(availableToRecruit.GetChild(i).gameObject);
        }

        for(int i = 0; i < barracks.transform.childCount; i++)
        {
            Destroy(barracks.transform.GetChild(i).gameObject);
        }
    }

    void UpdateUnitDisplay(List<PlayerUnitStats> units, Stronghold stronghold, Transform unitHolder, bool showCost = false)
    {
        foreach(PlayerUnitStats unit in units)
        {
            UnitMenuCard unitCard = Instantiate(unitOptionPrefab).GetComponent<UnitMenuCard>();
            unitCard.transform.SetParent(unitHolder);
            unitCard.unitStats = unit;
            unitCard.stronghold = stronghold;
            unitCard.cost.gameObject.SetActive(showCost);
        }
    }

    public void Barracks()
    {
        availableToRecruit.gameObject.SetActive(false);
        barracks.gameObject.SetActive(true);
        buyUnit.gameObject.SetActive(false);
    }

    public void NewRecruits()
    {
        availableToRecruit.gameObject.SetActive(true);
        barracks.gameObject.SetActive(false);
        buyUnit.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        ClearUnitDisplays();
        menu.gameObject.SetActive(false);
        StrategyEvents.Instance.DeselectStronghold();
    }

    private void Strategy_onCreatePath(object sender, StrategyPath strategyPath)
    {
        Barracks();
    }

    private void OnEnable()
    {
        StrategyEvents.Instance.onSelectStronghold += Strategy_onOpenStrongholdMenu;
        StrategyEvents.Instance.onUpdateStrongholdUnits += Strategy_onUpdateStrongholdUnits;
        StrategyEvents.Instance.onCreatePath += Strategy_onCreatePath;
    }

    private void OnDisable()
    {
        StrategyEvents.Instance.onSelectStronghold -= Strategy_onOpenStrongholdMenu;
        StrategyEvents.Instance.onUpdateStrongholdUnits -= Strategy_onUpdateStrongholdUnits;
        StrategyEvents.Instance.onCreatePath -= Strategy_onCreatePath;
    }

    private void Strategy_onUpdateStrongholdUnits(object sender, Stronghold stronghold)
    {
        UpdateUnitDisplays(stronghold);
    }
}
