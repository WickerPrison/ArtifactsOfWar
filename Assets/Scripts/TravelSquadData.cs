using UnityEngine;

public class TravelSquadData
{
    public PlayerSquad squad;
    public IAmDestination destination;
    public Vector3 position;
    public float speed;

    public TravelSquadData(PlayerSquad playerSquad, Vector3 squadPosition)
    {
        squad = playerSquad;
        position = squadPosition;
    }
}