using UnityEngine;

public class TravelSquadData
{
    public PlayerSquad squad;
    public Vector3 destination;
    public Vector3 position;
    public float speed;

    public TravelSquadData(PlayerSquad playerSquad, Vector3 squadPosition)
    {
        squad = playerSquad;
        position = squadPosition;
        destination = squadPosition;
        speed = 1f;
    }
}