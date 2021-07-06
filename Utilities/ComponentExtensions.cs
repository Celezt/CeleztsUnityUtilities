using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class ComponentExtensions
{
    /// <summary>
    /// Try to find specified component first on itself, children and lastly parents until no more parent is found.
    /// </summary>
    public static T GetComponentInHierarchy<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component != null)
            return component;

        component = gameObject.GetComponentInChildren<T>();
        if (component != null)
            return component;

        Transform currentParent = gameObject.transform.parent;
        while (currentParent != null)
        {
            component = currentParent.GetComponent<T>();
            if (component != null)
                return component;

            currentParent = currentParent.parent;
        }

        return null;
    }

    public static void SetEnabled(this Behaviour behaviour, bool isEnabled)
    {
        if (behaviour != null)
            behaviour.enabled = isEnabled;
    }
}
