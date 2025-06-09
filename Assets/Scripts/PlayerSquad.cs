using UnityEngine;

public class PlayerSquad
{
    public PlayerSquad(PlayerUnitStats[] frontRow, PlayerUnitStats[] backRow)
    {
        frontline = frontRow;
        backline = backRow;
    }

    public PlayerUnitStats[] frontline;
    public PlayerUnitStats[] backline;
}
