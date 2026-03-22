using UnityEngine;
using System.Collections.Generic;
using System;

public class TwoWayMap<T1, T2>
{
    private readonly Dictionary<T1, T2> forward = new();
    private readonly Dictionary<T2, T1> backward = new();

    public void Add(T1 val1, T2 val2)
    {
        forward.Add(val1, val2);
        backward.Add(val2, val1);
    }

    public void Add(T2 val2, T1 val1)
    {
        forward.Add(val1, val2);
        backward.Add(val2, val1);
    }

    public void Foreach(Action<T1> callback)
    {
        if (callback == null) return;
        foreach(T1 t1 in forward.Keys)
        {
            callback(t1);
        }
    }

    public void Foreach(Action<T2> callback)
    {
        if (callback == null) return;
        foreach (T2 t2 in forward.Values)
        {
            callback(t2);
        }
    }

    public T2 Get(T1 val1) => forward[val1];
    public T1 Get(T2 val2) => backward[val2];
}
