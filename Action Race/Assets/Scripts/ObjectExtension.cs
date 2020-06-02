using UnityEngine;
using System.Collections.Generic;

public static class ObjectExtension
{
    static List<Object> savedObjects = new List<Object>();

    public static void DontDestroyOnLoad(this Object obj)
    {
        savedObjects.Add(obj);
        Object.DontDestroyOnLoad(obj);
    }

    public static void DestroyAll()
    {
        foreach (var obj in savedObjects)
            Object.Destroy(obj);

        savedObjects.Clear();
    }
}
