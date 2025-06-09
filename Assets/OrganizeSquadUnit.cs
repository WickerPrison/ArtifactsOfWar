using UnityEngine;

public class OrganizeSquadUnit : MonoBehaviour
{
    [SerializeField] UnitRow row;
    [SerializeField] int pos;
    DropSpot dropSpot;
    AssignSquadMenu assignSquad;
    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        assignSquad = GetComponentInParent<AssignSquadMenu>();
        dropSpot = GetComponent<DropSpot>();
        dropSpot.DropFunc = AddToSquad;
    }

    bool AddToSquad(DragNDropData data)
    {
        if (!assignSquad.AddUnit(row, pos, data)) return false;
        data.unitCard.GoToPosition(rectTransform.position);
        return true;
    }
}
