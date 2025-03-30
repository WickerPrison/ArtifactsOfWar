using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecruitUnit : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cost;
    UnitMenuCard unitCard;

    private void Awake()
    {
        unitCard = GetComponent<UnitMenuCard>();
    }

    private void UnitCard_onUpdateUI(object sender, System.EventArgs e)
    {
        cost.text = $"Recruit: {unitCard.unitStats.unitType.cost}";
    }

    public void BuyUnit()
    {
        if(PersistData.money >= unitCard.unitStats.unitType.cost)
        {
            PersistData.money -= unitCard.unitStats.unitType.cost;
            PersistData.army.Add(unitCard.unitStats);
            unitCard.stronghold.AddToBarracks(unitCard.unitStats);
            unitCard.stronghold.availableRecruits.Remove(unitCard.unitStats);
            StrategyEvents.Instance.UpdateStrongholdUnits(unitCard.stronghold);
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        unitCard.onUpdateUI += UnitCard_onUpdateUI;
    }

    private void OnDisable()
    {
        unitCard.onUpdateUI -= UnitCard_onUpdateUI;
    }
}
