using UnityEngine;

public class MainBase : MonoBehaviour
{
    private void OnMouseDown()
    {
        StrategyEvents.Instance.OpenMainBaseMenu();
    }
}
