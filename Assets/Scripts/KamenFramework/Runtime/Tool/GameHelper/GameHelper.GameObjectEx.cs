using System;
using UnityEngine;

public static class GameObjectEx
{
    public static T GetOrAddComponent<T>(this GameObject o) where T : Component
    {
        T component = o.GetComponent<T>();
        if (component != null)
        {
            return component;
        }
        return o.AddComponent<T>();
    }

    public static Component GetOrAddComponent(this GameObject o, Type type)
    {
        Component component = o.GetComponent(type);
        if (component != null)
        {
            return component;
        }
        return o.AddComponent(type);
    }
}