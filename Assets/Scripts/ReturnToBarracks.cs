using UnityEngine;

public class ReturnToBarracks : MonoBehaviour
{
    DropSpot dropSpot;

    private void Start()
    {
        dropSpot = GetComponent<DropSpot>();
        dropSpot.DropFunc = ReturnUnit;
    }

    bool ReturnUnit(DragNDropData data)
    {
        data.unitCard.stronghold.AddToBarracks(data.unitStats);
        data.leaveFunc();
        Destroy(data.unitCard.gameObject);
        return true;
    }
}
