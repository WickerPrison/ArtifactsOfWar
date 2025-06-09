using UnityEngine;
using System.Collections.Generic;

public interface IAmDestination
{
    Transform transform { get; }
    void SquadArrived(PlayerSquad squad);
}
