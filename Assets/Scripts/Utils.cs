using UnityEngine;
using System;
using System.Collections.Generic;

public static class Utils
{
    public static void IncorrectInitialization(string className)
    {
        throw new System.Exception($"{className} must be initialized with constructor menthod");
    }

    public static void UpsertPersistentList<T>(List<T> list, T data) where T : IHaveGuid
    {
        int index = list.FindIndex(item => item.guid == data.guid);

        if(index != -1)
        {
            list[index] = data;
        }
        else
        {
            list.Add(data);
        }
    }

    public static void RemovePersistentList<T>(List<T> list, Guid guid) where T : IHaveGuid
    {
        int index = list.FindIndex(item => item.guid == guid);

        if (index != -1)
        {
            list.RemoveAt(index);
        }
    }
}
