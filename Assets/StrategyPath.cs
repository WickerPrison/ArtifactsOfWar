using UnityEngine;
using System.Collections.Generic;

public class StrategyPath : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    [SerializeField] GameObject travelSquadPrefab;
    IAmOrigin origin;
    IAmDestination destination;
    int travelTime;

    public void Setup(IAmOrigin _origin, IAmDestination _destination)
    {
        origin = _origin;
        destination = _destination;
        travelTime = Mathf.CeilToInt(Vector3.Distance(origin.transform.position, destination.transform.position));
    }

    private void Start()
    {
        line.SetPosition(0, origin.transform.position);
        line.SetPosition(1, destination.transform.position);
    }

    public void Depart(PlayerSquad squad)
    {
        TravelSquad travelSquad = Instantiate(travelSquadPrefab).GetComponent<TravelSquad>();
        travelSquad.squad = squad;
        travelSquad.origin = origin;
        travelSquad.destination = destination;
        travelSquad.totalDays = travelTime;
        travelSquad.transform.position = origin.transform.position;
        Destroy(gameObject);
    }

    public void DestroyStrategyPath()
    {
        Destroy(gameObject);
    }

    private void Strategy_onDeselectStronghold(object sender, System.EventArgs e)
    {
        DestroyStrategyPath();
    }

    private void OnEnable()
    {
        StrategyEvents.Instance.onDeselectStronghold += Strategy_onDeselectStronghold;
    }

    private void OnDisable()
    {
        StrategyEvents.Instance.onDeselectStronghold -= Strategy_onDeselectStronghold;

    }
}
