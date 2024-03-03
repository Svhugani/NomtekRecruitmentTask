using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    public static void DestroyChildren(Transform transform)
    {

        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }

        foreach (GameObject child in children)
        {
            GameObject.Destroy(child);
        }
    }
}
