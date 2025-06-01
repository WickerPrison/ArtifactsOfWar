using UnityEngine;
using UnityEngine.UI;

public class StrongholdMenu : MonoBehaviour
{
    [SerializeField] Image menu;
    [SerializeField] Transform availableToRecruit;
    [SerializeField] BarracksDisplay barracks;
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

    void UpdateUnitDisplays(Stronghold stronghold, UnitCardButton buttonType = UnitCardButton.NONE)
    {
        ClearUnitDisplays();
        foreach (PlayerUnitStats unit in stronghold.availableRecruits)
        {
            UnitMenuCard unitCard = Instantiate(recruitUnitPrefab).GetComponent<UnitMenuCard>();
            unitCard.transform.SetParent(availableToRecruit);
            unitCard.unitStats = unit;
            unitCard.stronghold = stronghold;
        }

        barracks.UpdateDisplay(stronghold);
    }

    void ClearUnitDisplays()
    {
        for (int i = 0; i < availableToRecruit.childCount; i++)
        {
            Destroy(availableToRecruit.GetChild(i).gameObject);
        }

        barracks.ClearUnits();
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
        StrategyEvents.Instance.DeselectStronghold();
    }

    private void Strategy_onCreatePath(object sender, StrategyPath strategyPath)
    {
        Barracks();
        barracks.SetButtonMode(UnitCardButton.ADD_TO_SQUAD);
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
