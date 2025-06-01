using UnityEngine;

public class MapEncounter : MonoBehaviour, IAmDestination
{
    private void OnMouseDown()
    {
        if (StrategyManager.Instance.strategyState == StrategyState.STRONGHOLD)
        {
            StrategyManager.Instance.CreatePath(this);
        }
    }
}
