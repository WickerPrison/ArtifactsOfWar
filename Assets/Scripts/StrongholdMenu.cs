using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StrongholdMenu : MonoBehaviour
{
    [SerializeField] Image menu;
    [SerializeField] Transform availableToRecruit;
    [SerializeField] Transform barracks;
    [SerializeField] GameObject unitOptionPrefab;
    [SerializeField] BuyUnit buyUnit;

    private void Start()
    {
        menu.gameObject.SetActive(false);
    }

    private void Strategy_onOpenStrongholdMenu(object sender, Stronghold stronghold)
    {
        menu.gameObject.SetActive(true);
        UpdateUnitDisplays(stronghold);
        NewRecruits();
    }

    void UpdateUnitDisplays(Stronghold stronghold)
    {
        ClearUnitDisplays();

        foreach(PlayerUnitStats unit in stronghold.availableRecruits)
        {
            UnitMenuCard unitCard = Instantiate(unitOptionPrefab).GetComponent<UnitMenuCard>();
            unitCard.transform.SetParent(availableToRecruit);
            unitCard.unitStats = unit;
            unitCard.stronghold = stronghold;
            unitCard.cost.gameObject.SetActive(true);
            unitCard.EnableDragNDrop(true);
        }

        foreach(PlayerUnitStats unit in stronghold.GetBarracksCount())
        {
            UnitMenuCard unitCard = Instantiate(unitOptionPrefab).GetComponent<UnitMenuCard>();
            unitCard.transform.SetParent(barracks);
            unitCard.unitStats = unit;
            unitCard.stronghold = stronghold;
            unitCard.cost.gameObject.SetActive(false);
            unitCard.EnableDragNDrop(true);
            unitCard.leaveFunc = () =>
            {
                stronghold.RemoveFromBarracks(unit);
            };
        }
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
