using UnityEngine;

public interface IAmOrigin
{
    Transform transform { get; }

    public void PrepareDeparture();
}
