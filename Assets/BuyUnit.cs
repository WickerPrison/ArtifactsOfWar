using UnityEngine;

public class BuyUnit : MonoBehaviour
{
    DropSpot dropSpot;

    private void Start()
    {
        dropSpot = GetComponent<DropSpot>();
        dropSpot.DropFunc = PurchaseUnit;
    }

    bool PurchaseUnit(DragNDropData data)
    {
        if (PersistData.money >= data.unitStats.unitType.cost)
        {
            PersistData.money -= data.unitStats.unitType.cost;
            PersistData.army.Add(data.unitStats);
            data.unitCard.stronghold.AddToBarracks(data.unitStats);
            data.unitCard.stronghold.availableRecruits.Remove(data.unitStats);
            StrategyEvents.Instance.UpdateStrongholdUnits(data.unitCard.stronghold);
            Destroy(data.unitCard.gameObject);
            return true;
        }
        return false;
    }
}
